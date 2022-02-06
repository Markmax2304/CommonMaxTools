namespace CommonMaxTools.Utility
{
    public static class CollectionUtility
    {
        /// <summary>
        /// It makes index to be in range of collection length. 
        /// It works like overflow of byte value, so even if index is negative it will return value as though index is counting from the end of collection. 
        /// For example -1 will be turned into collection lenght - 1
        /// </summary>
        public static int RoundIndex(int rawCollectionIndex, int collectionLength)
        {
            rawCollectionIndex %= collectionLength;

            if (rawCollectionIndex < 0)
                rawCollectionIndex += collectionLength;

            return rawCollectionIndex;
        }
    }
}
