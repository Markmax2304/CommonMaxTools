using UnityEngine;

namespace CommonMaxTools.PoolSystem.Tests
{
    public enum ItemType { Box, Image, Other }

    [CreateAssetMenu(fileName = "Pool Settings", menuName = "Pool/Test/PoolSettings")]
    public class PoolSettingsTest : PoolSettings<ItemType>
    {
        protected override bool Compare(ItemType first, ItemType second)
        {
            return first == second;
        }
    }
}