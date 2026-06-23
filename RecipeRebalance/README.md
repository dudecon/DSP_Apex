# RecipeRebalance

| Field | Value |
|-------|-------|
| **Suite #** | 0 |
| **Architecture** | [§0 Recipe Rebalance](../DSP%20Apex%20Architecture.md#0-recipe-rebalance) |
| **Status** | Substantial |
| **Progress** | ~72% |
| **Build** | PASS |
| **Tests** | — |
| **Plugin GUID** | `com.paulspooner.dsp.reciperebalance` |
| **Version** | 1.0.0 |

## Vision

Core recipe updates shared by several other mods: off-vein mining, stone-to-ore paths, fusion chains, and exotic transmutation routes.

## Completed

- [x] Helium item registration with icons and hints
- [x] 12 Apex-tab recipes (stone→ores, fusion, exotic transmutation)
- [x] Deuterium fusion patched to produce Helium
- [x] Vein miners on bare ground allowed (`MinerPlacementPatch`)
- [x] Off-vein miners produce stone at ~10% throughput (`MinerStonePatch`)
- [x] Recipe unlock sync on save import / planet load
- [x] Assembler `SetRecipe` execute-data fix for mod recipes
- [x] Public `RecipeRebalanceApi` for other suite mods
- [x] VFPreload bootstrap and mod item preload

## In Progress

- [ ] Extensible exotic-material recipe paths beyond current 12 recipes

## To Do

- [ ] BepInEx config for yields, costs, and realism toggles
- [ ] Unit test project (fusion chains, grid registration, miner patches)
- [ ] Migrate shared bootstrap to `Common`

## Dependencies

| Kind | Packages |
|------|----------|
| BepInEx | — |
| Project | Consumed by MegaStructuresUI (Apex replicator tab) |
| Conceptual | Exotic chains for DysonWeapons (6), ZeroGProduction (4) |

## Known Issues

- Bootstrap may fail silently if `LDB.recipes` is not ready on first VFPreload.
- `RefreshRecipeLinks` avoids `InitItemIds` due to hint-text corruption risk.
- `AssemblyVersion` (0.0.1) mismatches `PluginInfo` (1.0.0).

## Key Files

| File | Role |
|------|------|
| `Plugin.cs` | BepInEx entry point |
| `RecipeBootstrap.cs` | Item/recipe registration |
| `RecipeRebalanceApi.cs` | Public API for suite mods |
| `MinerStonePatch.cs` | Off-vein stone production |
| `MinerPlacementPatch.cs` | Bare-ground miner placement |
| `VFPreloadPatch.cs` | Early bootstrap hook |
| `AssemblerComponentPatch.cs` | Mod recipe execute-data fix |