# get path to directory where script is located
MY_PATH="`dirname \"$0\"`"
# get absolute path to solution itself
CREATEPROJECTPATH="`( cd \"$MY_PATH/..\" && pwd )`"
if [ -z "$CREATEPROJECTPATH" ] ; then
  # error; for some reason, the path is not accessible
  # to the script (e.g. permissions re-evaled after suid)
  exit 1  # fail
fi

# read paths from config
UNITYDIR=`LC_ALL=en_US.utf8 grep -Po '(?<=\<UnityDir\>).*(?=\</UnityDir\>)' "$CREATEPROJECTPATH/Solution.targets"`
VALHEIMPATH=`LC_ALL=en_US.utf8 grep -Po '(?<=\<GameDir\>).*(?=\</GameDir\>)' "$CREATEPROJECTPATH/Solution.targets"`
UNITYPATH="$UNITYDIR\Unity.exe"

# convert paths to unix format
VALHEIMPATH=$(cygpath.exe -u "$VALHEIMPATH")
UNITYPATH=$(cygpath.exe -u "$UNITYPATH")


echo "--------------------------------CALCULATED DATA--------------------------------------------"
# echo calculated variables for easier script debug
echo "UNITYPATH           =" $UNITYPATH
echo "CREATEPROJECTPATH   =" $CREATEPROJECTPATH
echo "VALHEIMPATH         =" $VALHEIMPATH
echo ""
echo "-----------------------------PUT VERSION NUMBER--------------------------------------------"
version=`LC_ALL=en_US.utf8 grep -Po '(?<="version_number": )".*"(?=,)' "$CREATEPROJECTPATH/manifest.json"`
echo $version
sed -i "s/\"1.0.0\"/$version/" "$CREATEPROJECTPATH/Plugin/Properties/AssemblyInfo.cs"
sed -i "s/\"1.0.0\"/$version/" "$CREATEPROJECTPATH/Plugin/Plugin.cs"
echo ""
echo "-----------------------COPY ASSEMBLIES TO UNITTY PROJECT-----------------------------------"
cp "$VALHEIMPATH"/valheim_Data/Managed/assembly_*.dll "$CREATEPROJECTPATH"/Assets/Assemblies -v
echo ""
echo "----------------------------GENERATE ASSET BUNDLE------------------------------------------"
# execute our pipeline script in batch mode of Unity
exec "$UNITYPATH" -batchmode -quit -projectPath "$CREATEPROJECTPATH" -executeMethod Pipeline.BuildAssetBundles -nographics -stackTraceLogType Full -disable-gpu-skinning