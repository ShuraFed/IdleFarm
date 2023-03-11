using DG.Tweening;
using UnityEngine;

public class Resource : MonoBehaviour,IInteractable
{
    [SerializeField]
    protected ResourceSO resourceSO;
    [SerializeField]
    protected int amountOfActionsBeforeGathered;
    [SerializeField]
    protected int amountOfResourcesToSpawn;
    [SerializeField]
    protected ParticleSystem particleSystemOnAction;
    protected int amountOfActions;

    private void Start()
    {
        amountOfActions = amountOfActionsBeforeGathered;
    }

    public virtual void Interact()
    {
        throw new System.NotImplementedException();
    }

    protected virtual void SpawnResources()
    {
        for (int i = 0; i < amountOfResourcesToSpawn; i++)
        {
            var objTransform = Instantiate(resourceSO.objPrefab, transform.position, Quaternion.identity).transform;
            objTransform.DOJump(GetRandomizedXZPosition(objTransform), 2, 1, 1);
        }
    }

    protected Vector3 GetRandomizedXZPosition(Transform objTransform)
    {
        var rX = objTransform.position.x + Random.Range(-1f, 1f);
        var rZ = objTransform.position.z + Random.Range(-1f, 1f);
        return new Vector3(rX, objTransform.position.y, rZ);
    }

}
