using System.IO;
using System.Linq;
using Mono.Cecil;
using Mono.Cecil.Cil;

internal static class DumpTipIl
{
    internal static void Run()
    {
        var managed = @"C:\Program Files (x86)\Steam\steamapps\common\Dyson Sphere Program\DSPGAME_Data\Managed";
        var asmPath = Path.Combine(managed, "Assembly-CSharp.dll");
        var resolver = new DefaultAssemblyResolver();
        resolver.AddSearchDirectory(managed);
        var asm = AssemblyDefinition.ReadAssembly(asmPath, new ReaderParameters { AssemblyResolver = resolver });

        DumpType(asm, "UIItemTip", "SetTip", "ShowTip", "OpenTip", "_OnUpdate", "UpdateTip", "SetItem");
        DumpType(asm, "ItemProto", "Preload", "GetPropName", "GetPropValue", "get_propertyName", "get_description");
        DumpType(asm, "ItemDesc", "Get", "Select");
        DumpFields(asm, "ItemProto");
        DumpFields(asm, "UITip");
    }

    private static void DumpFields(AssemblyDefinition asm, string typeName)
    {
        var type = asm.MainModule.Types.FirstOrDefault(t => t.Name == typeName);
        if (type == null)
            return;

        System.Console.WriteLine($"=== {typeName} fields ===");
        foreach (var field in type.Fields)
            System.Console.WriteLine($"  {field.FieldType.Name} {field.Name}");
        System.Console.WriteLine();
    }

    private static void DumpType(AssemblyDefinition asm, string typeName, params string[] methodNames)
    {
        var type = asm.MainModule.Types.FirstOrDefault(t => t.Name == typeName);
        if (type == null)
        {
            System.Console.WriteLine($"=== {typeName}: NOT FOUND ===\n");
            return;
        }

        foreach (var methodName in methodNames)
        {
            var method = type.Methods.FirstOrDefault(m => m.Name == methodName);
            if (method?.Body == null)
            {
                System.Console.WriteLine($"  -- {typeName}.{methodName}: not found");
                continue;
            }

            System.Console.WriteLine($"  -- {typeName}.{methodName} --");
            foreach (var ins in method.Body.Instructions)
            {
                string extra = "";
                if (ins.Operand is FieldReference fr)
                    extra = " " + fr.Name;
                else if (ins.Operand is MethodReference mr)
                    extra = " " + mr.Name;
                else if (ins.Operand is string s)
                    extra = " \"" + s + "\"";

                System.Console.WriteLine($"    {ins.OpCode}{extra}");
            }

            System.Console.WriteLine();
        }
    }
}