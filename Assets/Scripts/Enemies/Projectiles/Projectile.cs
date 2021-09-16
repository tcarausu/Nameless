using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    public float velocity;
    public int damage;
    public GameObject target;
    public Vector3 direction;

    public virtual void Move(GameObject target) {
        this.transform.position += direction*velocity;
    }

    public virtual void Hit(GameObject hit) {
        Destroy(this.gameObject);
    }

}
