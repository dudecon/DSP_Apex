# Build all DSP Apex mod projects (mods 0-21)
param(
    [int]$Runs = 1,
    [string]$LogDir = ""
)

$ErrorActionPreference = "Stop"
$dotnet = if ($env:DOTNET_EXE) { $env:DOTNET_EXE } else { "C:\Program Files\dotnet\dotnet.exe" }
$root = $PSScriptRoot

if (-not (Test-Path $dotnet)) {
    Write-Error "dotnet not found at $dotnet"
}

$projects = @(
    "RecipeRebalance\RecipeRebalance.csproj",
    "DysonHarvester\DysonHarvester.csproj",
    "OrbitalRings\OrbitalRings.csproj",
    "OrbitalInfrastructure\OrbitalInfrastructure.csproj",
    "ZeroGProduction\ZeroGProduction.csproj",
    "MegaStructuresUI\MegaStructuresUI.csproj",
    "DysonWeapons\DysonWeapons.csproj",
    "SystemMover\SystemMover.csproj",
    "HandmadeDyson\HandmadeDyson.csproj",
    "ExoticStars\ExoticStars.csproj",
    "TimelineScrubber\TimelineScrubber.csproj",
    "SelfPropagation\SelfPropagation.csproj",
    "TerraformingReGreening\TerraformingReGreening.csproj",
    "FaunaNomads\FaunaNomads.csproj",
    "SubterraneanConstruction\SubterraneanConstruction.csproj",
    "OrbitalShipyards\OrbitalShipyards.csproj",
    "DysonSwarm2\DysonSwarm2.csproj",
    "QuantumLogistics\QuantumLogistics.csproj",
    "DarkFogInfiltration\DarkFogInfiltration.csproj",
    "RamscoopShips\RamscoopShips.csproj",
    "RingworldsOrganic\RingworldsOrganic.csproj",
    "ThermalEffects\ThermalEffects.csproj"
)

if ($LogDir -ne "") {
    New-Item -ItemType Directory -Force -Path $LogDir | Out-Null
}

$failed = @()

for ($r = 1; $r -le $Runs; $r++) {
    if ($Runs -gt 1) {
        Write-Host "`n=== Build run $r of $Runs ===" -ForegroundColor Cyan
    }

    $runFailed = @()
    foreach ($proj in $projects) {
        $name = [System.IO.Path]::GetFileNameWithoutExtension($proj)
        Write-Host "Building $name..." -ForegroundColor Yellow

        Push-Location $root
        $output = & $dotnet build $proj -c Debug 2>&1
        $code = $LASTEXITCODE
        Pop-Location

        if ($LogDir -ne "") {
            $log = Join-Path $LogDir "build-$name-run$r.log"
            $output | Out-File -FilePath $log -Encoding utf8
        }

        if ($code -ne 0) {
            $runFailed += $name
            $failed += "$name (run $r)"
            Write-Host "FAILED: $name" -ForegroundColor Red
            if ($LogDir -eq "") { $output | Write-Host }
        } else {
            Write-Host "OK: $name" -ForegroundColor Green
        }
    }

    if ($runFailed.Count -eq 0) {
        $success = "All $($projects.Count) mods built successfully."
        Write-Host "`n$success" -ForegroundColor Green
        if ($LogDir -ne "") {
            $allLog = Join-Path $LogDir "build-all-run$r.log"
            @("=== Build run $r of $Runs ===", $success, "RUN_EXIT_CODE: 0") | Out-File -FilePath $allLog -Encoding utf8
        }
    } elseif ($LogDir -ne "") {
        $allLog = Join-Path $LogDir "build-all-run$r.log"
        @("=== Build run $r of $Runs ===", "FAILED: $($runFailed -join ', ')", "RUN_EXIT_CODE: 1") | Out-File -FilePath $allLog -Encoding utf8
    }
}

if ($failed.Count -gt 0) {
    Write-Host "`nFailed builds: $($failed -join ', ')" -ForegroundColor Red
    if ($LogDir -ne "") {
        @("FINAL_SCRIPT_EXIT_CODE: 1", "FAILED: $($failed -join ', ')") | Out-File -FilePath (Join-Path $LogDir "build-all-summary.log") -Encoding utf8
    }
    exit 1
}

if ($LogDir -ne "") {
    @("FINAL_SCRIPT_EXIT_CODE: 0", "RUNS: $Runs") | Out-File -FilePath (Join-Path $LogDir "build-all-summary.log") -Encoding utf8
}

Write-Host "`nAll $($projects.Count) mods built successfully." -ForegroundColor Green
exit 0