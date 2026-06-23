# DysonHarvester

| Field | Value |
|-------|-------|
| **Suite #** | 1 |
| **Architecture** | [§1 DysonHarvester](../DSP%20Apex%20Architecture.md#1-dysonharvester) |
| **Status** | Mature |
| **Progress** | ~68% |
| **Build** | PASS |
| **Tests** | 18 ✓ (`DysonHarvester.Tests`) |
| **Plugin GUID** | `com.paulspooner.dsp.dysonharvester` |
| **Version** | 0.1.0 |

## Vision

Dyson Sphere nodes act as advanced orbital collectors. Only the innermost completed layer actively harvests power and resources; yields vary by star class and luminosity.

## Completed

- [x] `DysonSphere.GameTick` harvest pulse every 60 ticks
- [x] Innermost completed layer-only harvesting
- [x] Per-node yield by star class + luminosity (H, D, Fire Ice, Strange Matter, Unipolar Magnet)
- [x] Node progress tracking via `NodeCollectorRegistry`
- [x] Deposit routing: orbital collectors → logistics stations → player package
- [x] Product register statistics update
- [x] `DysonNode` progress patch keeps registry in sync
- [x] 18 unit tests (yield, deposit, routing, service)

## In Progress

_None._

## To Do

- [ ] BepInEx config for yields and star modifiers
- [ ] Proliferation support
- [ ] ExoticStars X-class star yield integration
- [ ] Dedicated collector UI / feedback
- [ ] In-game integration tests

## Dependencies

| Kind | Packages |
|------|----------|
| BepInEx | — |
| Conceptual | Feeds DysonWeapons transmutation; synergizes with ExoticStars |

## Known Issues

- Deposit routing uses heuristic station flags, not Dyson-node-specific collectors.
- Exotic yields keyed to vanilla star types only, not ExoticStars classifications.

## Key Files

| File | Role |
|------|------|
| `Plugin.cs` | BepInEx entry point |
| `HarvesterService.cs` | Tick orchestration |
| `HarvesterYieldLogic.cs` | Pure yield rules |
| `HarvesterDepositService.cs` | Deposit routing |
| `HarvesterGameAdapter.cs` | Innermost layer detection |
| `DysonSphereHarvestPatch.cs` | Game tick hook |