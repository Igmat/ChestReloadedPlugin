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

$libraries = "$(Get-Location)\..\libraries"
# Test some preliminaries
("$TargetPath",
 "$ValheimPath",
 $libraries
) | % {
    if (!(Test-Path "$_")) {Write-Error -ErrorAction Stop -Message "$_ folder is missing"}
}

# Plugin name without ".dll"
$name = "$TargetAssembly" -Replace('.dll')

# Create the mdb file
$pdb = "$TargetPath\$name.pdb"
if (Test-Path -Path "$pdb") {
    Write-Host "Create mdb file for plugin $name"
    Invoke-Expression "& `"$libraries\Debug\pdb2mdb.exe`" `"$TargetPath\$TargetAssembly`""
}

$Package="Package"
$PackagePath="$ProjectPath\$Package"

# Main Script
Write-Host "Publishing for $Target from $TargetPath"

if ($Target.Equals("Debug")) {
    if ($DeployPath.Equals("")){
      $DeployPath = "$ValheimPath\BepInEx\plugins"
    }
    
    $plug = New-Item -Type Directory -Path "$DeployPath\$name" -Force
    Write-Host "Copy $TargetAssembly to $plug"
    Copy-Item -Path "$TargetPath\$name.dll" -Destination "$plug" -Force
    Copy-Item -Path "$TargetPath\$name.pdb" -Destination "$plug" -Force
    Copy-Item -Path "$TargetPath\$name.dll.mdb" -Destination "$plug" -Force
    Copy-Item -Path "$TargetPath\Translations" -Destination "$plug" -Force -Recurse

    Write-Host "------------------------Make game debuggable---------------------------------------"
    $EmbedRuntime = "$ValheimPath\MonoBleedingEdge\EmbedRuntime\"
    $dllName = "mono-2.0-bdwgc.dll"
    $dllCopiedName = "mono-2.0-bdwgc.original.dll"
    $dllOriginal = "$EmbedRuntime$dllName"
    $dllDebug = "$libraries\Debug\$dllName"
    $dllCopied = "$EmbedRuntime$dllNameCopied"
    if (Test-Path -Path "$dllCopied") {
        Write-Host "Game is already debuggable"
        Write-Host "Original $dllOriginal is already $dllCopiedName"
    } else {
        Rename-Item $dllOriginal $dllCopiedName
        Write-Host "Original $dllOriginal is renamed to $dllCopiedName"
        Copy-Item -Path "$dllDebug" -Destination "$EmbedRuntime" -Force
    }
}

if($Target.Equals("Release")) {
    Write-Host "Packaging for ThunderStore..."

    Write-Host "$PackagePath\$TargetAssembly"
    New-Item -Type Directory -Path "$PackagePath\plugins" -Force
    Copy-Item -Path "$TargetPath\$TargetAssembly" -Destination "$PackagePath\plugins\$TargetAssembly" -Force
    Copy-Item -Path "$TargetPath\Translations" -Destination "$PackagePath\plugins" -Force -Recurse
    Copy-Item -Path "$ProjectPath\README.md" -Destination "$PackagePath\README.md" -Force
    Compress-Archive -Path "$PackagePath\*" -DestinationPath "$TargetPath\$TargetAssembly.zip" -Force
}

Write-Host "----------------------------REMOVE VERSION NUMBER------------------------------------------"
$version = ((Get-Content "$PackagePath\manifest.json") | Select-String -Pattern '(?<="version_number": ").*(?=",)').Matches
Write-Host $version
$AssemblyInfo = "$ProjectPath\Properties\AssemblyInfo.cs"
$PluginFile = "$ProjectPath\$name.cs"
(Get-Content $AssemblyInfo).replace($version, "1.0.0") | Set-Content $AssemblyInfo
(Get-Content $PluginFile).replace($version, "1.0.0") | Set-Content $PluginFile

# Pop Location
Pop-Location