using UnityEditor;
using UnityEngine;

public class EditorTools
{
    [MenuItem("Tools/ClearData")]
    public static void ClearData()
    {
        PlayerPrefs.DeleteAll();
    }
}
