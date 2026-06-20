# references/

This folder contains the game and BepInEx assemblies needed to build the mods.

## How to populate

1. Install BepInEx into the game (recommended: use r2modman or Thunderstore mod manager for DSP).
2. Copy the following from the game install:

   From `DSPGAME_Data/Managed/`:
   - Assembly-CSharp.dll
   - UnityEngine*.dll (the main ones)

   From `BepInEx/core/` (after installing BepInEx):
   - BepInEx.Core.dll
   - BepInEx.Unity.Mono.dll (or the appropriate Unity loader)
   - 0Harmony.dll

3. Paste them into this folder.

Alternatively, for convenience during active development you can run a script or set post-build events in the .csproj files to reference the full game path directly.

Current contents (if any) were copied from the local DSP install.
