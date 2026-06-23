# SubterraneanConstruction

| Field | Value |
|-------|-------|
| **Suite #** | 14 |
| **Architecture** | [§14 SubterraneanConstruction](../DSP%20Apex%20Architecture.md#14-subterraneanconstruction) |
| **Status** | Partial |
| **Progress** | ~26% |
| **Build** | PASS |
| **Tests** | 2 ✓ (`SubterraneanConstruction.Tests`) |
| **Plugin GUID** | `com.paulspooner.dsp.subterraneanconstruction` |
| **Version** | 0.1.0 |

## Vision

Deep mantle/core mines and true layered ecumenopolis architecture.

## Completed

- [x] Mantle depth / city layer tracking from entity count
- [x] Miner yield bonus via `ApplyLayeredYield`
- [x] Pure layered-mine efficiency math
- [x] 2 unit tests

## In Progress

_None._

## To Do

- [ ] Deep mantle/core mine buildings
- [ ] True layered ecumenopolis architecture (vertical city layers)
- [ ] Underground construction UI and rules
- [ ] Integration with planet terrain
- [ ] Per-planet layer state (currently global)

## Dependencies

| Kind | Packages |
|------|----------|
| BepInEx | — |
| Conceptual | ThermalEffects waste heat; layered ecumenopolis theme |

## Known Issues

- `CityLayers` derived from `entityCount/100` heuristic — not real underground layers.
- Global static `MantleDepth` / `CityLayers` — not per-planet.

## Key Files

| File | Role |
|------|------|
| `Plugin.cs` | BepInEx entry point |
| `SubterraneanLogic.cs` | Layer and yield math |
| `SubterraneanConstructionRuntime.cs` | Depth/layer state |
| `SubterraneanConstructionPatch.cs` | Miner yield hook |