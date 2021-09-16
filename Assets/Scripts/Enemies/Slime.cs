using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy
{

    private float dist;
    private Vector3 dir;

    // Start is called before the first frame update
    void Start()
    {
        health = 50;
        maxHealth = health;

        speed = 0.03f;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Movement(GameObject target)
    {
        dist = Vector2.Distance(target.transform.position, this.transform.position);

        if (dist > 1)
        {
            //Debug.Log(dist);
            dir = (target.transform.position - this.transform.position).normalized;
            this.transform.position += dir * speed * 1.2f;
        }

    }
    public void Attack()
    {
        throw new System.NotImplementedException();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        //Debug.Log("Hit");
        if (other.name == "Player")
        {
            Movement(other.gameObject);
            
        }
    }

    public override void Attack(GameObject target)
    {
        throw new System.NotImplementedException();
    }

    //  private void OnTriggerExit2D(Collider2D collider)
    //{
    //  if (collider.name == "Player")
    //{
    //  FindObjectOfType<PlayerBehaviour>().moveSpeed = 5f;
    //Debug.Log("Free");
    //}

    //}
}



