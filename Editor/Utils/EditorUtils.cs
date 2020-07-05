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
        #region Auto Assign Utility

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

        #endregion Auto Assign Utility

        #region Public Methods

        /// <summary>
        /// Return a collection of <see cref="MethodInfo"/> by specified attribute type for target object
        /// </summary>
        public static IEnumerable<MethodInfo> GetMethodsInfoByAttributes<T>(UnityEngine.Object target) where T : Attribute
        {
            var methods = target.GetType()
                .GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static)
                .Where(m => m.IsDefined(typeof(T)));

            return methods;
        }

        #endregion Public Methods

        #region Nested Types

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

        #endregion Nested Types
    }
}

#endif
