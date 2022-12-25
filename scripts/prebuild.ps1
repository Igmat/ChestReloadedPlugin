param(
    [Parameter(Mandatory)]
    [ValidateSet('Debug','Release')]
    [System.String]$Target,
    
    [Parameter(Mandatory)]
    [System.String]$TargetPath,
    
    [Parameter(Mandatory)]
    [System.String]$TargetAssembly,

    [Parameter(Mandatory)]
    [System.String]$ValheimPath,

    [Parameter(Mandatory)]
    [System.String]$ProjectPath,

    [Parameter(Mandatory)]
    [System.String]$UnityDir,
    
    [System.String]$DeployPath
)

# Make sure Get-Location is the script path
Push-Location -Path (Split-Path -Parent $MyInvocation.MyCommand.Path)

# Test some preliminaries
("$ValheimPath",
 "$(Get-Location)\..\libraries"
) | % {
    if (!(Test-Path "$_")) {Write-Error -ErrorAction Stop -Message "$_ folder is missing"}
}

# Plugin name without ".dll"
$name = "$TargetAssembly" -Replace('.dll')
$UnityPath="$UnityDir\Unity.exe"
$Package="Package"
$unitySuffix="Unity"
$PackagePath="$ProjectPath\$Package"
$UnityProjectPath = "$ProjectPath$unitySuffix"
$UnityAssemblies = "$UnityProjectPath\Assets\Assemblies"
$ValheimManaged = "$ValheimPath\valheim_Data\Managed"
$version = ((Get-Content "$PackagePath\manifest.json") | Select-String -Pattern '(?<="version_number": ").*(?=",)').Matches


Write-Host "--------------------------------CALCULATED DATA--------------------------------------------"
# Write-Host calculated variables for easier script debug
Write-Host "name                = $name"
Write-Host "version             = $version"
Write-Host "UnityPath           = $UnityPath"
Write-Host "UnityProjectPath    = $UnityProjectPath"
Write-Host "UnityAssemblies     = $UnityAssemblies"
Write-Host "ValheimManaged      = $ValheimManaged"
Write-Host ""
Write-Host "-----------------------------PUT VERSION NUMBER--------------------------------------------"
$AssemblyInfo = "$ProjectPath\Properties\AssemblyInfo.cs"
$PluginFile = "$ProjectPath\$name.cs"
(Get-Content $AssemblyInfo).replace("1.0.0", $version) | Set-Content $AssemblyInfo
(Get-Content $PluginFile).replace("1.0.0", $version) | Set-Content $PluginFile
Write-Host ""
Write-Host "-----------------------COPY ASSEMBLIES TO UNITTY PROJECT-----------------------------------"
Copy-Item -Path "$ValheimManaged\assembly_*.dll" -Destination "$UnityAssemblies" -Force
Copy-Item -Path "$ValheimManaged\ConnectedStorage.dll" -Destination "$UnityAssemblies" -Force
Copy-Item -Path "$ValheimManaged\PlayFab.dll" -Destination "$UnityAssemblies" -Force
Copy-Item -Path "$ValheimManaged\PlayFabParty.dll" -Destination "$UnityAssemblies" -Force
Write-Host ""
Write-Host "----------------------------GENERATE ASSET BUNDLE------------------------------------------"
# execute our pipeline script in batch mode of Unity
& "$UnityPath" -batchmode -quit -executeMethod Pipeline.BuildAssetBundles -nographics -stackTraceLogType Full -disable-gpu-skinning -projectPath "$UnityProjectPath"