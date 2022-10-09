using System;
using System.Collections.Generic;

using UnityEngine;

using CommonMaxTools.Attributes;

namespace CommonMaxTools.PoolSystem.Tests
{
    public class PoolSystemTest : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private PoolSettingsTest poolSettings;

        private PoolManager<ItemType> _poolManager;

        #endregion Fields

        #region Public Methods

        [ButtonMethod]
        public void InitializePoolSystem()
        {
            _poolManager = new PoolManager<ItemType>(poolSettings);
        }

        #endregion Public Methods
    }
}
