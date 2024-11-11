using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

#if UNITY_EDITOR
//https://docs.google.com/spreadsheets/d/1Fy16eGDxAcyUBz0HUrkN-vnVj3x2XiOSiNMQJgEBY14/edit?gid=0#gid=0
public static partial class Localizeations
{
    private const string LocalizeURL = "https://docs.google.com/spreadsheets/d/e/2PACX-1vR15JUawkpLWtGQmq9h6Fdx0jPXpuUjDZG9TmQKTkG5izPQMCoVg9ZUeDXO0PRsUIZ7boQL0CEx95qM/pub?gid=0&single=true&output=csv";

    [MenuItem("Editor/Download Localize CSV Data")]
    private static void DownloadCSV()
    {
        EditorCoroutine.StartEditorCoroutine(DownloadData());

        IEnumerator DownloadData(string url = LocalizeURL)
        {
            using (UnityWebRequest request = UnityWebRequest.Get(url))
            {
                var op = request.SendWebRequest();
                while (!op.isDone) { yield return null; } 

                if (request.error != null || request.result is UnityWebRequest.Result.ConnectionError or UnityWebRequest.Result.ProtocolError)
                {
                    Debug.LogError($"Error: {request.error}");
                    yield break;
                }

                LocalizeGenerates.GenerateCSVFile(request.downloadHandler.text);
                LocalizeGenerates.GenerateTextID(request.downloadHandler.text.Split('\n'));
                LocalizeGenerates.GenerateLanguage(request.downloadHandler.text.Split('\n')[0].Split(','));
                AssetDatabase.Refresh();
            }
        }
    }
}
#endif

public static partial class Localizeations
{
    public static string GetLocalizeText(Config.TextID textID, Config.Language language)
    {
        var csvText = Resources.Load<TextAsset>("Localize").text;
        return csvText.Split('\n')[(int)textID].Split(',')[(int)language];
    }
}