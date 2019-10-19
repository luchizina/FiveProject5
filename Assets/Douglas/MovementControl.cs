using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementControl : MonoBehaviour
{
    public Animator animator;
    public bool isGrounded;
    public bool isCrouching;
    private float speed;
    private float w_speed = 0.02f;
    private float r_speed = 0.1f;
    private float c_speed = 0.025f;
    private float rotSpeed = 2f;
    public float jumpHeight;
    Rigidbody rb;
    Animator anim;
    CapsuleCollider col_size;


    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody>();
        col_size = gameObject.GetComponent<CapsuleCollider>();
        isGrounded = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            anim.SetBool("isDancing", true);
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if(isCrouching)
            {
                isCrouching = false;
                anim.SetBool("isCrouching", false);
                col_size.height = 2;
                col_size.center = new Vector3(0, 1, 0);
            }
            else
            {
                isCrouching = true;
                anim.SetBool("isCrouching", true);
                speed = c_speed;
                col_size.height = 1;
                col_size.center = new Vector3(0, 0.5f, 0);
            }
        }

        var z = Input.GetAxis("Vertical") * speed;
        var y = Input.GetAxis("Horizontal") * rotSpeed;
        Debug.Log(y);
        transform.Translate(0, 0, z);
        //transform.Translate(transform.forward);
        transform.Rotate(0, y, 0);

        if(isCrouching)
        {
            speed = c_speed;
            if (Input.GetKey(KeyCode.W))
            {
                anim.SetBool("isWalking", true);
                anim.SetBool("isIdle", false);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                anim.SetBool("isWalking", true);
                anim.SetBool("isIdle", false);
            }
            else
            {
                anim.SetBool("isWalking", false);
                anim.SetBool("isIdle", true);
            }
        }
        else if (!isCrouching)
        {
            speed = w_speed;
            if (Input.GetKey(KeyCode.W))
            {
                anim.SetBool("isWalking", true);
                anim.SetBool("isIdle", false);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                anim.SetBool("isWalking", true);
                anim.SetBool("isIdle", false);
            }
            else
            {
                anim.SetBool("isWalking", false);
                anim.SetBool("isIdle", true);
            }
        }
    }
}
