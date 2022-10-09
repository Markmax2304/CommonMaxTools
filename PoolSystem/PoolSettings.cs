using System;
using System.Collections.Generic;

using UnityEngine;

namespace CommonMaxTools.PoolSystem
{
    public abstract class PoolSettings<T> : ScriptableObject
    {
        public List<PoolInfoWrapper<T>> poolData;

        public virtual PoolInfo GetPoolInfo(T type)
        {
            foreach (var wrapper in poolData)
            {
                if (Compare(type, wrapper.type))
                    return wrapper.data;
            }

            throw new ArgumentException($"This type of poolabe isn't supported by pool settings. Type = {type}");
        }

        public virtual bool HasPool(T type)
        {
            foreach (var wrapper in poolData)
            {
                if (Compare(type, wrapper.type))
                    return true;
            }

            return false;
        }

        protected abstract bool Compare(T first, T second);
    }

    [Serializable]
    public struct PoolInfo
    {
        public PoolableObject poolableObject;
        public int initialObjectsAmount;
    }

    [Serializable]
    public struct PoolInfoWrapper<T>
    {
        public T type;
        public PoolInfo data;
    }
}
