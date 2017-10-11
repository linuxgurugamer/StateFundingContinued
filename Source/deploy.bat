

set H=R:\KSP_1.3.1_dev
echo %H%



copy /Y "bin\Debug\StateFunding.dll" "..\GameData\StateFunding\Plugins"
copy /Y StateFunding.version ..\GameData\StateFunding

cd ..\GameData
mkdir "%H%\GameData\StateFunding"
xcopy /y /s StateFunding "%H%\GameData\StateFunding"

