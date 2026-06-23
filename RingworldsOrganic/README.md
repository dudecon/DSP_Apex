# RingworldsOrganic

| Field | Value |
|-------|-------|
| **Suite #** | 20 |
| **Architecture** | [§20 RingworldsOrganic](../DSP%20Apex%20Architecture.md#20-ringworldsorganic) |
| **Status** | Scaffold |
| **Progress** | ~12% |
| **Build** | PASS |
| **Tests** | — |
| **Plugin GUID** | `com.paulspooner.dsp.ringworldsorganic` |
| **Version** | 0.1.0 |

## Vision

Ringworld-style structures with strong synergy to flora/organic mods. Organic Dyson Spheres focused on biomass maximization.

## Completed

- [x] Organic ring threshold (≥12 frames)
- [x] Biomass multiplier/yield formulas
- [x] Per-factory biomass tick (entity count as segment proxy)

## In Progress

_None._

## To Do

- [ ] Ringworld structure building
- [ ] Organic Dyson sphere biomass maximization
- [ ] Synergy hooks to TerraformingReGreening (currently hardcoded `floraBonus` 1.4)
- [ ] Biomass as harvestable resource
- [ ] Unit tests

## Dependencies

| Kind | Packages |
|------|----------|
| BepInEx | — |
| Conceptual | TerraformingReGreening flora; organic Dyson theme |

## Known Issues

- `SegmentCount` = `factory.entityCount` — not ringworld frames.
- `BiomassLevel` not connected to production or items.

## Key Files

| File | Role |
|------|------|
| `Plugin.cs` | BepInEx entry point |
| `RingworldOrganicLogic.cs` | Biomass and yield math |
| `RingworldsOrganicRuntime.cs` | Biomass state |
| `RingworldsOrganicPatch.cs` | Factory tick hook |