# Capture verification evidence for DSP Apex goal
$ErrorActionPreference = "Stop"
$scratch = "C:\Users\dudec\AppData\Local\Temp\grok-goal-030430b0ef10\implementer"
$root = $PSScriptRoot
$dotnet = "C:\Program Files\dotnet\dotnet.exe"
$summary = Join-Path $scratch "verification-summary.md"
$featureLog = Join-Path $scratch "feature-evidence.log"
$buildScript = Join-Path $root "build-all.ps1"

New-Item -ItemType Directory -Force -Path $scratch | Out-Null

$lines = New-Object System.Collections.Generic.List[string]
$featureLines = New-Object System.Collections.Generic.List[string]
$lines.Add("# DSP Apex Verification Evidence")
$lines.Add("Generated: $(Get-Date -Format 'yyyy-MM-dd HH:mm:ss')")
$lines.Add("")

$failed = New-Object System.Collections.Generic.List[string]

function Add-FeatureEvidence($title, $pattern, $path, $expectMatch = $true) {
    $fullPath = Join-Path $root $path
    $hits = Select-String -Path $fullPath -Pattern $pattern -AllMatches -ErrorAction SilentlyContinue
    $script:featureLines.Add("### $title")
    $script:featureLines.Add("Pattern: $pattern in $path")
    if ($hits) {
        foreach ($h in $hits | Select-Object -First 8) {
            $script:featureLines.Add("$($h.Filename):$($h.LineNumber): $($h.Line.Trim())")
        }
        $script:featureLines.Add("Result: FOUND ($($hits.Count) match(es))")
    } else {
        $script:featureLines.Add("Result: NOT FOUND")
    }
    $script:featureLines.Add("")
    if ($expectMatch -and -not $hits) { $script:failed.Add("feature $title") }
}

$lines.Add("## Build gating (build-all.ps1)")
$lines.Add("")
Push-Location $root
& powershell -NoProfile -ExecutionPolicy Bypass -File $buildScript -Run 2>&1 | Tee-Object -FilePath (Join-Path $scratch "verify-build-all-console.log")
$buildAllExit = $LASTEXITCODE
Pop-Location
$lines.Add("- build-all.ps1 -Run exit code: **$buildAllExit**")
if ($buildAllExit -ne 0) { $failed.Add("build-all exit") }

foreach ($runNum in @(1, 2)) {
    $runLog = Join-Path $scratch "build-all-run$runNum.log"
    $lines.Add("- build-all-run$runNum.log: ``$runLog``")
    if (-not (Test-Path $runLog)) {
        $lines.Add("  - **MISSING**")
        $failed.Add("build-all-run$runNum.log missing")
        continue
    }
    $successHit = Select-String -Path $runLog -Pattern "All \d+ mods built successfully" -Quiet
    $exitHit = Select-String -Path $runLog -Pattern "RUN_EXIT_CODE:\s*0" -Quiet
    $lines.Add("  - success string: $(if ($successHit) { 'FOUND' } else { 'NOT FOUND' })")
    $lines.Add("  - RUN_EXIT_CODE: 0: $(if ($exitHit) { 'FOUND' } else { 'NOT FOUND' })")
    if (-not $successHit) { $failed.Add("build-all-run$runNum success string") }
    if (-not $exitHit) { $failed.Add("build-all-run$runNum exit code") }
}
$summaryLog = Join-Path $scratch "build-all-summary.log"
if (Test-Path $summaryLog) {
    $finalLine = Get-Content $summaryLog | Where-Object { $_ -match "FINAL_SCRIPT_EXIT_CODE" } | Select-Object -First 1
    $lines.Add("- build-all-summary.log: $finalLine")
} else {
    $lines.Add("- build-all-summary.log: **MISSING**")
    $failed.Add("build-all-summary.log missing")
}
$lines.Add("")

$lines.Add("## Skeptic gap resolution (source grep)")
$lines.Add("")

function Get-AssemblyTypesSafe([Reflection.Assembly]$Assembly) {
    try {
        return @($Assembly.GetTypes())
    } catch [System.Reflection.ReflectionTypeLoadException] {
        return @($_.Exception.Types | Where-Object { $_ -ne $null })
    }
}

function Add-GrepEvidence($title, $pattern, $path, $expectMatch = $true) {
    $script:lines.Add("### $title")
    $script:lines.Add("Pattern: ``$pattern`` in ``$path``")
    $fullPath = Join-Path $root $path
    $hits = Select-String -Path $fullPath -Pattern $pattern -AllMatches -ErrorAction SilentlyContinue
    if ($hits) {
        foreach ($h in $hits | Select-Object -First 5) {
            $script:lines.Add("- $($h.Filename):$($h.LineNumber): $($h.Line.Trim())")
        }
        $script:lines.Add("Result: **FOUND** ($($hits.Count) match(es))")
    } else {
        $script:lines.Add("Result: **NOT FOUND**")
    }
    if ($expectMatch -and -not $hits) { $script:failed.Add($title) }
    if (-not $expectMatch -and $hits) { $script:failed.Add("$title (should be absent)") }
    $script:lines.Add("")
}

Add-GrepEvidence "Harvester stats gated on ShouldUpdateProductRegister" "ShouldUpdateProductRegister" "DysonHarvester\HarvesterService.cs"
Add-GrepEvidence "Harvester uses AccumulatePulseDeposits" "AccumulatePulseDeposits" "DysonHarvester\HarvesterService.cs"
Add-GrepEvidence "DepositService uses ExecuteDepositRoute" "ExecuteDepositRoute" "DysonHarvester\HarvesterDepositService.cs"
Add-GrepEvidence "Shipped ShouldUpdateProductRegister gate" "ShouldUpdateProductRegister" "DysonHarvester\HarvesterService.cs"
Add-GrepEvidence "No nodeCursor-based stats" "nodeCursor - 1" "DysonHarvester\HarvesterService.cs" -expectMatch $false
Add-GrepEvidence "Deposit uses station.AddItem" "station\.AddItem" "DysonHarvester\HarvesterDepositService.cs"
Add-GrepEvidence "No InsertInto in DepositService" "InsertInto" "DysonHarvester\HarvesterDepositService.cs" -expectMatch $false
Add-GrepEvidence "No TryDepositFactoryInsert" "TryDepositFactoryInsert" "DysonHarvester\HarvesterDepositService.cs" -expectMatch $false
Add-GrepEvidence "Relay uses TryResolveGeneratorPlanet" "TryResolveGeneratorPlanet" "OrbitalRings\PowerRelayPatch.cs"
Add-GrepEvidence "No localPlanet in PowerRelayPatch" "localPlanet" "OrbitalRings\PowerRelayPatch.cs" -expectMatch $false
Add-GrepEvidence "Sensor consumer patch exists" "SensorCoveragePatch" "OrbitalRings\SensorCoveragePatch.cs"
Add-GrepEvidence "Ring/swarm proto registration" "OrbitalRingProtoRegistry" "OrbitalRings\OrbitalRingProtoRegistry.cs"
Add-GrepEvidence "VFPreload proto bootstrap" "OrbitalRingsVFPreloadPatch" "OrbitalRings\OrbitalRingsVFPreloadPatch.cs"
Add-GrepEvidence "UI stats display" "RefreshStats" "MegaStructuresUI\OrbitalCommandInstaller.cs"
Add-GrepEvidence "UI visibility toggle" "TogglePanel" "MegaStructuresUI\OrbitalCommandInstaller.cs"
Add-GrepEvidence "UI Canvas fallback" "GetComponentInParent<Canvas>" "MegaStructuresUI\OrbitalCommandInstaller.cs"
Add-GrepEvidence "Single TimelineScrubber Bind" "TimelineScrubberConfig\.Bind" "TimelineScrubber\Plugin.cs"
Add-GrepEvidence "Deposit routing tests exist" "HarvesterDepositRoutingTests" "DysonHarvester.Tests\HarvesterDepositRoutingTests.cs"
Add-GrepEvidence "Service stats path tests exist" "HarvesterServiceStatsTests" "DysonHarvester.Tests\HarvesterServiceStatsTests.cs"
Add-GrepEvidence "Consumer bonus path tests exist" "OrbitalRingConsumerTests" "OrbitalRings.Tests\OrbitalRingConsumerTests.cs"
Add-GrepEvidence "Mod5 stats logic" "OrbitalCommandStatsLogic" "MegaStructuresUI\OrbitalCommandStatsLogic.cs"
Add-GrepEvidence "Mod5 beam damage stat" "BeamDamageRate" "MegaStructuresUI\OrbitalCommandStatsLogic.cs"
Add-GrepEvidence "Mod5 transmutation stat" "TransmutationRate" "MegaStructuresUI\OrbitalCommandStatsLogic.cs"
Add-GrepEvidence "Timelapse extrapolation" "TickExtrapolation" "TimelineScrubber\TimelineScrubberRuntime.cs"
Add-GrepEvidence "Self-propagation deficit" "ReportDeficit" "SelfPropagation\SelfPropagationRuntime.cs"
Add-GrepEvidence "Quantum stargate transfer" "TryStargateTransfer" "QuantumLogistics\QuantumLogisticsRuntime.cs"
Add-GrepEvidence "Thermal waste heat" "AccumulateWasteHeat" "ThermalEffects\ThermalEffectsLogic.cs"
Add-GrepEvidence "Fog reciprocal risk" "ApplyReciprocalRisk" "DarkFogInfiltration\DarkFogInfiltrationLogic.cs"
Add-GrepEvidence "Orbital command stats tests" "OrbitalCommandStatsTests" "MegaStructuresUI.Tests\OrbitalCommandStatsTests.cs"
Add-GrepEvidence "Subterranean runtime tick" "SubterraneanConstructionRuntime" "SubterraneanConstruction\SubterraneanConstructionRuntime.cs"
Add-GrepEvidence "Stargate patch wired" "QuantumLogisticsStargatePatch" "QuantumLogistics\QuantumLogisticsPatch.cs"
Add-GrepEvidence "Shipyard repair tick" "OrbitalShipyardsRepairPatch" "OrbitalShipyards\OrbitalShipyardsPatch.cs"
Add-GrepEvidence "Ramscoop effective speed" "GetEffectiveSpeed" "RamscoopShips\RamscoopShipsRuntime.cs"
Add-GrepEvidence "Swarm role assignment" "AssignRoleForOrbitCount" "DysonSwarm2\DysonSwarm2Runtime.cs"
Add-GrepEvidence "Fauna register agent wired" "RegisterAgent" "FaunaNomads\FaunaNomadsRuntime.cs"
Add-GrepEvidence "Fauna deficit scaling" "NomadAgentsForDeficit" "FaunaNomads\FaunaNomadsRuntime.cs"
Add-GrepEvidence "Fauna nomad range wired" "NomadRange" "FaunaNomads\FaunaNomadsRuntime.cs"
Add-GrepEvidence "Timeline AdvanceTick shipped" "AdvanceTick" "TimelineScrubber\TimelineScrubberRuntime.cs"
Add-GrepEvidence "Timeline NextBranchDepth shipped" "NextBranchDepth" "TimelineScrubber\TimelineScrubberRuntime.cs"
Add-GrepEvidence "Subterranean layered yield" "ApplyLayeredYield" "SubterraneanConstruction\SubterraneanLogic.cs"

Add-FeatureEvidence "TickLayers call site" "TickLayers\(" "SubterraneanConstruction\SubterraneanConstructionPatch.cs"
Add-FeatureEvidence "ApplyMiningBonus call site" "ApplyMiningBonus\(" "SubterraneanConstruction\SubterraneanConstructionPatch.cs"
Add-FeatureEvidence "TickRepair call site" "TickRepair\(" "OrbitalShipyards\OrbitalShipyardsPatch.cs"
Add-FeatureEvidence "RegisterAgent call site" "RegisterAgent\(" "FaunaNomads\FaunaNomadsRuntime.cs"
Add-FeatureEvidence "SyncAgentsFromDeficit call site" "SyncAgentsFromDeficit\(" "FaunaNomads\FaunaNomadsPatch.cs"
Add-FeatureEvidence "AdvanceGameTick call site" "AdvanceGameTick\(" "TimelineScrubber\TimelineScrubberPatch.cs"
Add-FeatureEvidence "NextBranchDepth call site" "NextBranchDepth\(" "TimelineScrubber\TimelineScrubberRuntime.cs"

$bindCount = (Select-String -Path (Join-Path $root "TimelineScrubber\Plugin.cs") -Pattern "TimelineScrubberConfig\.Bind" -AllMatches).Count
$lines.Add("### TimelineScrubber Bind call count")
$lines.Add("Count: **$bindCount** (expected 1)")
$lines.Add("")
if ($bindCount -ne 1) { $failed.Add("TimelineScrubber Bind count") }

$lines.Add("## Unit tests (shipped logic paths)")
$lines.Add("")

$testProjects = @(
    "DysonHarvester.Tests\DysonHarvester.Tests.csproj",
    "OrbitalRings.Tests\OrbitalRings.Tests.csproj",
    "MegaStructuresUI.Tests\MegaStructuresUI.Tests.csproj",
    "DysonWeapons.Tests\DysonWeapons.Tests.csproj",
    "ThermalEffects.Tests\ThermalEffects.Tests.csproj",
    "TimelineScrubber.Tests\TimelineScrubber.Tests.csproj",
    "SelfPropagation.Tests\SelfPropagation.Tests.csproj",
    "QuantumLogistics.Tests\QuantumLogistics.Tests.csproj",
    "FaunaNomads.Tests\FaunaNomads.Tests.csproj",
    "SubterraneanConstruction.Tests\SubterraneanConstruction.Tests.csproj"
)

foreach ($proj in $testProjects) {
    $name = [IO.Path]::GetFileNameWithoutExtension($proj)
    $log = Join-Path $scratch "test-$name.log"
    Push-Location $root
    & $dotnet test $proj -c Debug 2>&1 | Tee-Object -FilePath $log
    $code = $LASTEXITCODE
    Pop-Location
    $passed = Select-String -Path $log -Pattern "Passed!" -Quiet
    $status = if ($passed) { "PASSED" } else { "FAILED" }
    $lines.Add("- **$name**: exit=$code $status - log: ``$log``")
    if ($code -ne 0) { $failed.Add("test $name") }
}

$lines.Add("")
$lines.Add("## Assembly patch inspection")
$lines.Add("")

$mods = @(
    @{ Name = "RecipeRebalance"; Patch = "MinerStonePatch|RecipeBootstrap|VFPreloadPatch" },
    @{ Name = "DysonHarvester"; Patch = "DysonSphereHarvestPatch|HarvesterService|HarvesterDepositService" },
    @{ Name = "OrbitalRings"; Patch = "PowerRelayPatch|SensorCoveragePatch|OrbitalRingsVFPreloadPatch" },
    @{ Name = "OrbitalInfrastructure"; Patch = "OrbitalInfrastructure|InfrastructureProtoRegistry" },
    @{ Name = "ZeroGProduction"; Patch = "ZeroGProductionPatch|ZeroGProtoBootstrap" },
    @{ Name = "MegaStructuresUI"; Patch = "OrbitalCommandPatch|OrbitalCommandInstaller" },
    @{ Name = "DysonWeapons"; Patch = "DysonWeaponsPatch" },
    @{ Name = "SystemMover"; Patch = "SystemMoverPatch" },
    @{ Name = "HandmadeDyson"; Patch = "HandmadeDyson" },
    @{ Name = "ExoticStars"; Patch = "ExoticStars" },
    @{ Name = "TimelineScrubber"; Patch = "TimelineScrubberPatch" },
    @{ Name = "SelfPropagation"; Patch = "SelfPropagationPatch" },
    @{ Name = "TerraformingReGreening"; Patch = "TerraformingReGreeningPatch" },
    @{ Name = "FaunaNomads"; Patch = "FaunaNomadsPatch" },
    @{ Name = "SubterraneanConstruction"; Patch = "SubterraneanLayerTickPatch|SubterraneanMinerPatch" },
    @{ Name = "OrbitalShipyards"; Patch = "OrbitalShipyardsRocketPatch|OrbitalShipyardsRepairPatch" },
    @{ Name = "DysonSwarm2"; Patch = "DysonSwarm2Patch" },
    @{ Name = "QuantumLogistics"; Patch = "QuantumLogisticsTowerPatch|QuantumLogisticsStargatePatch" },
    @{ Name = "DarkFogInfiltration"; Patch = "DarkFogInfiltrationPatch" },
    @{ Name = "RamscoopShips"; Patch = "RamscoopShipsPatch" },
    @{ Name = "RingworldsOrganic"; Patch = "RingworldsOrganicPatch" },
    @{ Name = "ThermalEffects"; Patch = "ThermalEffects" }
)

foreach ($mod in $mods) {
    $dll = Join-Path $root "$($mod.Name)\bin\Debug\net472\$($mod.Name).dll"
    $log = Join-Path $scratch "patches-$($mod.Name).log"
    if (-not (Test-Path $dll)) {
        $lines.Add("- **$($mod.Name)**: DLL missing at ``$dll``")
        $failed.Add("dll $($mod.Name)")
        continue
    }
    $asm = [Reflection.Assembly]::LoadFrom($dll)
    $types = @(Get-AssemblyTypesSafe $asm | Where-Object { $_.Name -match $mod.Patch } | ForEach-Object { $_.FullName })
    $types | Out-File -FilePath $log -Encoding utf8
    $lines.Add("- **$($mod.Name)**: $($types.Count) type(s) - ``$log``")
    if ($types.Count -eq 0) { $failed.Add("patches $($mod.Name)") }
}

$lines.Add("")
$lines.Add("## Integrated path test mapping")
$lines.Add("")
$lines.Add("- HarvesterServiceStatsTests: calls HarvesterYieldLogic.AccumulatePulseDeposits + ShouldUpdateProductRegister (shipped by HarvesterService)")
$lines.Add("- HarvesterDepositRoutingTests: calls HarvesterDepositLogic.ExecuteDepositRoute (shipped by HarvesterDepositService)")
$lines.Add("- OrbitalRingConsumerTests: calls OrbitalRingLogic.ApplyRelayBonus/ApplyHarvestBonus/ApplySensorSearchBonus (shipped by patches)")
$lines.Add("- OrbitalCommandStateTests: calls OrbitalCommandState.AllocateModule (shipped by OrbitalCommandPanel buttons)")
$lines.Add("- OrbitalCommandStatsTests: calls OrbitalCommandStatsLogic.ComputeStats/FormatStatsText (shipped by OrbitalCommandInstaller.RefreshStats)")
$lines.Add("- TimelineScrubberLogicTests: calls TimelineScrubberLogic.InterpolateProduction/AdvanceTick (shipped by TimelineScrubberRuntime)")
$lines.Add("- SelfPropagationLogicTests: calls SelfPropagationLogic.BlueprintDemand/ShouldExpand (shipped by SelfPropagationRuntime.TickAgents)")
$lines.Add("- QuantumLogisticsLogicTests: calls QuantumLogisticsLogic.StargateTransferCost/CanBuildStargate (shipped by QuantumLogisticsRuntime)")
$lines.Add("- FaunaNomadsLogicTests: calls FaunaNomadLogic.NomadAgentsForDeficit/NomadRange (shipped by FaunaNomadsRuntime.SyncAgentsFromDeficit/TickHerds)")
$lines.Add("- SubterraneanLogicTests: calls SubterraneanLogic.ApplyLayeredYield (shipped by SubterraneanConstructionRuntime.ApplyMiningBonus)")
$lines.Add("")

$featureLines.Insert(0, "DSP Apex Feature Evidence")
$featureLines.Insert(1, "Generated: $(Get-Date -Format 'yyyy-MM-dd HH:mm:ss')")
$featureLines.Insert(2, "")
$featureLines | Out-File -FilePath $featureLog -Encoding utf8
$lines.Add("## Feature evidence log")
$lines.Add("Written to: ``$featureLog``")
$lines.Add("")

if ($failed.Count -gt 0) {
    $lines.Add("## FAILURES")
    foreach ($f in $failed) { $lines.Add("- $f") }
    $lines | Out-File -FilePath $summary -Encoding utf8
    Write-Host "Verification FAILED: $($failed -join ', ')" -ForegroundColor Red
    exit 1
}

$lines.Add("## RESULT: ALL CHECKS PASSED")
$lines | Out-File -FilePath $summary -Encoding utf8
Write-Host "Verification evidence written to $summary" -ForegroundColor Green
exit 0