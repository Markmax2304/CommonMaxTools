using System;
using System.Collections.Generic;

using UnityEngine;

namespace CommonMaxTools.Extensions
{
    public static class MonoBehaviourExtension
    {
        #region Public Methods

        public static void GetComponentsInChildren(this Component component, Type type, bool includeInactive, int deepLevel, List<Component> array)
        {
            TraverseComponentsInChildren(component.transform, type, includeInactive, deepLevel, 0, array);
        }

        public static Component GetComponentInChildren(this Component component, Type type, bool includeInactive, int deepLevel)
        {
            return TraverseComponentInChildren(component.transform, type, includeInactive, deepLevel, 0);
        }

        #endregion Public Methods

        #region Private Methods

        private static void TraverseComponentsInChildren(Transform transform, Type type, bool includeInactive, int targetLevel, int currentLevel, List<Component> array)
        {
            if (targetLevel == currentLevel)
            {
                array.AddRange(transform.GetComponents(type));
                return;
            }

            for (int i = 0; i < transform.childCount; i++)
            {
                var childTransform = transform.GetChild(i);

                if (includeInactive || childTransform.gameObject.activeInHierarchy)
                    TraverseComponentsInChildren(childTransform, type, includeInactive, targetLevel, currentLevel + 1, array);
            }
        }

        private static Component TraverseComponentInChildren(Transform transform, Type type, bool includeInactive, int targetLevel, int currentLevel)
        {
            if (targetLevel == currentLevel)
            {
                return transform.GetComponent(type);
            }

            for (int i = 0; i < transform.childCount; i++)
            {
                var childTransform = transform.GetChild(i);

                if (includeInactive || childTransform.gameObject.activeInHierarchy)
                {
                    var childComponent = TraverseComponentInChildren(childTransform, type, includeInactive, targetLevel, currentLevel + 1);

                    if (childComponent != null)
                        return childComponent;
                }
            }

            return null;
        }

        #endregion Private Methods
    }
}
