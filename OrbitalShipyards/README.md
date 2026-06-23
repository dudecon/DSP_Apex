# OrbitalShipyards

| Field | Value |
|-------|-------|
| **Suite #** | 15 |
| **Architecture** | [§15 OrbitalShipyards](../DSP%20Apex%20Architecture.md#15-orbitalshipyards) |
| **Status** | Scaffold |
| **Progress** | ~14% |
| **Build** | PASS |
| **Tests** | — |
| **Plugin GUID** | `com.paulspooner.dsp.orbitalshipyards` |
| **Version** | 0.1.0 |

## Vision

Low-energy satellite launchers, large vessel repair/replication, and mass-production of entire sphere components instead of sending up hundreds of individual microsats.

## Completed

- [x] Rocket batch count on `RocketGameTick`
- [x] Rocket capacity bump with launch energy discount
- [x] Damaged rocket detection + repair throughput counter

## In Progress

_None._

## To Do

- [ ] Low-energy satellite launchers
- [ ] Large vessel repair/replication buildings
- [ ] Mass-production of sphere components (not individual microsats)
- [ ] Shipyard proto items and UI
- [ ] Unit tests

## Dependencies

| Kind | Packages |
|------|----------|
| BepInEx | — |
| Conceptual | Dyson sphere rocket pipeline; MegaStructuresUI ship modules |

## Known Issues

- `BatchedRockets` / `RepairedHull` are statistics only.
- `ShipyardLevel` hardcoded to 1, never upgraded.

## Key Files

| File | Role |
|------|------|
| `Plugin.cs` | BepInEx entry point |
| `OrbitalShipyardLogic.cs` | Batch and repair math |
| `OrbitalShipyardsRuntime.cs` | Shipyard counters |
| `OrbitalShipyardsPatch.cs` | Rocket tick hook |