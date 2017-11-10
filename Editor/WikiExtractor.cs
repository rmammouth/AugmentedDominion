using UnityEngine;
using UnityEditor;

public class WikiExtractor : ScriptableObject
{
  [MenuItem("Tools/MyTool/Do It in C#")]
  static void DoIt()
  {
    EditorUtility.DisplayDialog("MyTool", "Do It in C# !", "OK", "");
  }
}