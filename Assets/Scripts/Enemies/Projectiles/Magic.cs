using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : Projectile
{
    private bool osc = false;
    private Vector2 tempDir;


    void Start() 
    {
        velocity = 0.008f;
        tempDir = target.transform.position - this.transform.position;
        direction = tempDir.normalized;

    }

    void Update()
    {
        Move(target);
    }

    private void Oscillate() {
        this.transform.position += (direction*Mathf.Sin(Time.time))*velocity/2;
    }

    public override void Move(GameObject target){
        this.transform.position += direction*velocity;
        //this.transform.position += direction*Mathf.Sin(Time.time)*velocity;
        this.transform.position += (direction*Mathf.Sin(Time.time))*velocity/2;

    }

    private void OnCollisionEnter2D(Collision2D hit) {
        if(hit.gameObject.name == "Player") {

        }
        Hit(hit.gameObject);
    }
}