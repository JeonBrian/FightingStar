using UnityEngine;

public class AnimatorPositionController : MonoBehaviour
{
    Animator anim;
    CharacterController controller;

    private void Start()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        UpdatePositionToAnimation();
    }

    private void UpdatePositionToAnimation()
    {
        Vector3 moveDir = Vector3.zero;
        moveDir = new Vector3(0, 0, anim.GetFloat("PositionX") * 100);
        moveDir = transform.TransformDirection(moveDir);
        controller.Move(moveDir * Time.deltaTime);
    }
}