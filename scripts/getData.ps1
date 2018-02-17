#!/usr/local/microsoft/powershell/6.0.1/pwsh

$interns = Get-Content ./assets/input.csv | ConvertFrom-Csv | Select-Object Name, SpeakingDay

$interns | Where-Object {$_.Name -ne 'Varsha'} | ConvertTo-Json | Out-File -Force './assets/data.json'