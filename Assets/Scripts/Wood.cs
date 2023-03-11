using UnityEngine;
using Random = UnityEngine.Random;

public class Wood : Resource
{
    private bool isChopped;
    private Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    public override void Interact()
    {
        if (amountOfActions>0)
        {
            amountOfActions--;
            particleSystemOnAction.Play();
        }
        if (amountOfActions <= 0)
        {
            SpawnResources();
            isChopped= true;
            rb.isKinematic= false;
            rb.AddForce(GameObject.FindGameObjectWithTag("Player").transform.forward,ForceMode.Impulse);
            Destroy(gameObject,2);
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!isChopped)
            {
                other.GetComponentInChildren<AnimatorController>().SetState(AnimatorController.State.Chop);
            }
            else
            {
                other.GetComponentInChildren<AnimatorController>().SetState(AnimatorController.State.Idle);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Axe"))
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
