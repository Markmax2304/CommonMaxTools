using System;
using System.Collections.Generic;

using UnityEngine;

namespace CommonMaxTools.PoolSystem
{
    public class PoolManager<T> where T : struct
    {
        #region Fields

        private readonly PoolSettings<T> _poolSettings;

        private readonly Dictionary<T, PoolStorage> _pools;

        #endregion Fields

        #region Constructors

        public PoolManager(PoolSettings<T> settings)
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("T must be enumaration.");

            _poolSettings = settings;
            _pools = new Dictionary<T, PoolStorage>();

            InitializePools();
        }

        #endregion Constructors

        #region Public Methods

        public PoolStorage GetPoolStorage(T type)
        {
            if (_pools.ContainsKey(type))
                return _pools[type];

            throw new ArgumentException($"There is no Pool storage for type {type}", nameof(type));
        }

        public void ReturnAllObjectsToPool()
        {
            // TODO
        }

        #endregion Public Methods

        #region Private Methods

        private void InitializePools()
        {
            foreach(T type in Enum.GetValues(typeof(T)))
            {
                if (_poolSettings.HasPool(type))
                {
                    PoolInfo info = _poolSettings.GetPoolInfo(type);
                    // TODO: add parent transform
                    //_pools[type] = new PoolStorage(info);
                }
            }
        }

        #endregion Private Methods
    }
}
