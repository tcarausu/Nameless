using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{

    protected int health, maxHealth;
    protected int randRange, damage;
    protected float speed, randSpeed;
    public GameObject player;
    private bool doneX = false, doneY = false;
    public bool newMove = true;
    private float xRand, yRand;
    public Rigidbody2D rig;
    private Vector3 lastfreePos;

    public abstract void Movement(GameObject target);
    public abstract void Attack(GameObject target);

    // public WeaponItem spawnedItem;
    // public Item item;
    // private ItemWorldSpawner spawner;

    public virtual void DefaultInit()
    {
        health = 10;
        maxHealth = health;
        speed = 0.05f;
        randSpeed = 0.005f;
        randRange = 2;
        rig = GetComponent<Rigidbody2D>();
        Physics2D.IgnoreLayerCollision(6, 6, true);
    }

    public virtual void Die()
    {
        Destroy(this.gameObject);

        // spawner = GetComponent<ItemWorldSpawner>();
        // spawner.enabled = true;

        //leaving it temporary 2 to the right 
        //Vector3 weaponOnDeathSpawnLocation = transform.position+ new Vector3(2,0,0);

        //ItemWorld.SpawnItemWorld(weaponOnDeathSpawnLocation, item, tag = "Weapon", spawner.checkIsWeaponItemType(item.itemType));
    }

    public virtual void TakeDamage(int dmg)
    {
        health -= dmg;
        Debug.Log("Enemy damaged" + health);

        if (health <= 0)
        {
            Die();
        }
    }

    public virtual void moveRand()
    {
        if (newMove)
        {
            xRand = this.transform.position.x + Random.Range(-randRange, randRange);
            yRand = this.transform.position.y + Random.Range(-randRange, randRange);
            var cast = new Vector2(xRand, yRand);
            //Debug.Log(cast);

            RaycastHit2D hit = Physics2D.Raycast(this.transform.position, cast);
            //Debug.Log(hit.collider.gameObject.tag);

            // if (hit.collider.gameObject.tag == "Wall" || hit.collider.gameObject.tag == "Door")
            // {
            //     this.transform.position = lastfreePos;
            //     //Debug.Log("Wall");
            //     xRand = this.transform.position.x + Random.Range(-randRange, randRange);
            //     yRand = this.transform.position.y + Random.Range(-randRange, randRange);
            // }
            //
            // else
            // {
                lastfreePos = this.transform.position;
                newMove = false;
            // }


            //Debug.Log("New move set");
        }
        else
        {
            if (!doneX)
            {
                if (this.transform.position.x > xRand + randSpeed)
                {
                    this.transform.position -= new Vector3(randSpeed, 0, 0);
                    //rig.AddForce(- new Vector3(randSpeed, 0, 0));
                }
                else if (this.transform.position.x < xRand - randSpeed)
                {
                    this.transform.position += new Vector3(randSpeed, 0, 0);
                    //rig.AddForce(new Vector3(randSpeed, 0, 0));
                }
                else
                {
                    doneX = true;
                }
            }
            if (!doneY)
            {
                if (this.transform.position.y > yRand + randSpeed)
                {
                    this.transform.position -= new Vector3(0, randSpeed, 0);
                    //rig.AddForce(- new Vector3(0, randSpeed, 0));
                }
                else if (this.transform.position.y < yRand - randSpeed)
                {
                    this.transform.position += new Vector3(0, randSpeed, 0);
                    //rig.AddForce(new Vector3(0, randSpeed, 0));
                }
                else
                {
                    doneY = true;
                }
            }
            if (doneX && doneY)
            {
                newMove = true;
                doneX = doneY = false;
                //Debug.Log("Movement done");
            }
        }
    }
}
