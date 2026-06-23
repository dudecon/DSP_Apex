# SystemMover

| Field | Value |
|-------|-------|
| **Suite #** | 7 |
| **Architecture** | [§7 SystemMover](../DSP%20Apex%20Architecture.md#7-systemmover) |
| **Status** | Scaffold |
| **Progress** | ~15% |
| **Build** | PASS |
| **Tests** | — |
| **Plugin GUID** | `com.paulspooner.dsp.systemmover` |
| **Version** | 0.1.0 |

## Vision

Relocate entire star systems or individual planets using massive energy and Warpers. Black hole and exotic star bonuses; inner-shell warping for NG+ style play. Ultimate capstone feature.

## Completed

- [x] Star registration on `StarData.Load`
- [x] Relocation cost formula (mass × base, 0.25× for black holes)
- [x] Anchor efficiency by star type
- [x] `CanRelocate` warper threshold check (logic only)

## In Progress

_None._

## To Do

- [ ] Actual system/planet relocation mechanic
- [ ] Warper and energy consumption
- [ ] Black hole inner-shell warping
- [ ] NG+ cluster transfer
- [ ] Player UI and command
- [ ] Unit tests

## Dependencies

| Kind | Packages |
|------|----------|
| BepInEx | — |
| Conceptual | ExoticStars black-hole anchors; DysonHarvester energy |

## Known Issues

- No patch applies relocation — costs stored in dictionary only.
- `CanRelocate` is never called from game patches.

## Key Files

| File | Role |
|------|------|
| `Plugin.cs` | BepInEx entry point |
| `SystemMoverLogic.cs` | Cost and eligibility formulas |
| `SystemMoverRuntime.cs` | Star registration state |
| `SystemMoverPatch.cs` | Star load hook |