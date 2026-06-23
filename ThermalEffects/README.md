# ThermalEffects

| Field | Value |
|-------|-------|
| **Suite #** | 21 |
| **Architecture** | [§21 ThermalEffects](../DSP%20Apex%20Architecture.md#21-thermaleffects) |
| **Status** | Partial |
| **Progress** | ~32% |
| **Build** | PASS |
| **Tests** | 3 ✓ (`ThermalEffects.Tests`) |
| **Plugin GUID** | `com.paulspooner.dsp.thermaleffects` |
| **Version** | 0.1.0 |

## Vision

Environmental specialization: lava planets reduce refining energy costs but excess heat slows assemblers; cold planets have converse effects. Waste heat accumulation with synergies across the suite.

## Completed

- [x] Planet temperature resolution for assemblers (`ThermalEffectsContext`)
- [x] Hot/cold assembler speed factors + waste-heat slowdown
- [x] Hot planet refinery energy reduction on miners
- [x] Waste heat accumulation/dissipation per tick
- [x] 3 unit tests

## In Progress

_None._

## To Do

- [ ] Lava/cold planet type specialization (not just temperature thresholds)
- [ ] Cold planet assembler bonus (currently only 0.95× penalty)
- [ ] Cross-mod synergies (Harvester, Weapons heat, Terraforming, Subterranean)
- [ ] Per-planet heat state (currently global `PlanetWasteHeat`)
- [ ] BepInEx config for thermal parameters

## Dependencies

| Kind | Packages |
|------|----------|
| BepInEx | — |
| Conceptual | Harvester (1), Weapons (6), Terraforming (12), Subterranean (14) — not wired |

## Known Issues

- `PlanetWasteHeat` is a single global float.
- Defaults to 300K when temperature unresolved — masks specialization on many planets.

## Key Files

| File | Role |
|------|------|
| `Plugin.cs` | BepInEx entry point |
| `ThermalEffectsLogic.cs` | Speed and energy factors |
| `ThermalEffectsRuntime.cs` | Waste heat state |
| `ThermalEffectsContext.cs` | Planet temperature resolution |
| `ThermalEffectsPatch.cs` | Assembler and miner hooks |