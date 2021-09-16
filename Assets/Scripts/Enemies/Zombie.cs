using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Enemy
{

    private float dist, buffer, bufferMax;
    private int attBuffer;
    private Vector3 dir;
    private bool moveOver;
    public GameObject origin;
    public Animator animator;

    void Start()
    {
        DefaultInit();
        health = 70;
        maxHealth = health;
        speed = 0.010f;
        randSpeed = 0.005f;
        randRange = 3;
        damage = 1;
    }

    void Update()
    {
        var lastPos = this.transform.position;
        this.transform.position = new Vector3(lastPos.x, lastPos.y, 0);
        animator.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
        animator.SetFloat("Vertical", Input.GetAxis("Vertical"));
        if (!moveOver)
        {
            //Debug.Log("Random Running");
            moveRand();
        }

    }

    public override void Movement(GameObject target)
    {
        dist = Vector2.Distance(target.transform.position, this.transform.position);

        if (dist > 1)
        {
            //Debug.Log(dist);
            dir = (target.transform.position - this.transform.position).normalized;
            this.transform.position += dir * speed * 1.2f;
            attBuffer++;
        }
        else
        {
            if(attBuffer > 240) {
                Attack(player);
                attBuffer = 0;
            }
            else {
                attBuffer++;
            }
        }

    }

    public override void Attack(GameObject target)
    {
        Debug.Log("Zomb attack!");
        HeartsHealthVisual.heartsHealthSystemStatic.damage(damage);
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.name == "Start_Walls")
        {
            /*Debug.Log("Hit wall");
            dist = Vector2.Distance(origin.transform.position, this.transform.position);
            
            Vector3 tempDir = (origin.transform.position - this.transform.position).normalized;
            this.transform.position += tempDir*speed*1.2f;*/
            newMove = true;
        }
    }

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
