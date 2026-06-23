# DSP_Apex

**Dyson Apex Mod Suite**

A modular suite of mods for *Dyson Sphere Program* that extends planetary industry into a full orbital civilization layer — from space elevators and rings to Dyson-scale weapons, exotics, and system manipulation.

## Key Resources

* [**Comprehensive Design Document**](DSP%20Apex%20Architecture.md) — Vision, features, synergies, and design principles (no implementation status).
* [LICENSE](LICENSE) — Public domain dedication per Paul Spooner's IP policy (ip.tryop.com).

## Philosophy

Inspired by [f.tryop.com](http://f.tryop.com): Deep simulation of interconnecting procedurally generated systems enables emergent play across vast scales — from personal action to galactic engineering. The emphasis is on **indirect control**: automating routine complexity through sub-routines so players excel at direction, integration, and pattern-matching against the larger whole. Fractally nested systems, time contraction, and moddability support believable, player-mutable worlds without losing hands-on satisfaction.

The Dyson Apex suite applies this vision to *Dyson Sphere Program* through progressive scale escalation (surface → orbital → stellar → system), manual construction paired with abstracted high-level management, realistic mechanics (zero-G advantages, thermal effects), procedural visuals, and performance-friendly abstraction so the Dyson Sphere becomes a living, player-directed platform.

---

## Suite Progress Dashboard

When you finish work in a pack, update its Completed / To Do sections and bump Progress in both the pack README and the root dashboard table below.

**Last updated:** 2026-06-23  
**Overall progress:** ~33% (average of pack progress; excludes `Common`)  
**Build:** All 22 mod packages compile  
**Tests:** 50 / 50 passing across 10 test projects (logic-only; no in-game integration tests)

### Status Legend

| Status | Meaning |
|--------|---------|
| **Not Started** | Placeholder only; no implementation |
| **Scaffold** | Plugin + tick hooks + formulas; little or no player-facing gameplay |
| **Partial** | Some real game hooks; major vision features missing |
| **Substantial** | Core mechanics or content in place; significant gaps remain |
| **Mature** | End-to-end feature loop with tests; polish and extensions still possible |

### Core Mods (0–9)

| # | Pack | Status | Progress | Tests | README |
|---|------|--------|----------|-------|--------|
| 0 | RecipeRebalance | Substantial | ~72% | — | [README](RecipeRebalance/README.md) |
| 1 | DysonHarvester | Mature | ~68% | 18 ✓ | [README](DysonHarvester/README.md) |
| 2 | OrbitalRings | Substantial | ~58% | 8 ✓ | [README](OrbitalRings/README.md) |
| 3 | OrbitalInfrastructure | Partial | ~32% | — | [README](OrbitalInfrastructure/README.md) |
| 4 | ZeroGProduction | Partial | ~38% | — | [README](ZeroGProduction/README.md) |
| 5 | MegaStructuresUI | Substantial | ~52% | 4 ✓ | [README](MegaStructuresUI/README.md) |
| 6 | DysonWeapons | Partial | ~30% | 3 ✓ | [README](DysonWeapons/README.md) |
| 7 | SystemMover | Scaffold | ~15% | — | [README](SystemMover/README.md) |
| 8 | HandmadeDyson | Scaffold | ~18% | — | [README](HandmadeDyson/README.md) |
| 9 | ExoticStars | Partial | ~28% | — | [README](ExoticStars/README.md) |

### Peripheral Mods (10–21)

| # | Pack | Status | Progress | Tests | README |
|---|------|--------|----------|-------|--------|
| 10 | TimelineScrubber | Partial | ~35% | 4 ✓ | [README](TimelineScrubber/README.md) |
| 11 | SelfPropagation | Scaffold | ~16% | 3 ✓ | [README](SelfPropagation/README.md) |
| 12 | TerraformingReGreening | Scaffold | ~14% | — | [README](TerraformingReGreening/README.md) |
| 13 | FaunaNomads | Scaffold | ~15% | 2 ✓ | [README](FaunaNomads/README.md) |
| 14 | SubterraneanConstruction | Partial | ~26% | 2 ✓ | [README](SubterraneanConstruction/README.md) |
| 15 | OrbitalShipyards | Scaffold | ~14% | — | [README](OrbitalShipyards/README.md) |
| 16 | DysonSwarm2 | Scaffold | ~14% | — | [README](DysonSwarm2/README.md) |
| 17 | QuantumLogistics | Partial | ~25% | 3 ✓ | [README](QuantumLogistics/README.md) |
| 18 | DarkFogInfiltration | Scaffold | ~10% | — | [README](DarkFogInfiltration/README.md) |
| 19 | RamscoopShips | Scaffold | ~14% | — | [README](RamscoopShips/README.md) |
| 20 | RingworldsOrganic | Scaffold | ~12% | — | [README](RingworldsOrganic/README.md) |
| 21 | ThermalEffects | Partial | ~32% | 3 ✓ | [README](ThermalEffects/README.md) |

### Shared Infrastructure

| Pack | Status | Progress | README |
|------|--------|----------|--------|
| Common | Not Started | 0% | [README](Common/README.md) |

---

## Implementation Roadmap

Recommended build order (moved from the architecture document):

1. **Common** — extract shared Harmony bootstrap, proto registry, game adapters, and test helpers.
2. **DysonHarvester** — foundational harvesting loop (largely complete).
3. **OrbitalRings + HandmadeDyson** — real ring entities and manual orbital build placement.
4. **MegaStructuresUI** — connect Orbital Command allocation to real mod simulation hooks.
5. **RecipeRebalance tests** — lock fusion chains and grid registration.
6. **ZeroGProduction + OrbitalInfrastructure** — custom recipes and elevator/station gameplay.
7. **DysonWeapons + ExoticStars** — after construction loop exists.
8. **SystemMover** — capstone relocation (last).

**Engineering practices:** BepInEx + Harmony; reuse vanilla assets; separate mod packages; test iteratively; maintain compatibility with GalacticScale, Dark Fog, and similar mods.

---

## Cross-Mod Dependencies

| From | To | Type |
|------|-----|------|
| MegaStructuresUI | RecipeRebalance | Soft `BepInDependency` + `ProjectReference` |

All other synergies described in the architecture document (Harvester ↔ Weapons ↔ Exotics ↔ Mover ↔ Thermal, etc.) are **conceptual only** — not wired in code yet.

---

## Suite-Wide Gaps

- **Common library missing** — `HarmonyBootstrap` and proto-clone patterns duplicated across every pack.
- **Megastructure construction loop incomplete** — rings, manual placement, and UI allocation are not connected end-to-end.
- **Proxy heuristics** — ring counts, telescope strength, and orbital context often inferred from vanilla structures (stations, gamma generators) rather than dedicated entities.
- **No integration tests** — all 50 tests are pure logic; Harmony patches are not validated in-game.
- **RecipeRebalance untested** — largest content mod has no test project.
- **No config surface** — architecture calls for yield/cost/realism configs; none implemented suite-wide.
- **Hardcoded IDs** — proto IDs, item registers, and deficit-scan targets are fragile across game versions.

---

## Building & Testing

- Install BepInEx before testing mods (see [references/README.md](references/README.md)).
- Each mod's `.csproj` has a `DSPPluginsPath` for post-build copy; point it at your game install (see `local.example.md`).
- There is no solution file; each pack builds standalone: `dotnet build <Pack>/<Pack>.csproj`
- Run tests: `dotnet test <Pack>.Tests/<Pack>.Tests.csproj`