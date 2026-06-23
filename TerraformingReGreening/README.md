# TerraformingReGreening

| Field | Value |
|-------|-------|
| **Suite #** | 12 |
| **Architecture** | [§12 TerraformingReGreening](../DSP%20Apex%20Architecture.md#12-terraformingregreening) |
| **Status** | Scaffold |
| **Progress** | ~14% |
| **Build** | PASS |
| **Tests** | — |
| **Plugin GUID** | `com.paulspooner.dsp.terraformingregreening` |
| **Version** | 0.1.0 |

## Vision

Expanded flora, harvestable organics, genetic engineering for harsh environments. Organic Dyson Spheres and bio-structures.

## Completed

- [x] Flora support check (ocean/desert planets)
- [x] Organic yield multiplier (1.4×, + genetic 1.25×)
- [x] Per-factory flora bonus tick

## In Progress

_None._

## To Do

- [ ] Expanded flora content
- [ ] Harvestable organics
- [ ] Genetic engineering gameplay
- [ ] Organic Dyson spheres / bio-structures
- [ ] Apply yield multiplier to actual harvesters/assemblers
- [ ] Unit tests

## Dependencies

| Kind | Packages |
|------|----------|
| BepInEx | — |
| Conceptual | RingworldsOrganic flora bonus; ThermalEffects planet specialization |

## Known Issues

- `AccumulatedOrganicBonus` computed but not patched into production.
- `GeneticEngineeringActive` is never set true in code.

## Key Files

| File | Role |
|------|------|
| `Plugin.cs` | BepInEx entry point |
| `TerraformingLogic.cs` | Flora and yield math |
| `TerraformingReGreeningRuntime.cs` | Per-factory state |
| `TerraformingReGreeningPatch.cs` | Factory tick hook |