using System;

using UnityEngine;

namespace CommonMaxTools.Editor.Utils
{
    internal class AutoAssignException : Exception
    {
        public AutoAssignException(string message) : base(message)
        {
        }
    }
}
