using System.IO;
using System.Text;
using UnityEngine;

public static class LocalizeGenerates
{
    private static string CSVSaveFilePath => Application.dataPath + "/Resources/Localize.csv";
    private static string TextIDPath => Application.dataPath + "/Scripts/Config/TextID.cs";
    private static string LanguageTypePath => Application.dataPath + "/Scripts/Config/LanguageType.cs";

    public static void GenerateCSVFile(string text)
    {
        using (FileStream fs = File.Create(CSVSaveFilePath)) { }

        using (StreamWriter writer = new StreamWriter(CSVSaveFilePath, false, Encoding.GetEncoding("utf-32")))
        {
            writer.WriteLine(text);
        }
    }

    public static void GenerateTextID(string[] texts)
    {
        foreach(var text in texts)
        {
            Debug.Log(text);
        }

        using (FileStream fs = File.Create(TextIDPath)) { }

        StringBuilder content = new();

        content.AppendLine("///<summary> 自動で生成するクラス。LocalizeGenerates 参照 </summary>");
        content.AppendLine("///<remarks> Editor => Download Localize CSV Data で生成開始する </remarks>");

        content.Append("namespace Config");
        content.Append("\n{");
        content.Append("\n    public enum TextID");
        content.Append("\n    {");

        for (int i = 1; i < texts.Length; i++)
        {
            content.Append($"\n        {texts[i].Split(',')[1]} = {i},");
        }
        content.Append("\n    }");
        content.Append("\n}");

        using (StreamWriter writer = new StreamWriter(TextIDPath, false))
        {
            writer.WriteLine(content.ToString());
        }
    }

    public static void GenerateLanguage(string[] texts)
    {
        using (FileStream fs = File.Create(LanguageTypePath)) { }

        StringBuilder content = new();

        content.AppendLine("///<summary> 自動で生成するクラス。LocalizeGenerates 参照 </summary>");
        content.AppendLine("///<remarks> Editor => Download Localize CSV Data で生成開始する </remarks>");

        content.Append("namespace Config");
        content.Append("\n{");
        content.Append("\n    public enum Language");
        content.Append("\n    {");

        for (int i = 2; i < texts.Length; i++)
        {
            content.Append($"\n        {texts[i]} = {i},");
        }
        content.Append("\n    }");
        content.Append("\n}");

        using (StreamWriter writer = new StreamWriter(LanguageTypePath, false))
        {
            writer.WriteLine(content.ToString());
        }
    }
}
