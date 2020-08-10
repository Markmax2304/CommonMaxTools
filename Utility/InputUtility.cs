using System;

using UnityEngine;

namespace CommonMaxTools.Utility
{
    public static class InputUtility
    {
        private static Camera mainCamera;

        public static Vector2 WorldMousePosition
        {
            get
            {
                if (mainCamera == null)
                    mainCamera = Camera.main;

                Vector2 screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                return mainCamera.ScreenToWorldPoint(screenPosition);
            }
        }

        public static Vector2 GetAxisVector2(bool raw)
        {
            float x = raw ? Input.GetAxisRaw(Constants.HorizontalAxisName) : Input.GetAxis(Constants.HorizontalAxisName);
            float y = raw ? Input.GetAxisRaw(Constants.VerticalAxisName) : Input.GetAxis(Constants.VerticalAxisName);

            return new Vector2(x, y);
        }
    }
}
