using System.Collections.Generic;

using UnityEngine;

namespace CommonMaxTools.PoolSystem
{
    public class PoolStorageInstance
    {
        #region Fields

        private PoolStorage.GetPoolableAction _getPoolableAction;

        private List<PoolableObject> _activePoolables;

        #endregion Fields

        #region Constructors

        internal PoolStorageInstance(PoolStorage.GetPoolableAction getPoolableAction)
        {
            _getPoolableAction = getPoolableAction;

            _activePoolables = new List<PoolableObject>();
        }

        #endregion Constructors

        #region Public Methods

        public T GetPoolable<T>() where T : MonoBehaviour
        {
            return GetPoolable().GetComponent<T>();
        }

        public PoolableObject GetPoolable()
        {
            UpdateStatusOfPoolables();

            var poolable = _getPoolableAction();
            _activePoolables.Add(poolable);

            return poolable;
        }

        public void ReturnAllObjectsToPool(bool removeStorageInstance)  // TODO
        {
            UpdateStatusOfPoolables();

            for(int i = 0; i < _activePoolables.Count; i++)
                _activePoolables[i].ReturnToPool();

            _activePoolables.Clear();
        }

        #endregion Public Methods

        #region Private Methods

        private void UpdateStatusOfPoolables()
        {
            for(int i = 0; i < _activePoolables.Count; i++)
            {
                if (_activePoolables[i].IsAccessible)
                    _activePoolables.RemoveAt(i--);
            }
        }

        #endregion Private Methods
    }
}
