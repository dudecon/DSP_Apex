# RamscoopShips

| Field | Value |
|-------|-------|
| **Suite #** | 19 |
| **Architecture** | [§19 RamscoopShips](../DSP%20Apex%20Architecture.md#19-ramscoopships) |
| **Status** | Scaffold |
| **Progress** | ~14% |
| **Build** | PASS |
| **Tests** | — |
| **Plugin GUID** | `com.paulspooner.dsp.ramscoopships` |
| **Version** | 0.1.0 |

## Vision

Trade travel speed for increased harvest quantity during transit.

## Completed

- [x] Transit harvest tick every 300 game ticks
- [x] Speed penalty (0.6) vs harvest bonus (2.5×) tradeoff math
- [x] Accumulated scoop yield counter
- [x] Effective speed calculation

## In Progress

_None._

## To Do

- [ ] Ramscoop ship proto/building
- [ ] Apply speed penalty to actual logistics ships
- [ ] Deposit scooped resources to stations
- [ ] Player-configurable scoop modes
- [ ] Unit tests

## Dependencies

| Kind | Packages |
|------|----------|
| BepInEx | — |
| Conceptual | DysonHarvester transit harvesting; logistics ship pipeline |

## Known Issues

- Uses fixed `baseYield=10` and `baseSpeed=100` — not tied to real ships.
- `AccumulatedScoopYield` never deposited anywhere.

## Key Files

| File | Role |
|------|------|
| `Plugin.cs` | BepInEx entry point |
| `RamscoopLogic.cs` | Speed/harvest tradeoff math |
| `RamscoopShipsRuntime.cs` | Scoop counters |
| `RamscoopShipsPatch.cs` | Transit tick hook |