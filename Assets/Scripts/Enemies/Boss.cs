using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{

    public bool recharge = false;
    private float xdiff, ydiff;
    public Object magic;
    public Animator anim; 
    private int buffer, bufferMax = 240, recTime, recMax = 4000, recAttack = 0;

    [SerializeField]
    private GameObject pauseMenu;

    void Start()
    {
        health = 15;
        maxHealth = health;
        speed = 0.05f;
        randSpeed = 0.005f;
        randRange = 2;
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Physics2D.IgnoreLayerCollision(6, 6, true);
    }

    void Update()
    {
        if(!recharge) {
            if(buffer >= bufferMax) {
                buffer = 0;
                Attack(player);
                recAttack++;
                if(Random.Range(1, 101) < recAttack) {
                    recharge = true;
                    Debug.Log("Recharging");
                }
            }
            else {
                buffer++;
            }
        }
        else if(recTime >= recMax) {
            recTime = 0;
            recharge = false;
        }
        else {
            recTime++;
        }
    }

    public override void Movement(GameObject target)
    {
        var newLocX = Random.Range(-9, 9);
        var newLocY = Random.Range(-3.5f, 3.5f);

        xdiff = (newLocX - target.transform.position.x);
        ydiff = (newLocY - target.transform.position.y);

        while(xdiff <= 2 && xdiff >= -2) {
            newLocX = Random.Range(-9, 9);
            xdiff = (newLocX - target.transform.position.x);
        }

        while(ydiff >= 2 && ydiff <= -2) {
            newLocY = Random.Range(-3.5f, 3.5f);
            ydiff = (newLocY - target.transform.position.y);
        }

        this.transform.position = new Vector3(newLocX, newLocY);
    }
    
    public override void Attack(GameObject target)
    {
        GameObject projectile = Instantiate(magic, this.transform.position, this.transform.rotation) as GameObject;
        projectile.layer = 6;
        projectile.GetComponent<Magic>().target = player;
        //buffer = 0;
    }

    public override void Die() {
        
        if(anim.GetBool("DeadDone")) {
            //Destroy(this.gameObject);
            Debug.Log("Die2");
            //anim.SetBool("IsDead", false);
            this.GetComponent<BoxCollider2D>().enabled = false;
            this.GetComponent<CircleCollider2D>().enabled = false;
            this.GetComponent<Boss>().enabled = false;

            pauseMenu.GetComponent<PauseMenu>().WonGame();
        }
        else /*if(!anim.GetBool("DeadDone"))*/ {
            anim.SetTrigger("IsDead");
            anim.SetBool("DeadDone", true);
            //this.GetComponent<Rigidbody2D>().isKinematic = true;
            Debug.Log("Die1");

        }
    }

    private void OnCollisionStay2D(Collision2D other) {
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if(other.name == "Player") {
            anim.SetTrigger("Teleport");
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.name == "Player") {
        }
    }
}