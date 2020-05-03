using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Animator animator;
    Rigidbody rigdbody;
    Vector3 movement;
    Portal portal;

    private bool Jump;

    public float speed;

    public int JumpPower;

    float MoveHorizontal;
    float MoveVertical;

    bool isAttacking = false;
    bool isJumping = false;

    // Start is called before the first frame update
    void Awake()
    {
        rigdbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        Jump = false;
    }

    // Update is called once per frame
    void Update()
    {
        MoveHorizontal= Input.GetAxis("Horizontal"); 
        MoveVertical = Input.GetAxis("Vertical");

        Keyinput();
        AnimationUpdate();  

        if (this.transform.position.y < -10)
        {
            this.transform.position = new Vector3(0,0,-3);
        }
    }

    void FixedUpdate()
    {
        Run();
    }

    void AnimationUpdate()
    {
        if (MoveHorizontal == 0 && MoveVertical == 0)
        {
            animator.SetBool("isRunning", false);
        }
        else
        {
            animator.SetBool("isRunning", true);
        }

        if (isAttacking == true)
        {
            animator.SetBool("isAttacking", true);
        }
        else
        {
            animator.SetBool("isAttacking", false);
        }

        if (isJumping == true)
        {
            animator.SetBool("isJumping", true);
        }
        else
        {
            animator.SetBool("isJumping", false);
        }


    }

    void Keyinput()
    {
        if (Input.GetMouseButton(0))
        {
            isAttacking = true;
        }
        else
        {
            isAttacking = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!Jump)
            {
                isJumping = true;
                Jump = true;
                rigdbody.AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
            }
        }
        else
        {
            isJumping = false;
            //Jump = false;

            return;
        }
    }

    void Run()
    {
        movement.Set(MoveHorizontal, 0, MoveVertical);
        movement = (transform.right * MoveHorizontal * speed * Time.deltaTime
            + transform.forward * MoveVertical * speed * Time.deltaTime);
        rigdbody.MovePosition(transform.position + movement);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Jump = false;
        }
    }

}
