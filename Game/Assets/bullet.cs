using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        
        EnemyHP hp = collision.GetComponent<EnemyHP>();
        Transform target = hp.transform;
        hp.knockBack();
        if (hp != null)
        {
            hp.getDmg(10);
        }
        Debug.Log(collision);
        Destroy(gameObject);
    }
}
