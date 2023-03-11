using UnityEngine;

public class Stone : Resource
{
    private bool isMined;
    public override void Interact()
    {
        if (amountOfActions > 0)
        {
            amountOfActions--;
            particleSystemOnAction.Play();
        }
        if (amountOfActions <= 0)
        {
            SpawnResources();
            isMined = true;
            GetComponent<Renderer>().enabled = false;
            GetComponent<Collider>().enabled = false;
            Destroy(gameObject, 2);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!isMined)
            {
                other.GetComponentInChildren<AnimatorController>().SetState(AnimatorController.State.Mine);
            }
            else
            {
                other.GetComponentInChildren<AnimatorController>().SetState(AnimatorController.State.Idle);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickaxe"))
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

}
