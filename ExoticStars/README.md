# ExoticStars

| Field | Value |
|-------|-------|
| **Suite #** | 9 |
| **Architecture** | [§9 ExoticStars](../DSP%20Apex%20Architecture.md#9-exoticstars) |
| **Status** | Partial |
| **Progress** | ~28% |
| **Build** | PASS |
| **Tests** | — |
| **Plugin GUID** | `com.paulspooner.dsp.exoticstars` |
| **Version** | 0.1.0 |

## Vision

New X-class stars with unique visuals and properties. Orbital Telescope arrays scan to reveal hidden exotics. Strong ties to Harvester yields, weapon cores, and relocation anchors.

## Completed

- [x] `ApexExoticStarClass` classification (WhiteHole, StrangeStar, MonopoleStar, etc.)
- [x] Auto-mark vanilla exotic stars on load
- [x] Telescope scan progress from Dyson shell/frame counts
- [x] Discovery threshold (8 panels + 100% progress)

## In Progress

_None._

## To Do

- [ ] Generate new X-class stars with unique visuals and properties
- [ ] Dedicated Orbital Telescope building/arrays
- [ ] KSP-style hidden exotic discovery in unvisited systems
- [ ] Cross-mod yield/weapon/mover bonuses wired to classifications
- [ ] Unit tests

## Dependencies

| Kind | Packages |
|------|----------|
| BepInEx | — |
| Conceptual | DysonHarvester yields; DysonWeapons cores; SystemMover anchors |

## Known Issues

- Classifies existing vanilla neutron/black-hole/giant stars only — no new star generation.
- Telescope counts all Dyson frames/shells, not telescope-specific modules.

## Key Files

| File | Role |
|------|------|
| `Plugin.cs` | BepInEx entry point |
| `ExoticStarLogic.cs` | Classification and discovery math |
| `ExoticStarsRuntime.cs` | Per-star runtime state |
| `ExoticStarsPatch.cs` | Star load and tick hooks |