using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatorController : MonoBehaviour
{
    const string IS_MOVING = "IsMoving";
    const string MOW = "Mow";
    const string MINE = "Mine";
    const string CHOP = "Chop";


    private Animator animator;
    public enum State { Idle, Move,Stay, Mow, Chop, Mine }
    private State currentState;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetState(State newState)
    {
        currentState= newState;
        switch (currentState)
        {
            case State.Idle:
                animator.SetBool(IS_MOVING, false);
                animator.SetBool(MOW, false);
                animator.SetBool(MINE, false);
                animator.SetBool(CHOP, false);
                break;
            case State.Move:
                animator.SetBool(IS_MOVING, true);
                break;
            case State.Stay:
                animator.SetBool(IS_MOVING, false);
                break;
            case State.Chop:
                animator.SetBool(CHOP, true);
                break;
            case State.Mine:
                animator.SetBool(MINE, true);
                break;
            case State.Mow:
                animator.SetBool(MOW, true);
                break;
            default:
                break;
        }
    }

}

