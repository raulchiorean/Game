using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class controller : MonoBehaviour
{
    public float maxSpeed = 10f;
    bool facingRight = true;
    // Start is called before the first frame update

    //ground
    bool grounded = false;
    public Transform groundCheck;
    float groundRadius = 0.2f;
    public LayerMask whatIsGround;

    Animator anim;

    public float jumpForce = 700f;

    void Start()
    {
        anim = GetComponent<Animator>();
        gameObject.GetComponent<DB_update>().db_addPlayer("Vasile", 100, 50, "Player_DB");
      
        
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //check if we are on the ground
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        anim.SetBool("ground", grounded);

        //jump
        anim.SetFloat("vspeed", GetComponent<Rigidbody2D>().velocity.y);

        float move = Input.GetAxis("Horizontal");

        anim.SetFloat("speed", Mathf.Abs(move));

        GetComponent<Rigidbody2D>().velocity = new Vector2(move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

        

        if (move > 0 && !facingRight)
        {
            Flip();
        }
        else if (move < 0 && facingRight)
        {
            Flip();
        }
    }
    private void Update()
    {
        if(grounded  && Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("ground", false);
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));
        }
    }
    void Flip()
    {
        //opposit direction
        facingRight = !facingRight;

        //get the local scale
        Vector3 theScale = transform.localScale;

        //flip on x
        theScale.x *= -1;
        //back to local scale
        transform.localScale = theScale;
    }
}
