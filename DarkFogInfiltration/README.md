# DarkFogInfiltration

| Field | Value |
|-------|-------|
| **Suite #** | 18 |
| **Architecture** | [§18 DarkFogInfiltration](../DSP%20Apex%20Architecture.md#18-darkfoginfiltration) |
| **Status** | Scaffold |
| **Progress** | ~10% |
| **Build** | PASS |
| **Tests** | — |
| **Plugin GUID** | `com.paulspooner.dsp.darkfoginfiltration` |
| **Version** | 0.1.0 |

## Vision

Use and research Dark Fog scraps to hide, hack, and convert enemy assets, units, and bases. Reciprocal risk: Fog can infiltrate player structures.

## Completed

- [x] Hourly infiltration progress simulation
- [x] Scrap level + conversion chance
- [x] Fog counter-risk / retaliation logic
- [x] Converted asset counter

## In Progress

_None._

## To Do

- [ ] Dark Fog scrap research
- [ ] Hide/hack/convert enemy units and bases
- [ ] Reciprocal fog infiltration of player structures
- [ ] Integration with Dark Fog API/assets
- [ ] Unit tests

## Dependencies

| Kind | Packages |
|------|----------|
| BepInEx | — |
| Conceptual | Requires Dark Fog DLC/mod |

## Known Issues

- No Dark Fog game types referenced — pure abstract simulation.
- `ScrapLevel` never incremented from actual scrap items.

## Key Files

| File | Role |
|------|------|
| `Plugin.cs` | BepInEx entry point |
| `DarkFogInfiltrationLogic.cs` | Infiltration and conversion math |
| `DarkFogInfiltrationRuntime.cs` | Progress counters |
| `DarkFogInfiltrationPatch.cs` | Hourly tick hook |