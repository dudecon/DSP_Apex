# FaunaNomads

| Field | Value |
|-------|-------|
| **Suite #** | 13 |
| **Architecture** | [§13 FaunaNomads](../DSP%20Apex%20Architecture.md#13-faunonomads) |
| **Status** | Scaffold |
| **Progress** | ~15% |
| **Build** | PASS |
| **Tests** | 2 ✓ (`FaunaNomads.Tests`) |
| **Plugin GUID** | `com.paulspooner.dsp.faunonomads` |
| **Version** | 0.1.0 |

## Vision

Robot animals, space fauna, and a nomadic herder playstyle — a mobile analog to sessile factories.

## Completed

- [x] Resource deficit scan (iron/copper, target 800)
- [x] Agent/herd scaling from deficit
- [x] Nomad harvest rate and range formulas
- [x] 600-tick `DysonSphere` sync loop
- [x] 2 unit tests

## In Progress

_None._

## To Do

- [ ] Robot animal entities
- [ ] Space fauna
- [ ] Nomadic herder playstyle with actual movement and harvest
- [ ] Visuals and player control

## Dependencies

| Kind | Packages |
|------|----------|
| BepInEx | — |
| Conceptual | Mobile alternative to sessile factories; SelfPropagation agent model |

## Known Issues

- Agents/herds are counters only — no entities or world interaction.
- `HarvestRate` / `NomadRange` never applied to game systems.

## Key Files

| File | Role |
|------|------|
| `Plugin.cs` | BepInEx entry point |
| `FaunaNomadLogic.cs` | Deficit and harvest math |
| `FaunaNomadsRuntime.cs` | Agent/herd counters |
| `FaunaNomadsContext.cs` | Game context helpers |
| `FaunaNomadsPatch.cs` | Sync loop hook |