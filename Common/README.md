# Common

| Field | Value |
|-------|-------|
| **Suite #** | — (shared infrastructure) |
| **Architecture** | [Cross-Cutting Features](../DSP%20Apex%20Architecture.md#cross-cutting-features--design-principles) |
| **Status** | Substantial |
| **Progress** | ~45% |
| **Build** | PASS |
| **Tests** | Exercised via `RecipeRebalance.Tests` (ProtoIdAllocator, ModTestHelpers) |

## Vision

Shared code, attributes, utilities, and Harmony patches for the suite.

## Completed

- [x] `Common.csproj` (`DspApex.Common`)
- [x] `HarmonyBootstrap` — shared `HasHarmonyPatches` (single definition)
- [x] `ProtoIdAllocator` — pure ID sequencing
- [x] `ProtoGameAdapter` — proto ID extraction from game `Proto[]` arrays
- [x] `ProtoRegistry` — shared LDB item/recipe append helpers
- [x] `ModTestHelpers` — contiguous ID and grid round-trip assertions
- [x] `DspApex.Common.targets` — copies `DspApex.Common.dll` to each mod's `DSPPluginsPath` on build

## In Progress

- [ ] Migrate OrbitalRings / OrbitalInfrastructure local `AppendItem` to `ProtoRegistry.AppendItem`

## To Do

- [ ] Shared Apex ID registry helpers across packs
- [ ] Additional game adapters (planet factory context, etc.)
- [ ] Dedicated `Common.Tests` project (optional; helpers currently tested via consumer packs)

## Dependencies

| Kind | Packages |
|------|----------|
| References | Assembly-CSharp, BepInEx, 0Harmony |
| Consumers | All 22 mod packs via `ProjectReference` + `DspApex.Common.targets` |

## Key Files

| File | Role |
|------|------|
| `HarmonyBootstrap.cs` | Patch discovery |
| `ProtoRegistry.cs` | LDB proto registration |
| `ProtoGameAdapter.cs` | Proto ID extraction |
| `ProtoIdAllocator.cs` | Pure ID math |
| `ModTestHelpers.cs` | Shared test assertions |
| `DspApex.Common.targets` | MSBuild deploy hook for Common.dll |