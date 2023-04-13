using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float speed = 5f;
    public float jumpForce = 600;

    private Animator anim;
    private Rigidbody2D rb;
    private bool facingRight = true;
    private bool jump;
    private bool onGround = false;
    private Transform groundCheck;
    private float hforce = 0;
    private bool crouched;
    private bool lookingUp;
    private bool reloading;

    private bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        groundCheck = gameObject.transform.Find("GroundCheck");
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead) {

            onGround = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
            
            if (onGround)
            {
                anim.SetBool("Jump", false);
            }
            
            if (Input.GetButtonDown("Jump") && onGround && !reloading)
            {
                jump = true;
            }
            else if (Input.GetButtonUp("Jump"))
            {
                if(rb.velocity.y > 0)
                {
                    rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
                }
            }

            if (Input.GetButtonDown("Crouch"))
            {
                anim.SetBool("Crouched", true);
            }
            if (Input.GetButtonDown("Reload"))
            {
                anim.SetBool("Reloading", true);
            }

            if (Input.GetButtonDown("Shoot"))
            {
                anim.SetTrigger("Shoot");
            }

            lookingUp = Input.GetButton("Up");
            crouched = Input.GetButton("Down");
            reloading = Input.GetButton("Reload");

            anim.SetBool("LookUp", lookingUp);
            anim.SetBool("Crouched", crouched);
            anim.SetBool("Reloading", reloading);

            if ((crouched || lookingUp || reloading) && onGround)
            {
                hforce = 0;
            }

        }
    }

    private void FixedUpdate()
    {
        if (!isDead) {

            if (!crouched && !lookingUp && !reloading)
            hforce = Input.GetAxisRaw("Horizontal");

            anim.SetFloat("Speed", Mathf.Abs(hforce));
            
            rb.velocity = new Vector2(hforce * speed, rb.velocity.y);

            if(hforce > 0 && !facingRight)
            {
                Flip();
            }
            else if(hforce < 0 && facingRight)
            {
                Flip();
            }
            if (jump)
            {
                anim.SetBool("Jump", true);
                jump = false;
                rb.AddForce(Vector2.up * jumpForce);
            }
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
