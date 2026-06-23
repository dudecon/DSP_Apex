# DysonWeapons

| Field | Value |
|-------|-------|
| **Suite #** | 6 |
| **Architecture** | [§6 DysonWeapons](../DSP%20Apex%20Architecture.md#6-dysonweapons) |
| **Status** | Partial |
| **Progress** | ~30% |
| **Build** | PASS |
| **Tests** | 3 ✓ (`DysonWeapons.Tests`) |
| **Plugin GUID** | `com.paulspooner.dsp.dysonweapons` |
| **Version** | 0.1.0 |

## Vision

Outer sphere rings convertible to massive particle accelerators. Transmutation, orbital particle beams, planet destruction at high tier, and exotic star Death Star cores.

## Completed

- [x] Outer polar complete-frame detection (`IsOuterPolarFrame`)
- [x] Beam charge accumulation per weapon frame (runtime counter)
- [x] H→D transmutation tick from sphere power + product register
- [x] Pure testable formulas (`DysonWeaponsLogic.Pure.cs`)
- [x] 3 unit tests

## In Progress

_None._

## To Do

- [ ] Particle accelerator conversion UI and mechanic
- [ ] Stone/soil→ore transmutation at higher efficiency
- [ ] Orbital particle beams (anti-Dark Fog, interstellar)
- [ ] Planet harvesting / evaporation / destruction
- [ ] Exotic star Death Star cores
- [ ] Actual beam combat and damage application

## Dependencies

| Kind | Packages |
|------|----------|
| BepInEx | — |
| Conceptual | DysonHarvester H/D feed; ExoticStars weapon cores; MegaStructuresUI weapon frames |

## Known Issues

- `BeamCharge` / `TransmutedDeuterium` are counters only — no combat integration.
- Transmutation reads hydrogen from product register, not live harvester pipeline.

## Key Files

| File | Role |
|------|------|
| `Plugin.cs` | BepInEx entry point |
| `DysonWeaponsLogic.cs` | Frame detection and weapon math |
| `DysonWeaponsLogic.Pure.cs` | Pure testable formulas |
| `DysonWeaponsRuntime.cs` | Runtime counters |
| `DysonWeaponsPatch.cs` | Game tick hook |