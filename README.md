accupressure-meridian-android

Unity project: build instructions for producing an Android APK / AAB

Overview
- This repository contains a Unity starter project for an Android app that displays acupressure meridians in 3D.
- This README explains how to build an APK (or AAB) locally using Unity Editor or command-line (batch mode).

Recommended Unity version
- Use a Unity LTS release. Recommended: Unity 2022.3 LTS or a compatible LTS version.

Prepare the project
1. Install Unity Editor (Hub) and add the Android Build Support module (including SDK & NDK) for the chosen Unity version.
2. Clone this repository locally:
   git clone https://github.com/muneestsql-dot/accupressure-meridian-android.git
3. Open the project in Unity Editor via Unity Hub.

Set up scenes
- Open File -> Build Settings and ensure at least one scene is added and enabled. The build script uses enabled scenes from Build Settings.
- If you don't have any scenes yet, create one (e.g., Scenes/Main.unity) and add it to Build Settings.

Player Settings (important)
- Edit -> Project Settings -> Player -> Android
  - Company Name / Product Name: set as desired.
  - Package Name (Application Identifier): default is com.muneestsql.accupressure. Change if you have a specific package name.
  - Minimum API Level: recommended 21 (Android 5.0) or higher.
  - Target API Level: use the latest installed Android SDK.

Signing (for release builds)
- For testing, Unity will produce a debug-signed APK automatically.
- For a signed release APK/AAB (required for Play Store), create or use an existing keystore:
  - Player Settings -> Publishing Settings -> Keystore Manager -> Create New or select Existing.
  - Enter keystore path, passwords, and key alias.
- Do NOT commit keystore files or passwords to the repo. Keep them private.

Build options
Option A: Build from Unity Editor (GUI)
- In Unity Editor you can use the menu: Build -> Build Android APK (added by the BuildScript) to produce an APK, or Build -> Build Android AAB for an app bundle.
- Alternatively use File -> Build Settings and choose "Build" or "Build and Run".

Option B: Build via command-line (batch mode)
- The repository includes a build script at Assets/Editor/BuildScript.cs with two methods exposed as menu items and callable from command-line.

Windows example (adjust paths):
"C:\Program Files\Unity\Hub\Editor\<version>\Editor\Unity.exe" -batchmode -projectPath "C:\path\to\accupressure-meridian-android" -executeMethod BuildScript.BuildAndroidApk -quit -logFile build.log

macOS example (adjust paths):
/Applications/Unity/Hub/Editor/<version>/Unity.app/Contents/MacOS/Unity -batchmode -projectPath "/path/to/accupressure-meridian-android" -executeMethod BuildScript.BuildAndroidApk -quit -logFile build.log

To build an AAB instead of APK from command-line, replace the method with BuildScript.BuildAndroidAab.

Output
- Built files will be placed in Builds/Android/Accupressure.apk (or Accupressure.aab).

Debug vs Release
- Debug APK: no keystore needed; useful for testing on device.
- Release APK/AAB: configure a keystore in Player Settings -> Publishing Settings before building. After building, verify that the artifact is signed as expected.

Automated CI (optional)
- If you later want CI builds (GitHub Actions), you can add a workflow that runs Unity in headless/batch mode. Note: running licensed Unity on CI requires a Unity license activation file or a Unity Personal license activation; follow Unity's licensing and the "unity-builder" GitHub Action documentation.

Troubleshooting
- Build fails with Android SDK/NDK errors: ensure Unity Android Build Support (SDK/NDK/OpenJDK) is installed for the Editor version.
- No scenes included: ensure at least one scene is enabled in Build Settings.
- If the build log shows errors, open the Editor and check Console for compile errors; fix script errors before batch builds.

Need help?
- Reply here with any build errors or the build.log contents and I will help diagnose.
