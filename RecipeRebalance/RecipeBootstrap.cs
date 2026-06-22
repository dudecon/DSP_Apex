using System.Collections.Generic;
using BepInEx.Logging;

namespace RecipeRebalance
{
    internal static class RecipeBootstrap
    {
        private static bool applied;
        private static TechProto cachedSmeltPreTech;

        internal static readonly List<int> ModRecipeIds = new List<int>();
        internal static bool BootstrapComplete { get; private set; }

        internal static void Apply(ManualLogSource logger)
        {
            if (applied || LDB.recipes == null || LDB.items == null)
                return;

            if (LDB.recipes.Select(1) == null)
                return;

            ApexIds.AssignRuntimeIds(logger);
            RegisterItems(logger);
            RegisterRecipes(logger);
            PatchDeuteriumFusion(logger);

            try
            {
                RefreshRecipeLinks();
            }
            catch (System.Exception ex)
            {
                logger.LogError($"RecipeRebalance: RefreshRecipeLinks failed: {ex}");
            }

            PreloadModItems(logger);
            ModItemHintUtil.ApplyHeliumHints(logger);
            ModItemIconLoader.Apply(logger);
            ReloadIcons();
            LogRegistrationDiagnostics(logger);

            applied = true;
            BootstrapComplete = true;
            logger.LogInfo("RecipeRebalance: recipes, tech unlocks, and items registered.");
        }

        private static void ReloadIcons()
        {
            if (GameMain.iconSet == null)
                return;

            GameMain.iconSet.loaded = false;
            GameMain.iconSet.Create();
            GameMain.iconSet.itemIconIndexBuffer?.SetData(GameMain.iconSet.itemIconIndex);
            GameMain.iconSet.recipeIconIndexBuffer?.SetData(GameMain.iconSet.recipeIconIndex);
        }

        private static void PreloadModItems(ManualLogSource logger)
        {
            foreach (var itemId in new[] { ApexIds.Helium })
            {
                if (!LDB.items.Exist(itemId))
                    continue;

                var item = LDB.items.Select(itemId);
                try
                {
                    ProtoUtil.PreloadItem(item);
                }
                catch (System.Exception ex)
                {
                    logger.LogWarning(
                        $"RecipeRebalance: Preload skipped for item {itemId} ({item?.Name}): {ex.Message}");
                }
            }
        }

        private static void RegisterItems(ManualLogSource logger)
        {
            var deuterium = LDB.items.Select(ApexIds.Deuterium);

            RegisterFluidItem(
                ApexIds.Helium,
                "Apex Helium",
                "Helium produced by deuterium fusion. Feed into energetic graphite synthesis.",
                deuterium?.IconPath ?? "Icons/Item/1121",
                0);

            logger.LogInfo("RecipeRebalance: registered Helium item.");
        }

        private static void RegisterFluidItem(int id, string name, string description, string iconPath, int gridIndex)
        {
            if (LDB.items.Exist(id))
                return;

            var item = new ItemProto
            {
                ID = id,
                Name = name,
                Description = description,
                Type = EItemType.Material,
                StackSize = 100,
                IsFluid = true,
                IconPath = iconPath,
                GridIndex = gridIndex,
                Productive = true
            };
            ProtoRegistry.AddItem(item);
        }

        private static void RegisterRecipes(ManualLogSource logger)
        {
            var stoneBrick = LDB.recipes.Select(5);
            var siliconSmelt = LDB.recipes.Select(3);
            var smeltTechTemplate = siliconSmelt ?? stoneBrick;
            var kimberliteChem = FindRecipeProducing(ApexIds.Diamond);
            var fractalChem = FindRecipeProducing(ApexIds.CrystalSilicon);
            var strangeRecipe = FindRecipeProducing(ApexIds.StrangeMatter);
            var artificialStarTemplate = FindDeuteriumFusionRecipe();

            siliconSmelt?.FindPreTech();
            stoneBrick?.FindPreTech();
            cachedSmeltPreTech = ResolveSmeltPreTech(smeltTechTemplate);

            // Replicator tab III (Apex) — all mod recipes in one matrix; UI provided by MegaStructuresUI.
            const int modRecipeCount = 11;
            int[] apexSlots = RecipeGrid.GetApexTabSlots(modRecipeCount, logger);

            int Slot(int index, int fallback) => index < apexSlots.Length ? apexSlots[index] : fallback;

            AddSmeltRecipe(
                ApexIds.RecipeStoneToIronOre,
                "Apex Stone to Iron Ore",
                smeltTechTemplate,
                new[] { ApexIds.Stone },
                new[] { 10 },
                new[] { ApexIds.IronOre },
                new[] { 1 },
                240,
                Slot(0, RecipeGrid.Encode(RecipeGrid.ApexTab, 1, 1)));

            AddSmeltRecipe(
                ApexIds.RecipeStoneToCopperOre,
                "Apex Stone to Copper Ore",
                smeltTechTemplate,
                new[] { ApexIds.Stone },
                new[] { 10 },
                new[] { ApexIds.CopperOre },
                new[] { 1 },
                240,
                Slot(1, RecipeGrid.Encode(RecipeGrid.ApexTab, 1, 2)));

            AddSmeltRecipe(
                ApexIds.RecipeStoneToSiliconOre,
                "Apex Stone to Silicon Ore",
                smeltTechTemplate,
                new[] { ApexIds.Stone },
                new[] { 12 },
                new[] { ApexIds.SiliconOre },
                new[] { 1 },
                300,
                Slot(2, RecipeGrid.Encode(RecipeGrid.ApexTab, 1, 3)));

            AddSmeltRecipe(
                ApexIds.RecipeStoneToTitaniumOre,
                "Apex Stone to Titanium Ore",
                smeltTechTemplate,
                new[] { ApexIds.Stone },
                new[] { 15 },
                new[] { ApexIds.TitaniumOre },
                new[] { 1 },
                360,
                Slot(3, RecipeGrid.Encode(RecipeGrid.ApexTab, 1, 4)));

            AddSmeltRecipe(
                ApexIds.RecipeStoneToCoal,
                "Apex Stone to Coal",
                smeltTechTemplate,
                new[] { ApexIds.Stone },
                new[] { 8 },
                new[] { ApexIds.Coal },
                new[] { 1 },
                180,
                Slot(4, RecipeGrid.Encode(RecipeGrid.ApexTab, 1, 5)));

            AddChemicalRecipe(
                ApexIds.RecipeStoneToKimberlite,
                "Apex Stone to Kimberlite Ore",
                kimberliteChem ?? stoneBrick,
                new[] { ApexIds.Stone, ApexIds.Hydrogen },
                new[] { 20, 10 },
                new[] { ApexIds.KimberliteOre },
                new[] { 1 },
                480,
                Slot(5, RecipeGrid.Encode(RecipeGrid.ApexTab, 1, 6)));

            AddChemicalRecipe(
                ApexIds.RecipeStoneToFractalSilicon,
                "Apex Stone to Fractal Silicon",
                fractalChem ?? siliconSmelt ?? stoneBrick,
                new[] { ApexIds.Stone, ApexIds.EnergeticGraphite },
                new[] { 25, 5 },
                new[] { ApexIds.FractalSilicon },
                new[] { 1 },
                600,
                Slot(6, RecipeGrid.Encode(RecipeGrid.ApexTab, 1, 7)));

            AddParticleRecipe(
                ApexIds.RecipeHeliumToEnergeticGraphite,
                "Apex Triple-Helium to Energetic Graphite",
                artificialStarTemplate ?? stoneBrick,
                new[] { ApexIds.Helium },
                new[] { 3 },
                new[] { ApexIds.EnergeticGraphite },
                new[] { 1 },
                240,
                Slot(7, RecipeGrid.Encode(RecipeGrid.ApexTab, 1, 8)));

            AddParticleRecipe(
                ApexIds.RecipeEnergeticGraphiteToStone,
                "Apex Energetic Graphite Fusion to Stone",
                artificialStarTemplate ?? stoneBrick,
                new[] { ApexIds.EnergeticGraphite },
                new[] { 2 },
                new[] { ApexIds.Stone },
                new[] { 5 },
                180,
                Slot(8, RecipeGrid.Encode(RecipeGrid.ApexTab, 1, 9)));

            AddParticleRecipe(
                ApexIds.RecipeEnergeticGraphiteToStrangeMatter,
                "Apex Energetic Graphite Transmutation to Strange Matter",
                strangeRecipe ?? artificialStarTemplate ?? stoneBrick,
                new[] { ApexIds.EnergeticGraphite, ApexIds.Deuterium, ApexIds.Hydrogen },
                new[] { 10, 20, 40 },
                new[] { ApexIds.StrangeMatter },
                new[] { 1 },
                720,
                Slot(9, RecipeGrid.Encode(RecipeGrid.ApexTab, 1, 10)));

            AddParticleRecipe(
                ApexIds.RecipeEnergeticGraphiteToUnipolarMagnet,
                "Apex Energetic Graphite Transmutation to Unipolar Magnet",
                strangeRecipe ?? artificialStarTemplate ?? stoneBrick,
                new[] { ApexIds.EnergeticGraphite, ApexIds.Helium, ApexIds.GravitonLens },
                new[] { 8, 12, 1 },
                new[] { ApexIds.UnipolarMagnet },
                new[] { 1 },
                900,
                Slot(10, RecipeGrid.Encode(RecipeGrid.ApexTab, 1, 11)));

            logger.LogInfo("RecipeRebalance: registered stone, fusion, and exotic transmutation recipes.");
        }

        private static void PatchDeuteriumFusion(ManualLogSource logger)
        {
            var fusion = FindDeuteriumFusionRecipe();
            if (fusion == null)
            {
                logger.LogWarning("RecipeRebalance: could not find deuterium fusion recipe to patch.");
                return;
            }

            fusion.Results = AppendResult(fusion.Results, fusion.ResultCounts, ApexIds.Helium, 1, out fusion.ResultCounts);
            logger.LogInfo($"RecipeRebalance: patched fusion recipe {fusion.ID} to produce Helium.");
        }

        private static RecipeProto FindDeuteriumFusionRecipe()
        {
            var recipes = LDB.recipes.dataArray;
            if (recipes == null)
                return null;

            RecipeProto fallback = null;
            foreach (var recipe in recipes)
            {
                if (recipe == null)
                    continue;

                if (!ArrayContains(recipe.Items, ApexIds.Deuterium))
                    continue;

                if (recipe.Type == ERecipeType.Particle)
                    return recipe;

                if (fallback == null)
                    fallback = recipe;
            }

            return fallback;
        }

        private static RecipeProto FindRecipeProducing(int itemId)
        {
            var recipes = LDB.recipes.dataArray;
            if (recipes == null)
                return null;

            foreach (var recipe in recipes)
            {
                if (recipe == null)
                    continue;

                if (ArrayContains(recipe.Results, itemId))
                    return recipe;
            }

            return null;
        }

        private static void AddSmeltRecipe(
            int id,
            string name,
            RecipeProto techTemplate,
            int[] inputs,
            int[] inputCounts,
            int[] outputs,
            int[] outputCounts,
            int timeSpend,
            int gridIndex)
        {
            AddRecipe(id, name, ERecipeType.Smelt, techTemplate, inputs, inputCounts, outputs, outputCounts, timeSpend, gridIndex);
        }

        private static void AddChemicalRecipe(
            int id,
            string name,
            RecipeProto techTemplate,
            int[] inputs,
            int[] inputCounts,
            int[] outputs,
            int[] outputCounts,
            int timeSpend,
            int gridIndex)
        {
            AddRecipe(id, name, ERecipeType.Chemical, techTemplate, inputs, inputCounts, outputs, outputCounts, timeSpend, gridIndex);
        }

        private static void AddParticleRecipe(
            int id,
            string name,
            RecipeProto techTemplate,
            int[] inputs,
            int[] inputCounts,
            int[] outputs,
            int[] outputCounts,
            int timeSpend,
            int gridIndex)
        {
            AddRecipe(id, name, ERecipeType.Particle, techTemplate, inputs, inputCounts, outputs, outputCounts, timeSpend, gridIndex);
        }

        private static void AddRecipe(
            int id,
            string name,
            ERecipeType type,
            RecipeProto techTemplate,
            int[] inputs,
            int[] inputCounts,
            int[] outputs,
            int[] outputCounts,
            int timeSpend,
            int gridIndex)
        {
            var recipe = LDB.recipes.Exist(id) ? LDB.recipes.Select(id) : new RecipeProto { ID = id };
            ApplyRecipeFields(recipe, id, name, type, techTemplate, inputs, inputCounts, outputs, outputCounts, timeSpend, gridIndex);
        }

        private static void ApplyRecipeFields(
            RecipeProto recipe,
            int id,
            string name,
            ERecipeType type,
            RecipeProto techTemplate,
            int[] inputs,
            int[] inputCounts,
            int[] outputs,
            int[] outputCounts,
            int timeSpend,
            int gridIndex)
        {
            recipe.ID = id;
            recipe.Name = name;
            recipe.Type = type;
            recipe.Handcraft = false;
            recipe.Explicit = false;
            recipe.TimeSpend = timeSpend;
            recipe.Items = inputs;
            recipe.ItemCounts = inputCounts;
            recipe.Results = outputs;
            recipe.ResultCounts = outputCounts;
            recipe.GridIndex = gridIndex;
            recipe.IconPath = outputs.Length > 0 ? LDB.items.Select(outputs[0])?.IconPath : "Icons/Item/1005";
            recipe.NonProductive = false;
            recipe.preTech = type == ERecipeType.Smelt
                ? ResolveSmeltPreTech(techTemplate)
                : ResolvePreTech(techTemplate);

            if (recipe.preTech == null)
                Plugin.Log.LogWarning($"RecipeRebalance: no prerequisite tech resolved for {name}.");

            try
            {
                if (!LDB.recipes.Exist(id))
                    ProtoRegistry.AddRecipe(recipe);
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"RecipeRebalance: failed to register recipe {id} ({name}): {ex.Message}");
                return;
            }

            TechBootstrap.UnlockRecipe(recipe);
            if (!ModRecipeIds.Contains(id))
                ModRecipeIds.Add(id);
        }

        private static TechProto ResolveSmeltPreTech(RecipeProto template)
        {
            if (cachedSmeltPreTech != null)
                return cachedSmeltPreTech;

            var fromTemplate = ResolvePreTech(template);
            if (fromTemplate != null)
                return fromTemplate;

            return FindTechUnlockingRecipe(3)
                ?? FindTechUnlockingRecipe(5)
                ?? FindFallbackSmeltTech();
        }

        private static TechProto ResolvePreTech(RecipeProto template)
        {
            if (template == null)
                return null;

            if (template.preTech != null)
                return template.preTech;

            var tech = FindTechUnlockingRecipe(template.ID);
            if (tech != null)
                return tech;

            template.FindPreTech();
            if (template.preTech != null)
                return template.preTech;

            if (template.Type == ERecipeType.Smelt)
                return FindFallbackSmeltTech();

            return null;
        }

        private static TechProto FindTechUnlockingRecipe(int recipeId)
        {
            var techs = LDB.techs?.dataArray;
            if (techs == null)
                return null;

            for (int t = 0; t < techs.Length; t++)
            {
                var tech = techs[t];
                if (tech == null)
                    continue;

                if (tech.UnlockRecipes != null)
                {
                    for (int i = 0; i < tech.UnlockRecipes.Length; i++)
                    {
                        if (tech.UnlockRecipes[i] == recipeId)
                            return tech;
                    }
                }

                if (tech.unlockRecipeArray != null)
                {
                    for (int i = 0; i < tech.unlockRecipeArray.Length; i++)
                    {
                        if (tech.unlockRecipeArray[i]?.ID == recipeId)
                            return tech;
                    }
                }
            }

            return null;
        }

        private static TechProto FindFallbackSmeltTech()
        {
            var templates = new[]
            {
                LDB.recipes.Select(3),
                LDB.recipes.Select(1),
                LDB.recipes.Select(2),
                LDB.recipes.Select(4),
                LDB.recipes.Select(5)
            };

            for (int i = 0; i < templates.Length; i++)
            {
                var recipe = templates[i];
                if (recipe == null)
                    continue;

                var tech = FindTechUnlockingRecipe(recipe.ID);
                if (tech != null)
                    return tech;

                if (recipe.preTech != null)
                    return recipe.preTech;

                recipe.FindPreTech();
                if (recipe.preTech != null)
                    return recipe.preTech;
            }

            var recipes = LDB.recipes?.dataArray;
            if (recipes != null)
            {
                for (int i = 0; i < recipes.Length; i++)
                {
                    var recipe = recipes[i];
                    if (recipe?.Type != ERecipeType.Smelt || recipe.preTech == null)
                        continue;

                    return recipe.preTech;
                }
            }

            return null;
        }

        private static int[] AppendResult(int[] results, int[] resultCounts, int itemId, int count, out int[] newCounts)
        {
            if (results == null || results.Length == 0)
            {
                newCounts = new[] { count };
                return new[] { itemId };
            }

            if (resultCounts == null || resultCounts.Length != results.Length)
            {
                resultCounts = new int[results.Length];
                for (int i = 0; i < results.Length; i++)
                    resultCounts[i] = 1;
            }

            for (int i = 0; i < results.Length; i++)
            {
                if (results[i] == itemId)
                {
                    newCounts = resultCounts;
                    newCounts[i] += count;
                    return results;
                }
            }

            var mergedResults = new int[results.Length + 1];
            newCounts = new int[resultCounts.Length + 1];
            for (int i = 0; i < results.Length; i++)
            {
                mergedResults[i] = results[i];
                newCounts[i] = resultCounts[i];
            }

            mergedResults[results.Length] = itemId;
            newCounts[resultCounts.Length] = count;
            return mergedResults;
        }

        private static bool ArrayContains(int[] array, int value)
        {
            if (array == null)
                return false;

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == value)
                    return true;
            }

            return false;
        }

        private static void RebuildRecipeExecuteData()
        {
            RecipeProto.recipeExecuteData = new Dictionary<int, RecipeExecuteData>();
            var dataArray = LDB.recipes.dataArray;
            if (dataArray == null)
                return;

            for (int i = 0; i < dataArray.Length; i++)
            {
                var recipe = dataArray[i];
                if (recipe == null || RecipeProto.recipeExecuteData.ContainsKey(recipe.ID))
                    continue;

                RecipeProto.recipeExecuteData.Add(
                    recipe.ID,
                    new RecipeExecuteData(
                        recipe.Items,
                        recipe.ItemCounts,
                        recipe.Results,
                        recipe.ResultCounts,
                        recipe.TimeSpend * 10000,
                        recipe.TimeSpend * 100000,
                        recipe.productive));
            }
        }

        private static void PreloadAllRecipes(ManualLogSource logger)
        {
            var dataArray = LDB.recipes.dataArray;
            if (dataArray == null)
                return;

            for (int i = 0; i < dataArray.Length; i++)
            {
                var recipe = dataArray[i];
                if (recipe == null)
                    continue;

                try
                {
                    typeof(RecipeProto).GetField(
                        "index",
                        System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
                        ?.SetValue(recipe, i);
                    recipe.Preload(i);
                }
                catch (System.Exception ex)
                {
                    logger.LogWarning(
                        $"RecipeRebalance: Preload skipped for recipe {recipe.ID} ({recipe.Name}): {ex.Message}");
                }
            }
        }

        private static void LogRegistrationDiagnostics(ManualLogSource logger)
        {
            int missing = 0;
            foreach (var recipeId in ModRecipeIds)
            {
                if (LDB.recipes.Select(recipeId) == null)
                {
                    missing++;
                    logger.LogWarning($"RecipeRebalance: recipe {recipeId} missing from LDB.recipes after bootstrap.");
                }
            }

            logger.LogInfo(
                $"RecipeRebalance: bootstrap complete — {ModRecipeIds.Count} mod recipes, {missing} missing from LDB.");
        }

        private static void RefreshRecipeLinks()
        {
            var touched = new HashSet<int>
            {
                ApexIds.Stone,
                ApexIds.IronOre,
                ApexIds.CopperOre,
                ApexIds.SiliconOre,
                ApexIds.TitaniumOre,
                ApexIds.Coal,
                ApexIds.KimberliteOre,
                ApexIds.FractalSilicon,
                ApexIds.Hydrogen,
                ApexIds.Deuterium,
                ApexIds.StrangeMatter,
                ApexIds.UnipolarMagnet,
                ApexIds.GravitonLens,
                ApexIds.Helium,
                ApexIds.EnergeticGraphite
            };

            // Do not call InitItemIds here — it rebuilds itemIds after mod items are appended,
            // which makes Preload map new IDs onto stale vanilla itemNames (wrong hint text).
            ItemProto.InitItemIndices();
            RecipeProto.InitRecipeItems();
            PreloadAllRecipes(Plugin.Log);

            foreach (var itemId in touched)
            {
                if (!LDB.items.Exist(itemId))
                    continue;

                ProtoUtil.RefreshItem(LDB.items.Select(itemId));
            }
        }
    }
}