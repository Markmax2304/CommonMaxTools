using UnityEditor;

namespace CommonMaxTools.Editor.Utils
{
    public static class AssetUtility
    {
        /// <summary>
        /// Finds and loads specified unity object, for example ScriptableObject, in project
        /// </summary>
        /// <typeparam name="T">Specify what type of object should be found</typeparam>
        /// <param name="unique">Specify this object should be unique or not. 
        /// If true, but here were found more than one - return null, otherwise - return first found object.</param>
        public static T LoadAsset<T>(bool unique) where T : UnityEngine.Object
        {
            var guids = AssetDatabase.FindAssets($"t:{typeof(T).Name}");

            bool valid = guids.Length > 0 && (!unique || guids.Length == 1);
            if (!valid)
                return null;

            string settingsPath = AssetDatabase.GUIDToAssetPath(guids[0]);
            return AssetDatabase.LoadAssetAtPath<T>(settingsPath);
        }
    }
}
