using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Movement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float rotateSpeed;

    private CharacterController characterController;
    private Joystick joystick;
    private float inputX;
    private float inputZ;
    private AnimatorController animatorController;

    private void Awake()
    {
        joystick = FindObjectOfType<Joystick>();
        characterController = GetComponent<CharacterController>();
        animatorController = GetComponentInChildren<AnimatorController>();
    }
    private void Update()
    {
        inputX = joystick.Horizontal;
        inputZ = joystick.Vertical;
        if (inputX != 0 || inputZ != 0)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(new Vector3(inputX, 0, inputZ)), Time.deltaTime * rotateSpeed);
            animatorController.SetState(AnimatorController.State.Move);
        }
        else
        {
            animatorController.SetState(AnimatorController.State.Stay);
        }

    }

    private void FixedUpdate()
    {
        characterController.Move(new Vector3(inputX,0,inputZ)*moveSpeed*Time.fixedDeltaTime);
    }
}
