# DysonSwarm2

| Field | Value |
|-------|-------|
| **Suite #** | 16 |
| **Architecture** | [§16 DysonSwarm2](../DSP%20Apex%20Architecture.md#16-dysonswarm2) |
| **Status** | Scaffold |
| **Progress** | ~14% |
| **Build** | PASS |
| **Tests** | — |
| **Plugin GUID** | `com.paulspooner.dsp.dysonswarm2` |
| **Version** | 0.1.0 |

## Vision

Expanded swarm types beyond vanilla power: collectors, construction, combat, and sensor swarms (and others as desired).

## Completed

- [x] `SwarmRole` enum (Power, Collector, Combat, Sensor)
- [x] Role assignment from orbit count
- [x] Role multipliers stored in runtime

## In Progress

_None._

## To Do

- [ ] Buildable expanded swarm types
- [ ] Collector/combat/sensor swarm gameplay effects
- [ ] Apply `RoleMultiplier` to actual swarm output/combat/sensing
- [ ] New swarm protos
- [ ] Unit tests

## Dependencies

| Kind | Packages |
|------|----------|
| BepInEx | — |
| Conceptual | OrbitalRings sensor swarms; DysonWeapons combat swarms |

## Known Issues

- `LastRoleMultiplier` never applied to `DysonSwarm` orbs.
- No new swarm entities — only classifies existing vanilla swarms.

## Key Files

| File | Role |
|------|------|
| `Plugin.cs` | BepInEx entry point |
| `DysonSwarm2Logic.cs` | Role assignment math |
| `DysonSwarm2Runtime.cs` | Role multiplier state |
| `DysonSwarm2Patch.cs` | Swarm tick hook |