cd %~dp0
cd ..
SET STANDALONEUNITYPATH="C:\Program Files\Unity\Editor\Unity.exe"
SET UNITYHUBEDITORPATH="C:\Program Files\Unity\Hub\Editor\2019.4.20f1\Editor\Unity.exe"
SET UNITYPATH=%UNITYHUBEDITORPATH%
SET CREATEPROJECTPATH="%CD%"

%UNITYPATH% -batchmode -quit -projectPath %CREATEPROJECTPATH% -executeMethod Pipeline.BuildAssetBundles
