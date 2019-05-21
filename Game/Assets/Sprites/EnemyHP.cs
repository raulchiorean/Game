using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    public float hp;
    Animator anim;
    bool agro = false;

    public float speed;
    bool facingRight = true;
    public int i = 1;

    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    public void getDmg(float dmg)
    {
        hp -= dmg;
        if(hp == 0)
        {
            gameObject.active = false;
        }

       
    }
    
       
   

    // Update is called once per frame
    void Update()
    {
        if (!agro && (Vector2.Distance(transform.position, target.position) < 9))
        {
            agro = true;
            
        }
        if (agro)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }

        if (target.position.x > transform.position.x && facingRight)
        {
            i = i * -1;
            Flip();
        }
        else if (target.position.x < transform.position.x && !facingRight)
        {
            i = i * -1;
            Flip();
        }
    }

    void Flip()
    {
        //opposit direction
        facingRight = !facingRight;

        transform.Rotate(0f, 180f, 0f);
    }
    public void knockBack()
    {
        float newx;
        newx = transform.position.x + 100* i;
        Vector2 newt = new Vector2(newx, transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, newt, 50 * Time.deltaTime);
    }
}
