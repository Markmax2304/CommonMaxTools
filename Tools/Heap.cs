using System;

namespace CommonMaxTools.Tools
{
    /// <summary>
    /// Represents wrapper on regular array, that can insert items as sorted by rules, that defined by IHeapItem interface
    /// </summary>
    public class Heap<T> where T : IHeapItem<T>
    {
        #region Fields

        private readonly T[] items;
        private int currentItemsCount = 0;

        #endregion Fields

        #region Properties

        public int Count { get { return currentItemsCount; } }

        #endregion Properties

        #region Constructors

        public Heap(int maxHeapSize)
        {
            items = new T[maxHeapSize];
        }

        #endregion Constructors

        #region Public Methods

        public void Add(T item)
        {
            item.HeapIndex = currentItemsCount;
            items[currentItemsCount++] = item;
            SortUp(item);
        }

        public T RemoveFirst()
        {
            T firstItem = items[0];
            currentItemsCount--;
            items[0] = items[currentItemsCount];
            items[0].HeapIndex = 0;
            SortDown(items[0]);
            return firstItem;
        }

        public void UpdateItem(T item)
        {
            if (!Contains(item))
                throw new Exception($"{item} isn't contained in the heap");

            SortUp(item);
        }

        public bool Contains(T item)
        {
            return currentItemsCount > item.HeapIndex && items[item.HeapIndex].Equals(item);
        }

        public void Clear()
        {
            currentItemsCount = 0;
        }

        #endregion Public Methods

        #region Private Methods

        private void SortUp(T item)
        {
            do
            {
                int parentIndex = (item.HeapIndex - 1) / 2;
                T parentItem = items[parentIndex];

                if (item.CompareTo(parentItem) > 0)
                    Swap(item, parentItem);
                else
                    break;
            }
            while (true);
        }

        private void SortDown(T item)
        {
            while (true)
            {
                int childIndexLeft = item.HeapIndex * 2 + 1;
                int childIndexRight = item.HeapIndex * 2 + 2;
                int swapIndex;

                if (childIndexLeft < currentItemsCount)
                {
                    swapIndex = childIndexLeft;

                    if (childIndexRight < currentItemsCount
                        && items[childIndexLeft].CompareTo(items[childIndexRight]) < 0)
                    {
                        swapIndex = childIndexRight;
                    }

                    if (item.CompareTo(items[swapIndex]) < 0)
                        Swap(item, items[swapIndex]);
                    else
                        break;
                }
                else
                    break;
            }
        }

        private void Swap(T item1, T item2)
        {
            items[item1.HeapIndex] = item2;
            items[item2.HeapIndex] = item1;

            int itemAIndex = item1.HeapIndex;
            item1.HeapIndex = item2.HeapIndex;
            item2.HeapIndex = itemAIndex;
        }

        #endregion Private Methods
    }

    public interface IHeapItem<T> : IComparable<T>
    {
        int HeapIndex { get; set; }
    }
}
