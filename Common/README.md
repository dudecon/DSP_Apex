# Common

| Field | Value |
|-------|-------|
| **Suite #** | — (shared infrastructure) |
| **Architecture** | [Cross-Cutting Features](../DSP%20Apex%20Architecture.md#cross-cutting-features--design-principles) |
| **Status** | Not Started |
| **Progress** | 0% |
| **Build** | N/A (no project) |
| **Tests** | — |

## Vision

Shared code, attributes, utilities, and Harmony patches for the suite. Intended to eliminate duplicated bootstrap and proto-registration boilerplate across packs.

## Completed

- [x] README placeholder

## In Progress

_None._

## To Do

- [ ] Create `Common.csproj` and extract shared `HarmonyBootstrap`
- [ ] Shared proto registration utilities (overlap with OrbitalRings, OrbitalInfrastructure, ZeroGProduction)
- [ ] Shared Apex ID registry helpers
- [ ] Common test helpers and DSP game adapters
- [ ] Migrate all packs to reference Common

## Dependencies

| Kind | Packages |
|------|----------|
| BepInEx | — |
| Project | All suite packs (future consumers) |

## Known Issues

- Each mod reimplements identical `HarmonyBootstrap` and plugin boilerplate.
- No `.cs` files or project exist yet.

## Key Files

| File | Role |
|------|------|
| `README.md` | This file |