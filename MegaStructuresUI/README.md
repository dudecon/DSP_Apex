# MegaStructuresUI

| Field | Value |
|-------|-------|
| **Suite #** | 5 |
| **Architecture** | [§5 MegaStructuresUI](../DSP%20Apex%20Architecture.md#5-megastructuresui) |
| **Status** | Substantial |
| **Progress** | ~52% |
| **Build** | PASS |
| **Tests** | 4 ✓ (`MegaStructuresUI.Tests`) |
| **Plugin GUID** | `com.paulspooner.dsp.megastructuresui` |
| **Version** | 0.1.0 |

## Vision

Sphere Production Panel / Orbital Command Interface for allocating modules across rings, stations, ships, and sphere sections. General UI hub serving the other mods in the suite.

## Completed

- [x] 7th replicator tab (Apex) with custom icons, labels, and clicks
- [x] `RecipePickerApexBridge` to mod recipes
- [x] Orbital Command panel in `UIDysonEditor` (runtime UI with +/- module buttons)
- [x] `ModuleAllocation` state per star (ring/station/ship/weapon/production/section)
- [x] Computed stats: power, harvest, transmutation, beam damage
- [x] `UIReplicator` safety patch
- [x] 4 unit tests (stats + state)

## In Progress

_None._

## To Do

- [ ] Procedural external visuals from internal allocation
- [ ] Enclosed orbital module blueprints snapping into megastructures
- [ ] Abstracted large-scale simulation
- [ ] Real-time cross-mod stats (actual harvest/beam data, not formula-only)
- [ ] Zoom-in factory detail

## Dependencies

| Kind | Packages |
|------|----------|
| BepInEx | RecipeRebalance (soft) |
| Project | RecipeRebalance |
| Conceptual | UI hub for entire suite |

## Known Issues

- Orbital Command allocation is abstract counters — not synced to real world structures.
- Harvest/power stats blend computed values with rough sphere estimates.

## Key Files

| File | Role |
|------|------|
| `Plugin.cs` | BepInEx entry point |
| `ReplicatorTabInstaller.cs` | Apex tab UI |
| `OrbitalCommandInstaller.cs` | Dyson editor panel |
| `OrbitalCommandState.cs` | Per-star allocation state |
| `OrbitalCommandStatsLogic.cs` | Stats computation |
| `RecipePickerApexBridge.cs` | Recipe picker integration |