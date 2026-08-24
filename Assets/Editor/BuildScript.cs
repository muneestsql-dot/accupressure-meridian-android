using UnityEditor;
using UnityEditor.Build.Reporting;
using System.IO;
using System.Collections.Generic;

public class BuildScript {
    private const string defaultPackageName = "com.muneestsql.accupressure";

    [MenuItem("Build/Build Android APK")]
    public static void BuildAndroidApk() {
        BuildAndroid(false);
    }

    [MenuItem("Build/Build Android AAB")]
    public static void BuildAndroidAab() {
        BuildAndroid(true);
    }

    public static void BuildAndroid(bool buildBundle) {
        string[] scenes = GetEnabledScenes();
        if (scenes.Length == 0) {
            UnityEngine.Debug.LogError("No enabled scenes in Build Settings. Add at least one scene.");
            return;
        }

        // Set default package name (can be changed in Player Settings)
        PlayerSettings.applicationIdentifier = defaultPackageName;

        // Ensure output directory exists
        string outputDir = "Builds/Android";
        if (!Directory.Exists(outputDir)) Directory.CreateDirectory(outputDir);

        string filename = buildBundle ? "Accupressure.aab" : "Accupressure.apk";
        string path = Path.Combine(outputDir, filename);

        // Configure build options
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions {
            scenes = scenes,
            locationPathName = path,
            target = BuildTarget.Android,
            options = BuildOptions.None
        };

        // Toggle AAB export
        EditorUserBuildSettings.buildAppBundle = buildBundle;

        BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
        if (report.summary.result == BuildResult.Succeeded) {
            UnityEngine.Debug.Log("Build succeeded: " + path);
        } else {
            UnityEngine.Debug.LogError("Build failed: " + report.summary.result + "\n" + report.summary.totalErrors + " errors");
        }
    }

    public static string[] GetEnabledScenes() {
        List<string> scenes = new List<string>();
        foreach (var scene in EditorBuildSettings.scenes) {
            if (scene.enabled) scenes.Add(scene.path);
        }
        return scenes.ToArray();
    }
}
