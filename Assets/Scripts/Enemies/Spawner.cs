using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // private int difficulty = 0, roomVal = 0;
    // public List<GameObject> spawners;
    // public int SpiderVal = 3, ZombieVal = 2, SlimeVal = 2;
    // public GameObject Spider, Zombie, Slime, MainCam;
    // private Room curRoom;
    // private bool cleared = false;
    //
    // private void OnEnable() {
    //     curRoom = MainCam.GetComponent<CameraController>().currRoom;
    //     if(!curRoom.cleared) {
    //         try {
    //             foreach(Transform child in curRoom.gameObject.transform)
    //             {
    //                 //Debug.Log(child);
    //                 if(child.tag == "Spawner") {
    //                     spawners.Add(child.gameObject);
    //                 }
    //             }
    //             //Debug.Log("Test");
    //             foreach(GameObject spawn in spawners) {
    //                 if(roomVal <= difficulty) {
    //                     switch (UnityEngine.Random.Range(0, 4)) {
    //                         case 1:
    //                             Instantiate(Spider, spawn.transform.position, spawn.transform.rotation);
    //                             roomVal += SpiderVal;
    //                             break;
    //                         case 2:
    //                             Instantiate(Zombie, spawn.transform.position, spawn.transform.rotation);
    //                             roomVal += ZombieVal;
    //                             break;
    //                         case 3:
    //                             break;
    //                     }
    //                     
    //                     //Instantiate();
    //                 }
    //                 else {
    //                     break;
    //                 }
    //             }
    //         spawners.Clear();
    //         //difficulty += UnityEngine.Random.Range(0, 2);
    //         roomVal = 0;
    //         difficulty++;
    //         Debug.Log(difficulty);
    //         curRoom.cleared = true; 
    //         this.enabled = false;
    //         }
    //
    //         catch (Exception e){
    //             Debug.Log(e);
    //             this.enabled = false;
    //         }
    //     }
    //     else {
    //         this.enabled = false;
    //     }
    // }
}