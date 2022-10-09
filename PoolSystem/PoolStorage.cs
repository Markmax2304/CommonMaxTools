using System.Collections.Generic;

using UnityEngine;

namespace CommonMaxTools.PoolSystem
{
    public class PoolStorage
    {
        #region Fields

        private PoolInfo _poolInfo;
        private Transform _parent;

        private List<PoolStorageInstance> _instances;
        private List<PoolableObject> _poolables;

        #endregion Fields

        #region Constructors

        internal PoolStorage(PoolInfo poolInfo, Transform parent)
        {
            _poolInfo = poolInfo;
            _parent = parent;

            _instances = new List<PoolStorageInstance>();
            _poolables = new List<PoolableObject>();

            InitializePoolables();
        }

        #endregion Constructors

        #region Public Methods

        public PoolStorageInstance CreateInstance()
        {
            var instance = new PoolStorageInstance(GetPoolable);
            _instances.Add(instance);

            return instance;
        }

        public void ReturnAllObjectsToPool()
        {
            for(int i = 0; i < _instances.Count; i++)
            {
                _instances[i].ReturnAllObjectsToPool(false);
            }
        }

        #endregion Public Methods

        #region Private Methods

        private void InitializePoolables()
        {
            for(int i = 0; i < _poolInfo.initialObjectsAmount; i++)
                CreateNewPoolable();
        }

        private PoolableObject GetPoolable()
        {
            var poolable = GetFreePoolable();

            if (poolable == null)
                poolable = CreateNewPoolable();

            poolable.Activate();

            return poolable;
        }

        private PoolableObject GetFreePoolable()
        {
            for(int i = 0; i < _poolables.Count; i++)
            {
                if (_poolables[i].IsAccessible)
                    return _poolables[i];
            }

            return null;
        }

        private PoolableObject CreateNewPoolable()
        {
            var poolable = GameObject.Instantiate<PoolableObject>(_poolInfo.poolableObject);
            poolable.Initialize(_parent);

            _poolables.Add(poolable);

            return poolable;
        }

        #endregion Private Methods

        #region Nested Types

        internal delegate PoolableObject GetPoolableAction();

        #endregion Nested Types
    }
}
