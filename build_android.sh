# 您可以通过setEnv函数设置原子间传递的参数
# setEnv "FILENAME" "package.zip"
# 然后在后续的原子的表单中使用${FILENAME}引用这个变量

#project目录
PROJECT_PATH=`pwd`

echo "Start Build Unity to Android"
if
	##ProjectTool.BuildForAndroid 编译入口函数
/Applications/Unity/Hub/Editor/2019.4.40f1/Unity.app/Contents/MacOS/Unity -batchmode -projectPath $PROJECT_PATH -executeMethod ProjectTool.BuildForAndroid -quit -logFile $PROJECT_PATH/log/android.log

then
	cat $PROJECT_PATH/log/android.log
else
	cat $PROJECT_PATH/log/android.log
	exit 1
fi