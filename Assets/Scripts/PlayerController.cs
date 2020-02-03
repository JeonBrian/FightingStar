
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Fighter ownerFighter;
    public KeyCode forward;
    public KeyCode back;
    public KeyCode jump;
    public KeyCode crouch;
    public KeyCode punch1;
    public KeyCode punch2;
    public KeyCode kick1;
    public KeyCode kick2;
    public KeyCode kick9;

    public float speed = 2;

    float gravity = 8;

    CharacterController controller;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ownerFighter.isNeutral)
        {
            moveCharacter();
        }
    }

    private void moveCharacter()
    {
        Vector3 moveDir = Vector3.zero;
        MoveState moveState = ParseInput();

        // Clear bools 
        anim.SetBool("WalkF", false);
        anim.SetBool("WalkB", false);
        anim.SetBool("JumpN", false);
        anim.SetBool("Crouch", false);

        // Depending on input, perform action
        switch (moveState)
        {
            case MoveState.Forward:
                anim.SetBool("WalkF", true);
                moveDir = new Vector3(0, 0, 1);
                moveDir *= speed;
                moveDir = transform.TransformDirection(moveDir);
                break;
            case MoveState.Back:
                anim.SetBool("WalkB", true);
                moveDir = new Vector3(0, 0, -1);
                moveDir *= speed;
                moveDir = transform.TransformDirection(moveDir);
                break;
            case MoveState.Jump:
                anim.SetBool("JumpN", true);
                break;
            case MoveState.Crouch:
                anim.SetBool("Crouch", true);
                break;
            default:
                break;
        }

        // Move character in space
        // Debug.Log("Move: " + moveState);
        if (anim.GetBool("IsNeutral"))
        {
            controller.Move(moveDir * Time.deltaTime);
        }
    }

    private MoveState ParseInput()
    {
        MoveState ms = MoveState.Neutral;
        // Walk
        if (Input.GetKey(forward))
        {
            ms = MoveState.Forward;
        }
        if (Input.GetKeyUp(forward))
        {
            ms = MoveState.Neutral;
        }

        if (Input.GetKey(back))
        {
            ms = MoveState.Back;
        }
        if (Input.GetKeyUp(back))
        {
            ms = MoveState.Neutral;
        }

        if (Input.GetKey(jump))
        {
            ms = MoveState.Jump;
        }
        if (Input.GetKeyUp(jump))
        {
            ms = MoveState.Neutral;
        }

        if (Input.GetKey(crouch))
        {
            ms = MoveState.Crouch;
        }
        if (Input.GetKeyUp(crouch))
        {
            ms = MoveState.Neutral;
        }

        return ms;
    }
}
