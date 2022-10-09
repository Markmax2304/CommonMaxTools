using UnityEngine;

using CommonMaxTools.Attributes;

namespace CommonMaxTools.PoolSystem
{
    public class PoolableObject : MonoBehaviour
    {
        #region Fields

        [AutoAssign]
        private GameObject _gameObject;
        [AutoAssign]
        private Transform _transform;

        private Transform _parent;

        #endregion Fields

        #region Properties

        public bool IsAccessible => !_gameObject.activeInHierarchy;

        public Transform Transform => _transform;

        #endregion Properties

        #region Public Methods

        internal void Initialize(Transform parent)
        {
            _parent = parent;
            ReturnToPool();
        }

        internal void Activate()
        {
            _gameObject.SetActive(true);
        }

        public void ReturnToPool()
        {
            _transform.parent = _parent;
            _gameObject.SetActive(false);
        }

        #endregion Public Methods
    }
}
