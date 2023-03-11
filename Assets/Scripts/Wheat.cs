using DG.Tweening;
using UnityEngine;

public class Wheat : Resource
{
    [SerializeField]
    private MeshRenderer meshRenderer;
    [SerializeField]
    private Color wheatReadyColor;
    [SerializeField]
    private Color wheatCutColor;
    [SerializeField]
    private Transform objectToGrow;
    [SerializeField]
    private float growTime = 10f;
    private bool isCut;

    public override void Interact()
    {
        if (!isCut)
        {
            amountOfActions--;
            objectToGrow.DOLocalMoveY((float)amountOfActions /amountOfActionsBeforeGathered - 1, 0.5f);
            particleSystemOnAction.Play();
        }
        if (amountOfActions <= 0)
        {
           SpawnResources();
            isCut = true;
            meshRenderer.material.color = wheatCutColor;
            amountOfActions = amountOfActionsBeforeGathered;
            Grow();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!isCut)
            {
                other.GetComponentInChildren<AnimatorController>().SetState(AnimatorController.State.Mow);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Scythe"))
        {
            Interact();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponentInChildren<AnimatorController>().SetState(AnimatorController.State.Idle);
        }
    }

    private void Grow()
    {
        objectToGrow.DOMoveY(0, 2).OnComplete(() => { isCut = false; }).SetDelay(growTime);
        objectToGrow.DOMoveY(-0.9f, 1f).SetDelay(0.5f);
        meshRenderer.material.DOColor(wheatReadyColor, 2).SetDelay(growTime);
    }

}
