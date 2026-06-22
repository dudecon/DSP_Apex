# Dyson Apex Architecture Mod Suite – Comprehensive Design Document

**Version:** 1.2 (Merged 2026-06-16)  
**Author:** Paul Spooner (with Grok assistance)

**Purpose:** This document captures the full vision for a modular Dyson Sphere Program mod suite. It is intended to be used as input for Grok Build or similar implementation tools. All features, refinements, and discussions from the conversation are summarized here, including core mods 1-9 and peripheral/side mods 10-21.

## Overall Vision & Philosophy

The suite bridges the scale gap between terrestrial factories and full stellar Dyson Spheres by adding a coherent mid-to-late game orbital civilization layer. It emphasizes:

- Progressive scale escalation (planet → orbital → system → exotic megastructures).
- Realistic physics-inspired mechanics (e.g., power layering, zero-G advantages, thermal effects).
- Player agency at all scales (manual construction + abstracted high-level management).
- Thematic cohesion around Dyson Spheres as ultimate industrial, scientific, and military platforms.
- Performance-friendly abstraction at large scales (procedural visuals, modular design, enclosed modules).
- Modularity: Each mod is independent but gains power when combined.

Core themes: Resource mastery, orbital infrastructure, exotic discovery, weaponization, and empire-scale manipulation. The Dyson Sphere evolves from a power source into a living command center and "Death Star" equivalent.

## Core Mod Suite (0-9)

### 0. Recipe Rebalance

- The core recipe updates, shared by several of the other mods.
- Miners not at ore spots produce stone (low rate).
- (DONE) Ore crafting from stone (like Silicon ore) to allow low-efficiency production anywhere.
- (IN PROGRESS) Fusion product chains. Deut Fusion produces Helium. Triple-helium to produce Energetic Graphite. Energetic Graphite fusion to produce stone.
- (IN PROGRESS) More recipes for a path to all the exotic materials using mod 4 & 6, but extensible with other non-pack mods if desired.


### 1. DysonHarvester

- Dyson Sphere nodes act as advanced Orbital Collectors: Produce Hydrogen + Deuterium (ratios vary by star class/luminosity). Exotic stars yield additional rare materials (Fire Ice, Strange Matter, etc.).
- Layered power realism: Only the **innermost completed shell/layer** actively collects power and resources. Outer layers are primarily structural/cosmetic with limited secondary benefits.
- Nodes on innermost layer prioritized. Configurable yields, star modifiers, and proliferation support.
- Synergies: Feeds transmutation, weapons, and relocation.

### 2. OrbitalRings

- Buildable swarms/rings/shells around planets (frames/struts as connecting elements, similar to Dyson Sphere frames). Fewer layers than stellar enclosures.
- Rocky planets: microsats act as **power relays** enabling ray receivers to work on the dark side (like vanilla planets inside a sphere).
- Gas giants: Enhanced atmospheric harvesting, fitting vanilla OC pattern, use Orbital Collectors as nodes in shells.
- Buildable struts/frames + panels for modularity. Supports later module integration.

### 3. OrbitalInfrastructure

- **Space Elevators**: On suitable planets; connect surface to orbit for high-throughput logistics and power. Vanilla 
- **Planetary Satellite Swarms**: Small orbital swarms for local power, relays, or early Dark Fog defense.
- **Modular Orbital Stations**: Fixed expandable platforms in orbit around planets/stars. Start small, grow into habitats/factories with docking.

### 4. ZeroGProduction

- **0G Refineries**: Produce Dyson Sphere / Ring components directly from Titanium (as metal foam) and advanced parts with high efficiency.
- **0G Assemblers**: Alternate advanced recipes (better yields, new byproducts, specialized megastructure parts).
- Encourages shifting late-game production to orbit. Bonuses when paired with rings/stations/ships.

### 5. MegaStructuresUI

- **Sphere Production Panel / Orbital Command Interface**: Dedicated UI (extending Dyson Sphere editor) for allocating space/modules across rings, stations, ships, and sphere sections. Real-time stats on output, power, transmutation, beams.
- **Orbital Modules**: Replace blueprints at orbital scale. Players design/optimize enclosed production blocks that snap into megastructures.
- Procedural generation of external visuals (glowing panels, struts, lights, antennas) based on internal allocation. Fully enclosed modules for performance; optional zoom-in for factory detail.
- Abstracted simulation at large scales while retaining factory optimization feel.

### 6. DysonWeapons

- Outer sphere rings (completed frames in polar mode) convertible to **massive particle accelerators**.
- Transmutation: Convert abundant H/D (plus power) into rarer resources; Stone/Soil into ores at higher efficiency.
- Orbital particle beams: Anti-Dark Fog defense, escalating to interstellar beams (Warper cost, Long Guns-inspired).
- Planet harvesting/evaporation/destruction at high tier (high cost/risk/reward).
- Exotic stars provide superior “Death Star cores” with enhanced power/special effects.

### 7. SystemMover

- Relocate entire star systems or individual planets using massive energy and Warpers.
- **Black hole / exotic star bonuses**: Massive advantages in efficiency, stability, range, and cost.
- **Black hole inner shell warping**: Ability to move a compact/custom inner shell (or full sphere) to a new cluster for NG+ style play.
- Ultimate capstone feature.

### 8. HandmadeDyson

- Manual construction mode with Icarus/mech.
- Land and build directly on orbital megastructures, rings, stations, factory ships, accelerator segments, etc. (similar to placing vanilla Orbital Collectors).
- Preserves hands-on satisfaction for key pieces; complements automated/UI systems.

### 9. ExoticStars

- New X-class stars: White Holes, Strange Stars, Monopole Stars, and others (unique visuals, properties, risks/rewards).
- **Discovery**: Massive **Orbital Telescope** arrays (built from Dyson panels or ring/station modules). Scan to reveal hidden exotics (dynamic, KSP-asteroid style).
- Strong ties: Rich exotic materials (Harvester), superior weapon cores (Weapons), best relocation anchors (Mover).

## Peripheral / Side Mods (10-21)

These are optional expansions. They are not core but integrate well. (Note: An early concept for timelapse capability was referred to as "FastForward".)

### 10. TimelineScrubber

Timelapse mode with fast-forward, interpolation/extrapolation. Save games as nodes/branch points with limited rewind.

### 11. SelfPropagation

Autonomous sub-agents that independently build infrastructure and expand the empire on multiple fronts.

### 12. TerraformingReGreening

Expanded flora, harvestable organics, genetic engineering for harsh environments. Organic Dyson Spheres / bio-structures.

### 13. FaunaNomads

Robot animals, space fauna, nomadic herder playstyle (mobile analog to sessile factories).

### 14. SubterraneanConstruction

Deep mantle/core mines and true layered ecumenopolis architecture.

### 15. OrbitalShipyards

Low-energy satellite launchers, large vessel repair/replication, mass-production of sphere components.

### 16. DysonSwarm2

Expanded swarm types beyond vanilla power: collectors, construction, combat, sensor swarms (and others as desired).

### 17. QuantumLogistics

Resource teleporters (instant intra-system between towers with Warper cost). New interstellar Stargates (orbital megastructures only).

### 18. DarkFogInfiltration

Use/research Dark Fog scraps to hide, hack, and convert enemy assets/units/bases. Reciprocal risk: Fog can infiltrate player structures.

### 19. RamscoopShips

Trade travel speed for increased harvest quantity during transit.

### 20. RingworldsOrganic

Ringworld-style structures with strong synergy to flora/organic mods. Organic Dyson Spheres focused on biomass maximization.

### 21. ThermalEffects

Environmental specialization: Lava planets reduce refining energy costs but excess heat slows assemblers; cold planets have converse effects. Waste heat accumulation. Synergies with Harvester (1), Weapons (6), Terraforming (12), and Subterranean (14). Adds depth even to vanilla bases.

## Cross-Cutting Features & Design Principles

- **Progression**: Surface factories → Elevators/swarms → Rings/stations/ships → Full Sphere → Exotic optimization → System manipulation.
- **Zero-G Advantages**: Orbital production superior for certain recipes/components.
- **Performance**: Heavy use of abstraction, procedural visuals, and enclosed modules at orbital scales.
- **Customization**: Extensive config files for yields, costs, realism level, etc.
- **Compatibility**: Designed to work alongside GalacticScale, Dark Fog, etc. Optional inter-mod dependencies.
- **Theming**: Dyson Sphere as ultimate resource generator, power relay network, weapon platform, and mobile empire core.
- **Manual vs Automated**: Handmade mode for precision; UI/modules for scale.

## Implementation Priorities & Roadmap Suggestion

- Start with **DysonHarvester** (foundational, high impact).
- Then **OrbitalRings** + **HandmadeDyson**.
- Follow with UI/abstraction layer, production mods, weapons, exotics, mover.
- Use BepInEx + Harmony for patching. Reuse vanilla assets heavily.
- Test iteratively; maintain separate mod packages.

## Implementation Notes

- Use BepInEx + Harmony. Reuse vanilla assets heavily.
- Maintain separate packages for modularity.
- Test compatibility with GalacticScale, Dark Fog, etc.
- The document is self-contained and ready for implementation.

---

This document contains every major detail and refinement from the full conversation. All core and peripheral features are included.
