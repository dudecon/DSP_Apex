using System;
using System.IO;
using System.Linq;
using System.Reflection;
using ICSharpCode.Decompiler;
using ICSharpCode.Decompiler.CSharp;
using ICSharpCode.Decompiler.TypeSystem;
using Mono.Cecil;
using Mono.Cecil.Cil;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length > 0 && args[0] == "tip")
        {
            DumpTipIl.Run();
            return;
        }

        var repoRoot = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", ".."));
        var refsDir = Path.Combine(repoRoot, "references");
        var managed = @"C:\Program Files (x86)\Steam\steamapps\common\Dyson Sphere Program\DSPGAME_Data\Managed";
        AppDomain.CurrentDomain.AssemblyResolve += (_, e) =>
        {
            var name = new AssemblyName(e.Name).Name + ".dll";
            foreach (var dir in new[] { managed, refsDir })
            {
                var path = Path.Combine(dir, name);
                if (File.Exists(path))
                    return Assembly.LoadFrom(path);
            }
            return null;
        };

        var asm = Assembly.LoadFrom(Path.Combine(refsDir, "Assembly-CSharp.dll"));
        Type[] types;
        try { types = asm.GetTypes(); }
        catch (ReflectionTypeLoadException ex) { types = ex.Types.Where(t => t != null).ToArray(); }
        DumpAllMethods(asm, "VFPreload");
        DumpMethods(asm, "ItemProto", "Init", "kMax");
        DumpMethods(asm, "PackageStatistics", "Count");
        DumpMethods(asm, "RecipeProto", "Init", "kMax");
        DumpMethods(asm, "RecipeProto", "Preload");
        Console.WriteLine("=== BuildTool/Miner types ===");
        foreach (var t in types.Where(t => t.Name.Contains("BuildTool") || (t.Name.Contains("Miner") && !t.Name.Contains("Window") && !t.Name.Contains("UI"))))
            Console.WriteLine(t.FullName);
        Console.WriteLine();
        var buildModel = asm.GetType("BuildModel");
        var previewModelsField = buildModel?.GetField("previewModels", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        Console.WriteLine($"BuildModel.previewModels type: {previewModelsField?.FieldType}");
        DumpAllMethods(asm, "BuildTool_Click");
        DumpTypeFields(asm, "BuildModel");
        DumpTypeFields(asm, "BuildPreviewModel");
        DumpTypeFields(asm, "BuildPreview");
        DumpTypeFields(asm, "PrefabDesc");
        DumpMethods(asm, "ABN_Miner");
        DumpMethods(asm, "GD_VeinCoveredByMiner");
        foreach (var tn in new[] { "BuildTool_Miner", "ToolMiner", "MinerBuildTool", "VeinBuildTool" })
        {
            var t = asm.GetType(tn);
            if (t != null) DumpAllMethods(asm, tn);
        }
        DumpAllMethods(asm, "TechProtoSet");
        DumpMethods(asm, "TechProto", "Unlock", "Recipe", "Find", "Init", "Preload");
        DumpMethods(asm, "RecipeProto", "Grid", "Tech", "Explicit");
        DumpMethods(asm, "AssemblerComponent", "recipe");
        DumpAllMethods(asm, "UIAssemblerWindow");
        foreach (var tn in new[]
        {
            "UIReplicatorWindow", "UIRecipePicker", "UIRecipeGrid", "UIRecipeSlot",
            "UIHandcraft", "UIPackageGrid", "UIPlayerPackage", "UIStorageGrid",
            "StorageComponent", "PlayerPackage", "UIBeltWindow"
        })
            DumpTypeFields(asm, tn);

        DumpAllMethods(asm, "UIRecipePicker");
        DumpAllMethods(asm, "UIReplicatorWindow");
        DumpAllMethods(asm, "UIStorageGrid");
        DumpMethods(asm, "UIStorageGrid", "row", "size", "grid", "Set", "Update", "Refresh", "Create", "Open");
        DumpMethods(asm, "UIRecipePicker", "row", "grid", "Set", "Update", "Refresh", "Create", "Open", "Init");
        DumpMethods(asm, "UIReplicatorWindow", "row", "grid", "Set", "Update", "Refresh", "Create", "Open", "Init");
        DumpTypesMatching(asm, "Recipe", "Picker");
        DumpRecipeGridSamples(asm);
        DumpProtoSetSelect(asm);
        DumpExistSelectIL(asm);

        var asmPath = Path.Combine(managed, "Assembly-CSharp.dll");
        DumpIl(managed, "UIRecipePicker", "SetMaterialProps", "OnTypeButtonClick", "_OnCreate");
        DumpIl(managed, "UIReplicatorWindow", "SetMaterialProps", "OnTypeButtonClick", "RefreshRecipeIcons", "TestMouseRecipeIndex", "_OnCreate");
        DumpIl(managed, "UIAssemblerWindow", "RefreshRecipeIcons", "TestMouseRecipeIndex", "SetMaterialProps", "_OnCreate", "_OnOpen");
        DumpIl(managed, "UIStorageGrid", "SetRectSize", "OnStorageSizeChanged", "_OnCreate");
        DumpTypeFields(asm, "UIAssemblerWindow");
    }

    static void DumpIl(string managedDir, string typeName, params string[] methodNames)
    {
        var path = Path.Combine(managedDir, "Assembly-CSharp.dll");
        var resolver = new DefaultAssemblyResolver();
        resolver.AddSearchDirectory(managedDir);
        var asm = AssemblyDefinition.ReadAssembly(path, new ReaderParameters { AssemblyResolver = resolver });
        var type = asm.MainModule.Types.FirstOrDefault(t => t.Name == typeName);
        if (type == null)
        {
            Console.WriteLine($"=== IL {typeName}: NOT FOUND ===\n");
            return;
        }

        Console.WriteLine($"=== IL {typeName} ===");
        foreach (var methodName in methodNames)
        {
            var method = type.Methods.FirstOrDefault(m => m.Name == methodName);
            if (method?.Body == null)
            {
                Console.WriteLine($"  -- {methodName}: not found");
                continue;
            }

            Console.WriteLine($"  -- {methodName} --");
            foreach (var ins in method.Body.Instructions)
            {
                string extra = "";
                if (ins.OpCode == OpCodes.Ldc_I4)
                    extra = " " + ins.Operand;
                else if (ins.OpCode == OpCodes.Ldc_I4_S)
                    extra = " " + ins.Operand;
                else if (ins.Operand is FieldReference fr)
                    extra = " " + fr.Name;
                else if (ins.Operand is MethodReference mr)
                    extra = " " + mr.Name;
                else if (ins.Operand is string s)
                    extra = " \"" + s + "\"";
                else if (ins.Operand is float f)
                    extra = " " + f;
                else if (ins.Operand is double d)
                    extra = " " + d;
                Console.WriteLine($"    {ins.OpCode}{extra}");
            }
        }
        Console.WriteLine();
    }

    static void DecompileMethods(string managedDir, string asmPath, string typeName, params string[] methodNames)
    {
        var resolver = new ICSharpCode.Decompiler.Metadata.UniversalAssemblyResolver(asmPath, false, null);
        resolver.AddSearchDirectory(managedDir);
        var decompiler = new CSharpDecompiler(asmPath, resolver, new DecompilerSettings());
        var type = decompiler.TypeSystem.FindType(new FullTypeName(typeName)).GetDefinition();
        if (type == null)
        {
            Console.WriteLine($"=== DECOMPILE {typeName}: NOT FOUND ===\n");
            return;
        }

        Console.WriteLine($"=== DECOMPILE {typeName} ===");
        foreach (var methodName in methodNames)
        {
            var method = type.Methods.FirstOrDefault(m => m.Name == methodName);
            if (method == null)
            {
                Console.WriteLine($"  -- {methodName}: not found");
                continue;
            }

            Console.WriteLine($"  -- {methodName} --");
            Console.WriteLine(decompiler.DecompileAsString(method.MetadataToken));
        }
        Console.WriteLine();
    }

    static void DumpExistSelectIL(Assembly asm)
    {
        var recipeProto = asm.GetType("RecipeProto");
        var protoSet = asm.GetType("ProtoSet`1").MakeGenericType(recipeProto);
        foreach (var name in new[] { "Exist", "Select" })
        {
            var m = protoSet.GetMethod(name, BindingFlags.Public | BindingFlags.Instance);
            Console.WriteLine($"=== {name} body ===");
            if (m == null) continue;
            try
            {
                var body = m.GetMethodBody();
                if (body?.LocalVariables != null)
                    foreach (var lv in body.LocalVariables)
                        Console.WriteLine($"  local {lv.LocalType.Name}");
            }
            catch { Console.WriteLine("  (no body)"); }
            foreach (var p in m.GetParameters()) Console.WriteLine($"  param {p.ParameterType.Name} {p.Name}");
        }
        Console.WriteLine();
    }

    static void DumpProtoSetSelect(Assembly asm)
    {
        var recipeProto = asm.GetType("RecipeProto");
        var protoSet = asm.GetType("ProtoSet`1").MakeGenericType(recipeProto);
        Console.WriteLine("=== ProtoSet<RecipeProto> Select/Exist ===");
        foreach (var m in protoSet.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
        {
            if (m.Name is "Select" or "Exist" or "Init")
                Console.WriteLine($"  {m}");
        }
        foreach (var f in protoSet.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
            Console.WriteLine($"  FIELD {f.FieldType.Name} {f.Name}");
        Console.WriteLine();
    }

    static void DumpTypesMatching(Assembly asm, params string[] parts)
    {
        Console.WriteLine("=== Types matching " + string.Join("/", parts) + " ===");
        Type[] types;
        try { types = asm.GetTypes(); }
        catch (ReflectionTypeLoadException ex) { types = ex.Types.Where(t => t != null).ToArray(); }
        foreach (var t in types.Where(t => parts.All(p => t.Name.IndexOf(p, StringComparison.OrdinalIgnoreCase) >= 0)))
            Console.WriteLine("  " + t.FullName);
        Console.WriteLine();
    }

    static void DumpRecipeGridSamples(Assembly asm)
    {
        Console.WriteLine("=== Recipe grid samples ===");
        try
        {
            var ldbType = asm.GetType("LDB");
            var recipesProp = ldbType.GetProperty("recipes");
            // Can't load LDB without game - print known vanilla grid conventions from field names
        }
        catch { }
        foreach (var name in new[] { "Smelt", "Chemical", "Particle", "Refine" })
            Console.WriteLine($"  ERecipeType.{name}");
        Console.WriteLine();
    }

    static void DumpTypeFields(Assembly asm, string name)
    {
        var t = asm.GetType(name);
        Console.WriteLine($"=== {name} fields ===");
        if (t == null) { Console.WriteLine("NOT FOUND\n"); return; }
        foreach (var f in t.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static))
            Console.WriteLine($"  {f.FieldType.Name} {f.Name}");
        Console.WriteLine();
    }

    static void DumpAllMethods(Assembly asm, string name)
    {
        var t = asm.GetType(name);
        Console.WriteLine($"=== {name} (all methods) ===");
        if (t == null) { Console.WriteLine("NOT FOUND\n"); return; }
        foreach (var m in t.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly))
            Console.WriteLine($"  {m}");
        Console.WriteLine();
    }

    static void DumpMethods(Assembly asm, string name, params string[] contains)
    {
        var t = asm.GetType(name);
        Console.WriteLine($"=== {name} ===");
        if (t == null) { Console.WriteLine("NOT FOUND\n"); return; }
        if (t.IsEnum)
        {
            foreach (var v in Enum.GetNames(t)) Console.WriteLine($"  {v}");
        }
        else
        {
            foreach (var m in t.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly))
                if (contains.Length == 0 || contains.Any(c => m.Name.IndexOf(c, StringComparison.OrdinalIgnoreCase) >= 0))
                    Console.WriteLine($"  {m.IsPublic}/{m.IsAssembly}/{m.IsStatic} {m}");
            foreach (var f in t.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly))
                if (contains.Length == 0 || contains.Any(c => f.Name.IndexOf(c, StringComparison.OrdinalIgnoreCase) >= 0))
                    Console.WriteLine($"  FIELD {f.FieldType.Name} {f.Name}");
        }
        Console.WriteLine();
    }

    static void DumpTypeHierarchy(Assembly asm, string name)
    {
        var t = asm.GetType(name);
        if (t == null)
        {
            Console.WriteLine($"=== {name}: NOT FOUND ===");
            return;
        }
        while (t != null)
        {
            Console.WriteLine($"=== {t.FullName} ===");
            foreach (var m in t.GetMembers(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly))
                Console.WriteLine($"  {m.MemberType,-10} {m}");
            t = t.BaseType;
        }
        Console.WriteLine();
    }

    static void DumpType(Assembly asm, string name)
    {
        var t = asm.GetType(name);
        if (t == null)
        {
            Console.WriteLine($"=== {name}: NOT FOUND ===");
            return;
        }

        Console.WriteLine($"=== {t.FullName} ===");
        foreach (var m in t.GetMembers(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly))
            Console.WriteLine($"  {m.MemberType,-10} {m}");
        Console.WriteLine();
    }
}