# DSP Mod Build Script
# Usage: .\build.ps1

$project = "DysonHarvester\DysonHarvester.csproj"
$dotnet = "C:\Program Files\dotnet\dotnet.exe"

Write-Host "Building DysonHarvester mod..." -ForegroundColor Cyan

if (-not (Test-Path $dotnet)) {
    Write-Host "dotnet not found at expected location. Make sure .NET SDK is installed." -ForegroundColor Red
    exit 1
}

& $dotnet build $project -c Debug

if ($LASTEXITCODE -eq 0) {
    Write-Host "`nBuild succeeded!" -ForegroundColor Green
    
    $dll = "DysonHarvester\bin\Debug\net472\DysonHarvester.dll"
    if (Test-Path $dll) {
        Write-Host "Output: $dll"
    }
} else {
    Write-Host "Build failed." -ForegroundColor Red
}
