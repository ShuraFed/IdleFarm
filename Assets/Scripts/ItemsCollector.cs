using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class ItemsCollector : MonoBehaviour
{
    public static event EventHandler<ItemPickedEventArgs> OnNumberOfItemsChanged;
    public static event EventHandler<ItemSoldEventArgs> ItemSold;
    public static event EventHandler MaxCapacityReached;

    [SerializeField]
    private int maxNumberOfItems;
    [SerializeField]
    private float yOffsetPerItem;
    [SerializeField]
    private StackFollower stackFollower;

    private int currentNumberOfItems;

    private void Start()
    {
        OnNumberOfItemsChanged?.Invoke(this, new ItemPickedEventArgs {maxCapacity=maxNumberOfItems,currentAmount=currentNumberOfItems});
    }
    private void OnTriggerStay(Collider other)
    {
        Pickable itemToCollect;
        if (other.TryGetComponent(out itemToCollect))
        {
            if (!itemToCollect.isPicked)
            {
                TryAddItem(other.transform);
            }
        }
    }

    public void TryAddItem(Transform itemToAdd)
    {
        if (currentNumberOfItems != maxNumberOfItems)
        {
            currentNumberOfItems++;
            itemToAdd.GetComponent<Pickable>().Picked();
            itemToAdd.SetParent(transform);
            itemToAdd.DOLocalJump(new Vector3(0, yOffsetPerItem * (currentNumberOfItems - 1), 0), 1, 1, 1)
                .OnComplete(() => { stackFollower.children.Add(itemToAdd);});
            itemToAdd.localRotation = Quaternion.identity;
            OnNumberOfItemsChanged?.Invoke(this, new ItemPickedEventArgs { maxCapacity = maxNumberOfItems, currentAmount = currentNumberOfItems });
        }
        else
        {
            MaxCapacityReached?.Invoke(this,EventArgs.Empty);
        }
    }

    public void TryRemoveItem(Transform newParent)
    {
        if (currentNumberOfItems > 0)
        {
            currentNumberOfItems--;
            Transform item = transform.GetChild(transform.childCount - 1);
            stackFollower.children.Remove(item);
            item.transform.SetParent(newParent);
            item.DOLocalMove(Vector3.zero, 1).OnComplete(() =>
            {
                Destroy(item.gameObject, 1);
            });
            OnNumberOfItemsChanged?.Invoke(this, new ItemPickedEventArgs { maxCapacity = maxNumberOfItems, currentAmount = currentNumberOfItems });
            ItemSold?.Invoke(this, new ItemSoldEventArgs { cost = item.GetComponent<Pickable>().resourceSO.cost });
        }
    }
}

public class ItemPickedEventArgs : EventArgs
{
    public int maxCapacity;
    public int currentAmount;
}

public class ItemSoldEventArgs : EventArgs
{
    public int cost;
}