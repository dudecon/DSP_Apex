# ZeroGProduction

| Field | Value |
|-------|-------|
| **Suite #** | 4 |
| **Architecture** | [§4 ZeroGProduction](../DSP%20Apex%20Architecture.md#4-zerogproduction) |
| **Status** | Partial |
| **Progress** | ~38% |
| **Build** | PASS |
| **Tests** | — |
| **Plugin GUID** | `com.paulspooner.dsp.zerogproduction` |
| **Version** | 0.1.0 |

## Vision

0G refineries and assemblers produce megastructure components with high efficiency. Encourages shifting late-game production to orbit with bonuses when paired with rings, stations, and ships.

## Completed

- [x] 0G Refinery + 0G Assembler building protos (cloned from vanilla assemblers)
- [x] Assembler yield bonus patch (1.35× refinery / 1.20× assembler + orbital multipliers)
- [x] Orbital structure detection via `ZeroGContext`

## In Progress

_None._

## To Do

- [ ] Dedicated 0G megastructure-component recipes (titanium foam, etc.)
- [ ] Alternate assembler recipes with byproducts
- [ ] Ring/station/ship-specific recipe sets
- [ ] Recipes and tech unlocks for cloned buildings
- [ ] Unit tests

## Dependencies

| Kind | Packages |
|------|----------|
| BepInEx | — |
| Conceptual | OrbitalRings / OrbitalInfrastructure for orbital bonus; RecipeRebalance exotic parts |

## Known Issues

- Buildings registered but no recipe grid entries.
- Orbital detection mirrors OrbitalRings heuristics, not Apex proto IDs.

## Key Files

| File | Role |
|------|------|
| `Plugin.cs` | BepInEx entry point |
| `ZeroGProtoBootstrap.cs` | Building proto registration |
| `ZeroGProductionLogic.cs` | Yield bonus math |
| `ZeroGProductionPatch.cs` | Assembler yield hook |
| `ZeroGContext.cs` | Orbital structure detection |