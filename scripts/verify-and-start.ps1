#Script para preparar el entorno de desarrollo y ejecutar la aplicación y hacer pruebas antes de iniciar.

Write-Host "Iniciando CustomerOrderSystem..." -ForegroundColor Cyan

Write-Host "`n1. Ejecutando pruebas unitarias e integrales..." -ForegroundColor Yellow
dotnet test

if ($LASTEXITCODE -ne 0) {
    Write-Host "`nError: Las pruebas han fallado. No se iniciará la aplicación." -ForegroundColor Red
    exit $LASTEXITCODE
}

Write-Host "`n2. Iniciando la aplicación..." -ForegroundColor Yellow
dotnet run --project src/CustomerOrderSystem.Presentation
