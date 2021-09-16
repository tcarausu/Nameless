using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Web : Projectile
{
    public Animator animator;

    private void Awake() {
        Physics.IgnoreLayerCollision(11, 10);
    }

    void Start() 
    {
        velocity = 0.025f;
        direction = (target.transform.position - this.transform.position).normalized;
        damage = 1;

    }

    void Update()
    {
        Move(target);
    }

    private void OnCollisionEnter2D(Collision2D hit) {
        if(hit.gameObject.tag == "Player") {
            HeartsHealthVisual.heartsHealthSystemStatic.damage(damage);
        }
        else if (hit.gameObject.tag == "Detail") {
            return;
        }
        Hit(hit.gameObject);
        //Debug.Log("Hit " + hit.gameObject.name);
        
    }
}