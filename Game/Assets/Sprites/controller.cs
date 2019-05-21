using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class controller : MonoBehaviour
{
    public float maxSpeed = 10f;
    public float hp_player = 100;
    bool facingRight = true;
    // Start is called before the first frame update

    //ground
    bool grounded = false;
    public Transform groundCheck;
    float groundRadius = 0.2f;
    public LayerMask whatIsGround;
    bool fire = false;

    public int i = 1;


    Animator anim;

    public float jumpForce = 700f;

    void Start()
    {
        anim = GetComponent<Animator>();
        gameObject.GetComponent<DB_update>().db_addPlayer("Vasile", 100, 50, "Player_DB");
      
        
        
    }
    public void getDmgPlayer(float dmg)
    {
        hp_player -= dmg;
        if (hp_player == 0)
        {
            gameObject.active = false;
        }


    }
    public void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Zombi")
        {

            this.getDmgPlayer(10);
            this.knockBackPlayer();
        }
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


        anim.SetBool("fire", fire);

        if (move > 0 && !facingRight)
        {
            i = i * (-1);
            Flip();
        }
        else if (move < 0 && facingRight)
        {
            i = i * (-1);
            Flip();
        }

        if (Input.GetButtonDown("Fire1"))
        {
            fire = true;
        }
        else
        {
            fire = false;
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

        transform.Rotate(0f, 180f, 0f);
    }
    public void knockBackPlayer()
    {
        float newx;
        newx = transform.position.x + 100 * (-i);
        Vector2 newt = new Vector2(newx, transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, newt, 50 * Time.deltaTime);
    }
}
