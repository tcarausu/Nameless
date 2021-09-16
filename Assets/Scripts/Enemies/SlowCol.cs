using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SlowCol : MonoBehaviour
{

    bool Slow; 

    private void OnTriggerStay2D(Collider2D collider)
    {
        

        if (collider.name == "Player")
        {
            FindObjectOfType<PlayerBehaviourScript>().moveSpeed = 3f;
            Debug.Log("Hit");
            Invoke("speedUp", 2.0f);
        }
      
    }



    void speedUp()
    {
        FindObjectOfType<PlayerBehaviourScript>().moveSpeed = 5f;
        Debug.Log("Speed up");
    }




}
