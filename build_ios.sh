# 您可以通过setEnv函数设置原子间传递的参数
# setEnv "FILENAME" "package.zip"
# 然后在后续的原子的表单中使用${FILENAME}引用这个变量

# cd ${WORKSPACE} 可进入当前工作空间目录

#配置证书
developmentTeam="FN2V63AD2J"
codeSignIdentity="Apple Development: Created via API (SBUG6Q755M)"
provisioningProfile="56e7c3f0-d7b4-4ed2-91e4-c2b804710bde"

#需要接受3个参数 1、scheme名 2、工程目录 3、工程名字
# cd ${WORKSPACE} 可进入当前工作空间目录
if [ -d ${WORKSPACE}/result ]
then
rm -rf ${WORKSPACE}/result
fi
mkdir -p ${WORKSPACE}/result
cd ${WORKSPACE}


#project目录
PROJECT_PATH=`pwd`
#project名称
PROJECT_NAME="Unity-iPhone.xcodeproj"
#scheme名称
SCHEME_NAME="Unity-iPhone"

#归档文件地址
ARCHIVE_PATH=$PROJECT_PATH/iOSProj/$SCHEME_NAME

echo "Start Build Unity to Xcodeproject"
if
	##ProjectTool.BuildForIOS 编译入口函数
/Applications/Unity/Unity.app/Contents/MacOS/Unity -batchmode -projectPath $PROJECT_PATH -executeMethod ProjectTool.BuildForIOS -quit -logFile $PROJECT_PATH/log/ios.log
# Double run purposely, since the first build always fails the later xcodebuild with "Undefined symbols for architecture arm64"
/Applications/Unity/Unity.app/Contents/MacOS/Unity -batchmode -projectPath $PROJECT_PATH -executeMethod ProjectTool.BuildForIOS -quit -logFile $PROJECT_PATH/log/ios.log

then
	cat $PROJECT_PATH/log/ios.log
else
	cat $PROJECT_PATH/log/ios.log
	exit 1
fi

##取消自动签名
# echo hello
# sed -i '' 's/ProvisioningStyle = Automatic;/ProvisioningStyle = Manual;/' iOSProj/Unity-iPhone.xcodeproj/project.pbxproj
# sed -i '' 's/CODE_SIGN_STYLE = Automatic;/CODE_SIGN_STYLE = Manual;/' iOSProj/Unity-iPhone.xcodeproj/project.pbxproj

#通过archive归档出对应的xcarchive文件
#对应步骤:
#1、清理工程
#2、归档工程
#3、工程名称
#4、设置工程Scheme
#5、设置Debug或者Release模式
#6、归档输出地址
xcodebuild clean \
archive \
-project "$PROJECT_PATH/iOSProj/$PROJECT_NAME" \
-scheme "$SCHEME_NAME" \
-configuration "Debug" \
-archivePath "$ARCHIVE_PATH" \
CODE_SIGN_STYLE="Manual" DEVELOPMENT_TEAM="${developmentTeam}" PRODUCT_BUNDLE_IDENTIFIER_APP="${codeSignIdentity}" PROVISIONING_PROFILE_APP="${provisioningProfile}" ENABLE_BITCODE=NO
echo "--------------------------------------"

#生成ExportOptions.plist文件
cat >> ./iOSProj/ExportOptions.plist <<EOF
<?xml version="1.0" encoding="UTF-8"?>
<!DOCTYPE plist PUBLIC "-//Apple//DTD PLIST 1.0//EN" "http://www.apple.com/DTDs/PropertyList-1.0.dtd">
<plist version="1.0">
<dict>
<key>teamID</key>
<string>FN2V63AD2J</string>
<key>method</key>
<string>development</string>
<key>compileBitcode</key>
<false></false>
<key>provisioningProfiles</key>
<dict>
<key>com.tencent.im.unity.apiexample</key>
<string>56e7c3f0-d7b4-4ed2-91e4-c2b804710bde</string>
</dict>
</dict>
</plist>
EOF

#通过归档文件打包出对应的ipa文件
#对应步骤：
#1、打包命令
#2、归档文件地址
#3、ipa输出地址
#4、ipa打包设置文件地址
xcodebuild -exportArchive \
-archivePath "$ARCHIVE_PATH.xcarchive" \
-exportPath ${WORKSPACE}/result \
-exportOptionsPlist "$PROJECT_PATH/iOSProj/ExportOptions.plist"

ls -ltr ${WORKSPACE}/result