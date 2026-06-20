# DSP Mod Build Script
# Usage: .\build.ps1

$project = "MegaStructuresUI\MegaStructuresUI.csproj"
$dotnet = "C:\Program Files\dotnet\dotnet.exe"

Write-Host "Building MegaStructuresUI mod..." -ForegroundColor Cyan

if (-not (Test-Path $dotnet)) {
    Write-Host "dotnet not found at expected location. Make sure .NET SDK is installed." -ForegroundColor Red
    exit 1
}

& $dotnet build $project -c Debug

if ($LASTEXITCODE -eq 0) {
    Write-Host "`nBuild succeeded!" -ForegroundColor Green
    
    $dll = "MegaStructuresUI\bin\Debug\net472\MegaStructuresUI.dll"
    if (Test-Path $dll) {
        Write-Host "Output: $dll"
    }
} else {
    Write-Host "Build failed." -ForegroundColor Red
}
