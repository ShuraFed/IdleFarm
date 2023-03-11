using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    public ResourceSO resourceSO;
    public bool isPicked=true;
    [SerializeField]
    private float timeBeforePickingAvaliable=1f;

    private void Start()
    {
        StartCoroutine(WaitBeforePickingAvaliable());
    }

    public void Picked()
    {
        isPicked= true;
        GetComponent<Collider>().enabled = false;
    }

    IEnumerator WaitBeforePickingAvaliable()
    {
        yield return new WaitForSeconds(timeBeforePickingAvaliable);
        isPicked= false;
    }
}
