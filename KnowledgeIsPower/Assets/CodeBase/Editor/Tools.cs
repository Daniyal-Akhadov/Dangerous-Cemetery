using UnityEditor;
using UnityEngine;

namespace CodeBase.Editor
{
    public static class Tools
    {
        [MenuItem("Tools/CleanPrefs")]
        public static void CleanPrefs()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }
    }
}