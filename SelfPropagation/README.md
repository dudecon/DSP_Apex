# SelfPropagation

| Field | Value |
|-------|-------|
| **Suite #** | 11 |
| **Architecture** | [§11 SelfPropagation](../DSP%20Apex%20Architecture.md#11-selfpropagation) |
| **Status** | Scaffold |
| **Progress** | ~16% |
| **Build** | PASS |
| **Tests** | 3 ✓ (`SelfPropagation.Tests`) |
| **Plugin GUID** | `com.paulspooner.dsp.selfpropagation` |
| **Version** | 0.1.0 |

## Vision

Autonomous sub-agents that independently build infrastructure and expand the empire on multiple fronts. Automatic blueprint generation to increase production of resources in demand.

## Completed

- [x] Hourly deficit scan (iron/copper vs target 1000)
- [x] Agent count expansion up to `MaxAgents=16` based on total deficit
- [x] Blueprint priority/demand pure logic
- [x] 3 unit tests

## In Progress

_None._

## To Do

- [ ] Autonomous sub-agents that build infrastructure
- [ ] Automatic blueprint generation from demand
- [ ] Multi-front empire expansion
- [ ] Integration with actual build system and blueprints

## Dependencies

| Kind | Packages |
|------|----------|
| BepInEx | — |
| Conceptual | Complements MegaStructuresUI automation |

## Known Issues

- `ActiveAgents` increments but no agents exist in-game.
- Hardcoded iron/copper IDs (1001/1002) and targets.

## Key Files

| File | Role |
|------|------|
| `Plugin.cs` | BepInEx entry point |
| `SelfPropagationLogic.cs` | Deficit and priority math |
| `SelfPropagationRuntime.cs` | Agent counters |
| `SelfPropagationPatch.cs` | Hourly tick hook |