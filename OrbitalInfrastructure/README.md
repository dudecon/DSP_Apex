# OrbitalInfrastructure

| Field | Value |
|-------|-------|
| **Suite #** | 3 |
| **Architecture** | [§3 OrbitalInfrastructure](../DSP%20Apex%20Architecture.md#3-orbitalinfrastructure) |
| **Status** | Partial |
| **Progress** | ~32% |
| **Build** | PASS |
| **Tests** | — |
| **Plugin GUID** | `com.paulspooner.dsp.orbitalinfrastructure` |
| **Version** | 0.1.0 |

## Vision

Space elevators, planetary satellite swarms, and modular orbital stations — connecting surface industry to orbit and enabling expandable habitats and factories.

## Completed

- [x] Proto registration: Space Elevator, Satellite Swarm, Orbital Station Module
- [x] Elevator throughput bump on `RefreshStationTraffic`
- [x] Station module energy cap scaling by slot count
- [x] Placement eligibility logic (`CanBuildSpaceElevator`)

## In Progress

_None._

## To Do

- [ ] Functional space elevator surface↔orbit logistics
- [ ] Planetary satellite swarm power, relay, and Dark Fog sensing
- [ ] Modular orbital stations with Dyson planning interface
- [ ] Repurpose vanilla logistics towers as elevator spine
- [ ] Build rules, recipes, and tech unlocks
- [ ] Unit tests

## Dependencies

| Kind | Packages |
|------|----------|
| BepInEx | — |
| Conceptual | OrbitalRings swarms; MegaStructuresUI station modules; HandmadeDyson placement |

## Known Issues

- Effect patch hardcodes elevator tower level 4.
- Cloned protos reuse vanilla OC/logistics prefabs without custom behavior.

## Key Files

| File | Role |
|------|------|
| `Plugin.cs` | BepInEx entry point |
| `InfrastructureProtoRegistry.cs` | Proto registration |
| `OrbitalInfrastructureLogic.cs` | Placement eligibility |
| `OrbitalInfrastructureEffectPatch.cs` | Throughput and energy patches |