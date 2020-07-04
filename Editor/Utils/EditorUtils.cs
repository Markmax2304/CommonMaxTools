#if UNITY_EDITOR

using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

namespace CommonMaxTools.Editor.Utils
{
    public static class EditorUtils
    {
        /// <summary>
        /// Return a collection of fields with related components that is declared with an attribute of type T
        /// </summary>
        public static ComponentField[] GetComponentsByAttribute<T>() where T : Attribute
        {
            var behaviours = GetAllMonobehavioursInScenes();
            var components = new List<ComponentField>();

            foreach(var behaviour in behaviours)
            {
                Type type = behaviour.GetType();
                var matchingFields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                    .Where(field => field.IsDefined(typeof(T), false));

                foreach (var field in matchingFields)
                    components.Add(new ComponentField(field, behaviour));
            }

            return components.ToArray();
        }

        public struct ComponentField
        {
            public readonly FieldInfo Field;
            // maybe use something else
            public readonly Component Component;

            public ComponentField(FieldInfo field, Component component)
            {
                Field = field;
                Component = component;
            }
        }

        /// <summary>
        /// Collect all MonoBehaviour components in all project scenes
        /// </summary>
        /// <returns>Array of Monobehaviours</returns>
        public static MonoBehaviour[] GetAllMonobehavioursInScenes()
        {
            var components = new List<MonoBehaviour>();

            for(int i = 0; i < SceneManager.sceneCount; i++)
            {
                var scene = SceneManager.GetSceneAt(i);
                var rootObjects = scene.GetRootGameObjects();

                foreach(var root in rootObjects)
                {
                    components.AddRange(root.GetComponentsInChildren<MonoBehaviour>(true));
                }
            }

            return components.ToArray();
        }
    }
}

#endif
