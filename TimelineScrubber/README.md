# TimelineScrubber

| Field | Value |
|-------|-------|
| **Suite #** | 10 |
| **Architecture** | [§10 TimelineScrubber](../DSP%20Apex%20Architecture.md#10-timelinescrubber) |
| **Status** | Partial |
| **Progress** | ~35% |
| **Build** | PASS |
| **Tests** | 4 ✓ (`TimelineScrubber.Tests`) |
| **Plugin GUID** | `com.paulspooner.dsp.timelinescrubber` |
| **Version** | 0.1.0 |

## Vision

Timelapse mode with fast-forward, interpolation/extrapolation. Save games as nodes/branch points with limited rewind. Statistics-view-driven extrapolation.

## Completed

- [x] BepInEx `EnableFastForward` config
- [x] `DysonSphere` tick fast-forward prefix (4× scale)
- [x] Branch tick recording every 216000 game ticks
- [x] Production extrapolation/interpolation logic
- [x] Max branch depth = 3
- [x] 4 unit tests

## In Progress

_None._

## To Do

- [ ] Save-game branch nodes
- [ ] Limited rewind mechanic
- [ ] Statistics-view-driven extrapolation
- [ ] Player UI for timelapse control
- [ ] Full-game tick fast-forward (currently Dyson-only prefix)

## Dependencies

| Kind | Packages |
|------|----------|
| BepInEx | — |
| Conceptual | Statistics view for extrapolation (not integrated) |

## Known Issues

- Fast-forward only patches `DysonSphere.BeforeGameTick`, not global `GameData` tick.
- `BranchTicks` recorded but no restore/rewind implementation.

## Key Files

| File | Role |
|------|------|
| `Plugin.cs` | BepInEx entry point |
| `TimelineScrubberConfig.cs` | BepInEx config |
| `TimelineScrubberLogic.cs` | Extrapolation math |
| `TimelineScrubberRuntime.cs` | Branch state |
| `TimelineScrubberPatch.cs` | Fast-forward hook |