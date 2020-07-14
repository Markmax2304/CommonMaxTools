using System;

using UnityEngine;

namespace CommonMaxTools.Editor.Tools
{
    public class EditorEventsProcessor : UnityEditor.AssetModificationProcessor
    {
        #region Events

        public static Action OnSaveScene;

        #endregion Events

        static string[] OnWillSaveAssets(string[] paths)
        {
            // Prefab creation enforces SaveAsset and this may cause unwanted dir cleanup
            if (paths.Length == 1 && (paths[0] == null || paths[0].EndsWith(".prefab")))
                return paths;

            //Debug.Log("Save assets is processing");
            OnSaveScene?.Invoke();

            return paths;
        }
    }
}
