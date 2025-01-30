using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Windows;

public class AutoBuild : MonoBehaviour
{   //현재 프로젝트는 windons이기에 추후에 Android 대신 windons로 변경해야함
    static string[] SCENES = FindEnableEditorScenes();
    static string TARGET_DIR = "찾아갈 폴더 이름";
    static string APP_NAME = "앱 이름";

    static int BUILD = 0;

    static string[] FindEnableEditorScenes()
    {
        List<string> editorscenes = new List<string>();

        foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
        {
            if (!scene.enabled)
                continue;

            editorscenes.Add(scene.path);
        }

        return editorscenes.ToArray();
    }

    [MenuItem("Custom/Version/CodeUp",false,1)]

    static void CodeUp()
    {
        int code = PlayerSettings.Android.bundleVersionCode;

        code += 1;

        PlayerSettings.Android.bundleVersionCode = code;
    }

    [MenuItem("Custom/Build/Android")]

    static void AndroidBuild()
    {
        string buildpath = TARGET_DIR + "/Android/";

        Directory.CreateDirectory(buildpath);

        PlayerSettings.companyName = "";
        PlayerSettings.productName = "";

        PlayerSettings.Android.keystoreName = Application.dataPath + "/";   //키 스토어

        PlayerSettings.Android.keystorePass = "";
        PlayerSettings.Android.keyaliasName = "";
        PlayerSettings.Android.keyaliasPass = "";

        PlayerSettings.bundleVersion = Application.version;

        string filename = APP_NAME + ".apk";

        GenericBuild(SCENES, buildpath + filename, BuildTarget.Android, BuildOptions.None);
    }

    static void GenericBuild(string[] scenes, string filename, BuildTarget buildTarget, BuildOptions buildOptions)
    {
        EditorUserBuildSettings.SwitchActiveBuildTarget(buildTarget);

        BuildPipeline.BuildPlayer(scenes, filename, buildTarget, buildOptions);
    }
}
