using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy
{
    private float dist;
    // private int buffer, bufferMax = 150;
    private Vector3 dir;
    private bool moveOver;
    public Object web;
    public GameObject origin;

    void Start()
    {
        DefaultInit();
        health = 40;
        maxHealth = health;
        speed = 0.05f;
        randSpeed = 0.005f;
        randRange = 2;
        web = Resources.Load("Prefabs/Enemies/Projectile/Web") as GameObject;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        var lastPos = this.transform.position;
        this.transform.position = new Vector3(lastPos.x, lastPos.y, 0);
        rig.velocity = new Vector2(0, 0);
        if (!moveOver)
        {
            //Debug.Log("Random Running");
            moveRand();
            // if(newMove && buffer >= bufferMax) {
            //     if (Random.Range(1, 3) > 1) {
            //         Attack(player);
            //     }
            //     //Debug.Log("Attack");
            // }
            // else {
            //     buffer++;
            // }
        }
        else
        {
            // if(buffer >= bufferMax) {
            //     Attack(player);
            // }
            // else {
            //     buffer++;
            // }
        }
    }

    public override void Movement(GameObject target)
    {
        dist = Vector2.Distance(target.transform.position, this.transform.position);

        if (dist <= 4)
        {
            //Debug.Log(dist);
            dir = (target.transform.position - this.transform.position).normalized;
            this.transform.position -= dir * speed * 1.2f;

            /*RaycastHit2D hit = Physics2D.Raycast(this.transform.position, dir * speed * 1.2f);

            if (hit.collider.tag == "Wall") {
                moveRand();
            }
            else {
                this.transform.position -= dir * speed * 1.2f;
            }*/
        }
    }

    public override void Attack(GameObject target)
    {
        GameObject projectile = Instantiate(web, this.transform.position, this.transform.rotation) as GameObject;
        projectile.layer = 11;
        projectile.GetComponent<Web>().target = player;
        // buffer = 0;
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Wall")
        {
            //Debug.Log("Hit wall");
            dist = Vector2.Distance(origin.transform.position, this.transform.position);

            Vector3 tempDir = (origin.transform.position - this.transform.position).normalized;
            this.transform.position += tempDir * speed * 1.2f;

            newMove = true;
        }
    }

    // void OnCollisionEnter2D(Collider2D other)
    // {
    //
    //
    // }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            moveOver = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        //Debug.Log("Hit");
        if (other.name == "Player")
        {
            moveOver = true;
            Movement(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            Movement(other.gameObject);
            moveOver = false;
        }
    }
}