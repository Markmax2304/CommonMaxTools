using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace CommonMaxTools.UI
{
    public abstract class ItemListControl<T, D> : MonoBehaviour where T : MonoBehaviour
    {
        public event Action<T, D> OnItemAdded;
        public event Action<T> OnItemRemoved;
        public event Action OnAddItemPressed;

        [SerializeField] private T itemPrefab;
        [SerializeField] private Button addItemPrefab;

        protected Transform ownTransform;

        protected List<T> items;
        protected Button addItemButton;

        protected virtual void Start()
        {
            ownTransform = transform;
            items = new List<T>();

            for (int i = 0; i < ownTransform.childCount; i++)
            {
                Destroy(ownTransform.GetChild(i).gameObject);
            }

            addItemButton = Instantiate(addItemPrefab, ownTransform);
            addItemButton.onClick.AddListener(() =>
            {
                OnAddItemPressed?.Invoke();
            });
        }

        public T AddItem(D data)
        {
            var item = Instantiate(itemPrefab, ownTransform);
            items.Add(item);

            // add button must always stay the last of items
            addItemButton.transform.SetAsLastSibling();

            OnItemAdded?.Invoke(item, data);

            return item;
        }

        public void RemoveItem(T item)
        {
            if (!items.Contains(item))
                return;

            OnItemRemoved?.Invoke(item);

            Destroy(item.gameObject);
            items.Remove(item);
        }

        public void Clear()
        {
            foreach (var item in items)
            {
                Destroy(item.gameObject);
            }

            items.Clear();
        }
    }
}