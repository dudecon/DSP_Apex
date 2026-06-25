param(
    [Parameter(Mandatory)][string]$Name,
    [Parameter(Mandatory)][string]$Guid,
    [Parameter(Mandatory)][string]$Description,
    [string]$LogicClass,
    [string]$PatchTarget = "DysonSphere",
    [string]$PatchMethod = "GameTick"
)

$root = Split-Path $PSScriptRoot -Parent
$dir = Join-Path $root $Name
New-Item -ItemType Directory -Force -Path $dir | Out-Null

$logic = if ($LogicClass) { $LogicClass } else { "${Name}Logic" }

$csproj = @"
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net472</TargetFramework>
    <AssemblyName>$Name</AssemblyName>
    <Version>0.1.0</Version>
    <LangVersion>latest</LangVersion>
    <AllowUnsafeBlocks>true</AllowUnsafeBlocks>
    <Nullable>disable</Nullable>
    <GenerateDocumentationFile>false</GenerateDocumentationFile>
  </PropertyGroup>
  <PropertyGroup>
    <DSPPluginsPath>C:\Program Files (x86)\Steam\steamapps\common\Dyson Sphere Program\BepInEx\plugins\$Name</DSPPluginsPath>
  </PropertyGroup>
  <ItemGroup>
    <Reference Include="Assembly-CSharp"><HintPath>..\references\Assembly-CSharp.dll</HintPath><Private>false</Private></Reference>
    <Reference Include="UnityEngine"><HintPath>..\references\UnityEngine.dll</HintPath><Private>false</Private></Reference>
    <Reference Include="UnityEngine.CoreModule"><HintPath>..\references\UnityEngine.CoreModule.dll</HintPath><Private>false</Private></Reference>
    <Reference Include="BepInEx"><HintPath>..\references\BepInEx.dll</HintPath><Private>false</Private></Reference>
    <Reference Include="0Harmony"><HintPath>..\references\0Harmony.dll</HintPath><Private>false</Private></Reference>
  </ItemGroup>
  <Target Name="CopyToPlugins" AfterTargets="Build">
    <MakeDir Directories="`$(DSPPluginsPath)" />
    <Copy SourceFiles="`$(TargetPath)" DestinationFolder="`$(DSPPluginsPath)" />
    <Message Text="Copied `$(TargetFileName) to `$(DSPPluginsPath)" Importance="high" />
  </Target>
</Project>
"@
Set-Content -Path (Join-Path $dir "$Name.csproj") -Value $csproj -Encoding UTF8

$plugin = @"
using System;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace $Name
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        internal static ManualLogSource Log;

        private void Awake()
        {
            Log = Logger;
            Logger.LogInfo(`$"{PluginInfo.PLUGIN_NAME} v{PluginInfo.PLUGIN_VERSION} — $Description");

            var harmony = new Harmony(PluginInfo.PLUGIN_GUID);
            int patchedTypes = 0;
            foreach (var type in typeof(Plugin).Assembly.GetTypes())
            {
                if (!HarmonyBootstrap.HasHarmonyPatches(type)) continue;
                try { harmony.CreateClassProcessor(type).Patch(); patchedTypes++; }
                catch (Exception ex) { Logger.LogError(`$"Failed to patch {type.FullName}: {ex.Message}"); }
            }
            Logger.LogInfo(`$"{PluginInfo.PLUGIN_NAME} loaded ({patchedTypes} patch types).");
        }
    }

    public static class PluginInfo
    {
        public const string PLUGIN_GUID = "$Guid";
        public const string PLUGIN_NAME = "$Name";
        public const string PLUGIN_VERSION = "0.1.0";
    }
}
"@
Set-Content -Path (Join-Path $dir "Plugin.cs") -Value $plugin -Encoding UTF8

$bootstrap = @"
using System;
using System.Reflection;
using HarmonyLib;

namespace $Name
{
    internal static class HarmonyBootstrap
    {
        internal static bool HasHarmonyPatches(Type type)
        {
            if (type.GetCustomAttributes(typeof(HarmonyPatch), true).Length > 0)
                return true;

            const BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance | BindingFlags.DeclaredOnly;
            foreach (var method in type.GetMethods(flags))
            {
                if (method.GetCustomAttributes(typeof(HarmonyPatch), true).Length > 0)
                    return true;
            }
            return false;
        }
    }
}
"@
Set-Content -Path (Join-Path $dir "HarmonyBootstrap.cs") -Value $bootstrap -Encoding UTF8

Write-Host "Scaffolded $Name at $dir"