using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    public float speed;
    public float jumpPower;

    private Vector3 moveVec;
    private float gravity;

    private CharacterController controll;
    private Animator anim;

    void Start()
    {
        controll = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        CharacterMove();
        Gravity();
    }

    private void CharacterMove()
    {
        if(controll.isGrounded){
            moveVec = Vector3.zero;
            moveVec.x = Input.GetAxis("Horizontal") * speed;
            moveVec.z = Input.GetAxis("Vertical") * speed;
            
            if(moveVec.x != 0 || moveVec.z != 0) 
                anim.SetBool("Move", true);
            else 
                anim.SetBool("Move", false);

            if(Vector3.Angle(Vector3.forward, moveVec)>1f || Vector3.Angle(Vector3.forward, moveVec) == 0)
            {
                Vector3 dir = Vector3.RotateTowards(transform.forward, moveVec, speed, 0.0f);
                transform.rotation = Quaternion.LookRotation(dir);
            }
        }
        
        moveVec.y = gravity;

        controll.Move(moveVec * Time.deltaTime);
    }

    private void Gravity()
    {
        if (!controll.isGrounded) 
            gravity -=20f * Time.deltaTime;
        else 
            gravity = -1f; 
        
        if(Input.GetKeyDown(KeyCode.Space) && controll.isGrounded)
        {
            gravity = jumpPower;
            anim.SetTrigger("Jump");
        }
    }

}
