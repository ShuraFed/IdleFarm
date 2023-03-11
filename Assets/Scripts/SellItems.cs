using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellItems : MonoBehaviour, IInteractable
{
    [SerializeField]
    private float timeBetweenSells=1f;
    private bool canSell=true;

    private void OnEnable()
    {
        ItemsCollector.ItemSold += ItemsCollector_ItemSold;
    }
    private void OnDisable()
    {
        ItemsCollector.ItemSold-= ItemsCollector_ItemSold;
    }

    private void ItemsCollector_ItemSold(object sender, System.EventArgs e)
    {
        canSell = false;
        StartCoroutine(GetReadyToSell());
    }

    private void OnTriggerStay(Collider other)
    {
        ItemsCollector itemsCollector;
        if (other.CompareTag("Player"))
        {
            itemsCollector = other.GetComponentInChildren<ItemsCollector>();
            if (itemsCollector != null&&canSell)
            {
               itemsCollector.TryRemoveItem(transform);
            }
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        other.GetComponent<Movement>().Sell(true);
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        other.GetComponent<Movement>().Sell(false);
    //    }
    //}


    public void Interact()
    {
       // throw new System.NotImplementedException();
    }

    IEnumerator GetReadyToSell()
    {
       yield return new WaitForSeconds(timeBetweenSells);
        canSell = true;
    }

}
