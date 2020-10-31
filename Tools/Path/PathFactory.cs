using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace CommonMaxTools.Tools.Path
{
    public static class PathFactory
    {
        private const int capacity = 10;

        private static Dictionary<PathType, List<IPathable>> paths;

        static PathFactory()
        {
            paths = new Dictionary<PathType, List<IPathable>>();

            foreach(PathType type in Enum.GetValues(typeof(PathType)))
            {
                paths.Add(type, new List<IPathable>());
            }
        }

        public static T CreatePath<T>() where T : IPathable
        {
            Type t = typeof(T);
        }

        public static void DestroyPath(IPathable path)
        {

        }
    }

    public enum PathType { Line, CircleSector };
}
