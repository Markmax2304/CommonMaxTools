using System;
using System.Linq;
using System.Reflection;
using System.Collections;

using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEditor.Experimental.SceneManagement;

using CommonMaxTools.Attributes;

namespace CommonMaxTools.Editor.Utils
{
    public static class ExtendedEditorGUI
    {
        /// <summary>
        /// Handles and show button in Inspector that invokes and performs specified method
        /// </summary>
        public static void ButtonHandle(UnityEngine.Object target, MethodInfo method)
        {
            if(!method.GetParameters().All(p => p.IsOptional))
            {
                Debug.LogError($"Failed to invoke method {method.Name} from gameobject {target.name}. It has a non optional parameters.");
                return;
            }

            var buttonAttribute = method.GetCustomAttribute(typeof(ButtonMethodAttribute), true) as ButtonMethodAttribute;
            if(buttonAttribute == null)
            {
                Debug.LogError($"Failed to get attribute by type {typeof(ButtonMethodAttribute)} from method");
                return;
            }

            string buttonText = String.IsNullOrWhiteSpace(buttonAttribute.Text) ? 
                ObjectNames.NicifyVariableName(method.Name) : buttonAttribute.Text;

            bool buttonEnabled = true;
            if(method.ReturnType == typeof(IEnumerator))
            {
                buttonEnabled &= Application.isPlaying ? true : false;
            }

            EditorGUI.BeginDisabledGroup(!buttonEnabled);

            if (GUILayout.Button(buttonText))
            {
                object[] defaultParams = method.GetParameters().Select(p => p.DefaultValue).ToArray();
                IEnumerator methodResult = method.Invoke(target, defaultParams) as IEnumerator;

                if (!Application.isPlaying)
                {
                    // Set target object and scene dirty to serialize changes to disk
                    EditorUtility.SetDirty(target);

                    PrefabStage stage = PrefabStageUtility.GetCurrentPrefabStage();
                    if (stage != null)
                    {
                        // Prefab mode
                        EditorSceneManager.MarkSceneDirty(stage.scene);
                    }
                    else
                    {
                        // Normal scene
                        EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
                    }
                }
                else if (methodResult != null && target is MonoBehaviour behaviour)
                {
                    behaviour.StartCoroutine(methodResult);
                }
            }

            EditorGUI.EndDisabledGroup();
        }
    }
}
