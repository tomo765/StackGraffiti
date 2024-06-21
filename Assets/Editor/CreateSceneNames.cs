using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

/// <summary> 参考サイト https://www.mum-meblog.com/entry/research/scene_names_editor_extension </summary>

public class CreateSceneNames : EditorWindow
{
    private static string ScriptsFolderPath => Application.dataPath + "/Scripts";

    [MenuItem("Editor/Generate Scene Names Class")]
    private static void GenerateSceneNamesClass()
    {
        Debug.Log("GenerateSceneNamesClass is executing...");
        List<string> sceneNames = GetSceneNames();
        List<string> stageNames = GetStageNames();
        StringBuilder log = new StringBuilder();
        foreach (var scene in sceneNames)
        {
            log.Append($"{scene}\n");
        }
        Debug.Log($"Available scene names are\n{log}");
        CreateSceneNamesClass(sceneNames, stageNames);
    }

    private static void CreateSceneNamesClass(List<string> sceneNames, List<string> stageNames)
    {
        if (Directory.Exists(ScriptsFolderPath + "/Config") == false)
        {
            Debug.Log("Scripts/Config directory not found. Create new...");
            Directory.CreateDirectory(ScriptsFolderPath + "/Config");
        }

        if (File.Exists(ScriptsFolderPath + "/Config/SceneNames.cs") == false)
        {
            Debug.Log("Scripts/Config/SceneNames.cs not found. Create new...");
            using (var sr = File.Create(ScriptsFolderPath + "/Config/SceneNames.cs"))
            {
            }
        }
        using (var sr = new StreamWriter(ScriptsFolderPath + "/Config/SceneNames.cs"))
        {
            sr.Write(GenerateClassContent(sceneNames, stageNames));
        }
        AssetDatabase.Refresh();
    }

    private static string GenerateClassContent(List<string> sceneNames, List<string> stageNames)
    {
        StringBuilder content = new();
        content.Append("namespace Config {\n");
        content.Append("    public class SceneNames {");
        foreach (var sceneName in sceneNames)
        {
            content.Append($"\n        public static readonly string {sceneName} = \"{sceneName}\";");
        }

        content.Append("\n\n        public static readonly string[] m_StageNames = {");
        foreach (var stageName in stageNames)
        {
            content.Append($"\n         \"{stageName}\"");
            if (stageName != stageNames[stageNames.Count - 1]) { content.Append(','); }
        }
        content.Append("\n        };");
        content.Append("\n    }");

        content.Append("\n}");
        return content.ToString();
    }

    public static List<string> GetSceneNames() => EditorBuildSettings.scenes
                                                  .Where(scene => scene.enabled)
                                                  .Select(scene => Path.GetFileNameWithoutExtension(scene.path))
                                                  .Where(sceneName => !sceneName.Contains("Main_Stage"))
                                                  .ToList();

    public static List<string> GetStageNames() => EditorBuildSettings.scenes
                                                  .Where(scene => scene.enabled)
                                                  .Select(scene => Path.GetFileNameWithoutExtension(scene.path))
                                                  .Where(sceneName => sceneName.Contains("Main_Stage"))
                                                  .ToList();

}