# HandmadeDyson

| Field | Value |
|-------|-------|
| **Suite #** | 8 |
| **Architecture** | [§8 HandmadeDyson](../DSP%20Apex%20Architecture.md#8-handmadedyson) |
| **Status** | Scaffold |
| **Progress** | ~18% |
| **Build** | PASS |
| **Tests** | — |
| **Plugin GUID** | `com.paulspooner.dsp.handmadedyson` |
| **Version** | 0.1.0 |

## Vision

Manual construction with Icarus/mech of orbital megastructures laid out in the Dyson Sphere blueprint. Land and build directly on rings, stations, factory ships, and accelerator segments.

## Completed

- [x] Build mode open/close tracking (`EnableOrbitalBuild`)
- [x] Build preview forced valid for OC protos 2212/2213
- [x] Build range bonus constant (1.5×, defined but not applied)

## In Progress

_None._

## To Do

- [ ] Land-and-build on orbital megastructures, rings, stations, ships, accelerators
- [ ] Cruise-mode navigation on structures
- [ ] Integration with suite-specific protos (not just vanilla OC)
- [ ] Actual placement on non-planet surfaces
- [ ] Apply `BuildRangeBonus` in patches
- [ ] Unit tests

## Dependencies

| Kind | Packages |
|------|----------|
| BepInEx | — |
| Conceptual | OrbitalRings, OrbitalInfrastructure, DysonWeapons frames, OrbitalShipyards |

## Known Issues

- Only sets `cursorValid` — does not change placement surface or collision.
- `BuildRangeBonus` is defined but unused in patches.
- Hardcoded proto IDs (2212/2213).

## Key Files

| File | Role |
|------|------|
| `Plugin.cs` | BepInEx entry point |
| `HandmadeDysonLogic.cs` | Build validity rules |
| `HandmadeDysonRuntime.cs` | Build mode state |
| `HandmadeDysonPatch.cs` | Build preview hook |