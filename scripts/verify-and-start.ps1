#Script para preparar el entorno de desarrollo y ejecutar la aplicación y hacer pruebas antes de iniciar.

Write-Host "Iniciando CustomerOrderSystem..." -ForegroundColor Cyan

Write-Host "`n1. Ejecutando pruebas unitarias e integrales..." -ForegroundColor Yellow
dotnet test

if ($LASTEXITCODE -ne 0) {
    Write-Host "`nError: Las pruebas han fallado. No se iniciará la aplicación." -ForegroundColor Red
    exit $LASTEXITCODE
}

Write-Host "`n2. Iniciando la aplicación..." -ForegroundColor Yellow
Write-Host "Nota: La raíz (/) redirige automáticamente a Swagger." -ForegroundColor Gray

Start-Job -ScriptBlock { 
    Start-Sleep -Seconds 5
    Start-Process "https://localhost:7188"
} | Out-Null

dotnet run --project src/CustomerOrderSystem.Presentation
