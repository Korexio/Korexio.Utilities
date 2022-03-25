#Requires -Version 7

$ErrorActionPreference = 'Stop'
$WarningPreference = 'Continue'
$ProgressPreference = 'SilentlyContinue'

Push-Location "$PSScriptRoot/.."
try {

    if (Test-Path './Artifacts/') {
        Remove-Item './Artifacts/' -Recurse
    }

    $Solution = Get-Item './Source/Korexio.Utilities.sln'

    dotnet tool update --global dotnet-reportgenerator-globaltool

    dotnet restore $Solution

    dotnet build $Solution --no-restore '-binaryLogger:LogFile=./Artifacts/msbuild.binlog'

    dotnet test $Solution --no-build --results-directory './Artifacts/TestResults/' --logger 'trx' --logger 'html' --collect "XPlat Code Coverage"
  
    reportgenerator "-reports:./Artifacts/TestResults/*/coverage.cobertura.xml" "-targetdir:./Artifacts/CodeCoverage/Summary" "-reporttypes:HtmlSummary"
    reportgenerator "-reports:./Artifacts/TestResults/*/coverage.cobertura.xml" "-targetdir:./Artifacts/CodeCoverage/Full" "-reporttypes:Html_Dark"

}
finally {
    Pop-Location
}
