# Generates mods 2-4 and 6-21 with csproj, Plugin, HarmonyBootstrap, Logic, and Patch files.
$root = Split-Path $PSScriptRoot -Parent

function Write-ModCsproj($name) {
@'
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net472</TargetFramework>
    <AssemblyName>{0}</AssemblyName>
    <Version>0.1.0</Version>
    <LangVersion>latest</LangVersion>
    <AllowUnsafeBlocks>true</AllowUnsafeBlocks>
    <Nullable>disable</Nullable>
  </PropertyGroup>
  <PropertyGroup>
    <DSPPluginsPath>C:\Program Files (x86)\Steam\steamapps\common\Dyson Sphere Program\BepInEx\plugins\{0}</DSPPluginsPath>
  </PropertyGroup>
  <ItemGroup>
    <Reference Include="Assembly-CSharp"><HintPath>..\references\Assembly-CSharp.dll</HintPath><Private>false</Private></Reference>
    <Reference Include="UnityEngine"><HintPath>..\references\UnityEngine.dll</HintPath><Private>false</Private></Reference>
    <Reference Include="UnityEngine.CoreModule"><HintPath>..\references\UnityEngine.CoreModule.dll</HintPath><Private>false</Private></Reference>
    <Reference Include="BepInEx"><HintPath>..\references\BepInEx.dll</HintPath><Private>false</Private></Reference>
    <Reference Include="0Harmony"><HintPath>..\references\0Harmony.dll</HintPath><Private>false</Private></Reference>
  </ItemGroup>
  <Target Name="CopyToPlugins" AfterTargets="Build">
    <MakeDir Directories="$(DSPPluginsPath)" />
    <Copy SourceFiles="$(TargetPath)" DestinationFolder="$(DSPPluginsPath)" />
    <Message Text="Copied $(TargetFileName) to $(DSPPluginsPath)" Importance="high" />
  </Target>
</Project>
'@ -f $name
}

function Write-Plugin($name, $guid, $desc) {
@'
using System;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace {0}
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        internal static ManualLogSource Log;

        private void Awake()
        {
            Log = Logger;
            Logger.LogInfo($"{PluginInfo.PLUGIN_NAME} v{PluginInfo.PLUGIN_VERSION} — {2}");

            var harmony = new Harmony(PluginInfo.PLUGIN_GUID);
            int patched = 0;
            foreach (var type in typeof(Plugin).Assembly.GetTypes())
            {
                if (!HarmonyBootstrap.HasHarmonyPatches(type)) continue;
                try { harmony.CreateClassProcessor(type).Patch(); patched++; }
                catch (Exception ex) { Logger.LogError($"Patch failed {type.FullName}: {ex.Message}"); }
            }
            Logger.LogInfo($"{PluginInfo.PLUGIN_NAME} loaded ({patched} patch types).");
        }
    }

    public static class PluginInfo
    {
        public const string PLUGIN_GUID = "{1}";
        public const string PLUGIN_NAME = "{0}";
        public const string PLUGIN_VERSION = "0.1.0";
    }
}
'@ -f $name, $guid, $desc
}

$harmonyBootstrap = @'
using System;
using System.Reflection;
using HarmonyLib;

namespace MOD
{
    internal static class HarmonyBootstrap
    {
        internal static bool HasHarmonyPatches(Type type)
        {
            if (type.GetCustomAttributes(typeof(HarmonyPatch), true).Length > 0) return true;
            const BindingFlags f = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance | BindingFlags.DeclaredOnly;
            foreach (var m in type.GetMethods(f))
                if (m.GetCustomAttributes(typeof(HarmonyPatch), true).Length > 0) return true;
            return false;
        }
    }
}
'@

$mods = @(
    @{ Name='OrbitalRings'; Guid='com.paulspooner.dsp.orbitalrings'; Desc='Buildable planetary rings/swarms with relay and harvest effects' }
    @{ Name='OrbitalInfrastructure'; Guid='com.paulspooner.dsp.orbitalinfrastructure'; Desc='Space elevators, satellite swarms, modular orbital stations' }
    @{ Name='ZeroGProduction'; Guid='com.paulspooner.dsp.zerogproduction'; Desc='0G refineries and assemblers with orbital production bonuses' }
    @{ Name='DysonWeapons'; Guid='com.paulspooner.dsp.dysonweapons'; Desc='Particle accelerators, transmutation, and orbital beams' }
    @{ Name='SystemMover'; Guid='com.paulspooner.dsp.systemmover'; Desc='Relocate star systems and planets using warp energy' }
    @{ Name='HandmadeDyson'; Guid='com.paulspooner.dsp.handmadedyson'; Desc='Manual Icarus construction on orbital megastructures' }
    @{ Name='ExoticStars'; Guid='com.paulspooner.dsp.exoticstars'; Desc='X-class exotic stars and orbital telescope discovery' }
    @{ Name='TimelineScrubber'; Guid='com.paulspooner.dsp.timelinescrubber'; Desc='Timelapse fast-forward and limited rewind nodes' }
    @{ Name='SelfPropagation'; Guid='com.paulspooner.dsp.selfpropagation'; Desc='Autonomous sub-agents expanding infrastructure' }
    @{ Name='TerraformingReGreening'; Guid='com.paulspooner.dsp.terraformingregreening'; Desc='Expanded flora and organic megastructures' }
    @{ Name='FaunaNomads'; Guid='com.paulspooner.dsp.faunonomads'; Desc='Robot animals and nomadic herder playstyle' }
    @{ Name='SubterraneanConstruction'; Guid='com.paulspooner.dsp.subterraneanconstruction'; Desc='Deep mantle mines and layered ecumenopolis' }
    @{ Name='OrbitalShipyards'; Guid='com.paulspooner.dsp.orbitalshipyards'; Desc='Satellite launchers and sphere component mass production' }
    @{ Name='DysonSwarm2'; Guid='com.paulspooner.dsp.dysonswarm2'; Desc='Expanded swarm types: collector, combat, sensor' }
    @{ Name='QuantumLogistics'; Guid='com.paulspooner.dsp.quantumlogistics'; Desc='Intra-system teleporters and interstellar stargates' }
    @{ Name='DarkFogInfiltration'; Guid='com.paulspooner.dsp.darkfoginfiltration'; Desc='Hack and convert Dark Fog assets' }
    @{ Name='RamscoopShips'; Guid='com.paulspooner.dsp.ramscoopships'; Desc='Trade speed for transit harvest quantity' }
    @{ Name='RingworldsOrganic'; Guid='com.paulspooner.dsp.ringworldsorganic'; Desc='Organic ringworld biomass maximization' }
    @{ Name='ThermalEffects'; Guid='com.paulspooner.dsp.thermaleffects'; Desc='Planet thermal specialization for refineries and assemblers' }
)

foreach ($m in $mods) {
    $dir = Join-Path $root $m.Name
    New-Item -ItemType Directory -Force -Path $dir | Out-Null
    Set-Content (Join-Path $dir "$($m.Name).csproj") (Write-ModCsproj $m.Name) -Encoding UTF8
    Set-Content (Join-Path $dir 'Plugin.cs') (Write-Plugin $m.Name $m.Guid $m.Desc) -Encoding UTF8
    Set-Content (Join-Path $dir 'HarmonyBootstrap.cs') ($harmonyBootstrap -replace 'MOD', $m.Name) -Encoding UTF8
    Write-Host "Scaffolded $($m.Name)"
}

Write-Host 'Done. Add Logic and Patch files per mod.'