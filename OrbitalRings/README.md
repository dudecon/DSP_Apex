# OrbitalRings

| Field | Value |
|-------|-------|
| **Suite #** | 2 |
| **Architecture** | [§2 OrbitalRings](../DSP%20Apex%20Architecture.md#2-orbitalrings) |
| **Status** | Substantial |
| **Progress** | ~58% |
| **Build** | PASS |
| **Tests** | 8 ✓ (`OrbitalRings.Tests`) |
| **Plugin GUID** | `com.paulspooner.dsp.orbitalrings` |
| **Version** | 0.1.0 |

## Vision

Buildable swarms, rings, and shells around planets. Rocky planets get power relays and missile sensor networks; gas giants get enhanced atmospheric harvesting.

## Completed

- [x] Proto registration: Apex Orbital Ring Frame + Satellite Swarm Node
- [x] Per-planet ring/swarm counting (`OrbitalRingCounter`)
- [x] Rocky planet ray-receiver relay bonus (`PowerRelayPatch`)
- [x] Gas giant OC harvest bonus (`StationCollectionPatch`)
- [x] Missile sensor coverage / search-range bonus (`SensorCoveragePatch`)
- [x] Runtime state per planet updated each factory tick
- [x] 8 unit tests (logic + consumer)

## In Progress

_None._

## To Do

- [ ] True buildable planetary rings/shells with struts and frames
- [ ] Dyson-style multi-layer shells around planets
- [ ] Module integration with OrbitalInfrastructure stations
- [ ] HandmadeDyson manual ring construction
- [ ] BepInEx config for bonuses
- [ ] Wire registered Apex protos to custom placement/build rules

## Dependencies

| Kind | Packages |
|------|----------|
| BepInEx | — |
| Conceptual | ZeroGProduction orbital bonus; MegaStructuresUI ring module stats |

## Known Issues

- Ring/swarm detection counts vanilla gamma gens, stellar stations, and collectors — not only Apex protos.
- Registered Apex items are not wired to custom placement or build rules.

## Key Files

| File | Role |
|------|------|
| `Plugin.cs` | BepInEx entry point |
| `OrbitalRingLogic.cs` | Pure relay/harvest/sensor math |
| `OrbitalRingCounter.cs` | Per-factory ring/swarm counting |
| `OrbitalRingProtoRegistry.cs` | Proto registration |
| `PowerRelayPatch.cs` | Ray receiver relay bonus |
| `SensorCoveragePatch.cs` | Missile search bonus |
| `StationCollectionPatch.cs` | Gas giant harvest bonus |