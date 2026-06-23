# QuantumLogistics

| Field | Value |
|-------|-------|
| **Suite #** | 17 |
| **Architecture** | [§17 QuantumLogistics](../DSP%20Apex%20Architecture.md#17-quantumlogistics) |
| **Status** | Partial |
| **Progress** | ~25% |
| **Build** | PASS |
| **Tests** | 3 ✓ (`QuantumLogistics.Tests`) |
| **Plugin GUID** | `com.paulspooner.dsp.quantumlogistics` |
| **Version** | 0.1.0 |

## Vision

Resource teleporters (instant intra-system between towers with Warper cost). New interstellar Stargates built on orbital megastructures only.

## Completed

- [x] Logistics tower storage detection (planetary + interstellar protos)
- [x] Warper cost calculation on `StorageComponent.AddItem` prefix
- [x] Stargate eligibility from Dyson frame/shell module count ≥ 8
- [x] Stargate transfer cost accounting
- [x] 3 unit tests

## In Progress

_None._

## To Do

- [ ] Actual instant intra-system teleport between towers
- [ ] Interstellar stargate buildings (orbital-only)
- [ ] Warper consumption from player inventory
- [ ] Stargate UI and destination selection

## Dependencies

| Kind | Packages |
|------|----------|
| BepInEx | — |
| Conceptual | Orbital megastructures for stargates; warper economy |

## Known Issues

- Teleport only bumps `inc` field — items are not relocated.
- Stargate registers when modules are sufficient but no gate entity exists.

## Key Files

| File | Role |
|------|------|
| `Plugin.cs` | BepInEx entry point |
| `QuantumLogisticsLogic.cs` | Warper cost and eligibility |
| `QuantumLogisticsRuntime.cs` | Transfer counters |
| `QuantumLogisticsContext.cs` | Tower detection |
| `QuantumLogisticsPatch.cs` | Storage hook |