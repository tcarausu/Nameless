using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerObjectByTag : MonoBehaviour
{
    private List<GameObject> takeQuests;

    private bool valueBillboardTag;

    private bool isTriggered { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        valueBillboardTag = gameObject.CompareTag("BillboardTag");
    }


    // Update is called once per frame
    void Update()
    {
        if (isTriggered && Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log(takeQuests.Count);
            Destroy(takeQuests[0]);
            Debug.Log(takeQuests.Count);
            // takeQuest.SetActive(false);
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (valueBillboardTag)
            {
                isTriggered = true;

                var billboardParent = this.transform.parent.gameObject;
                var questsAndTile = billboardParent.GetComponentInChildren<Transform>();

                Debug.Log(billboardParent.name);
                foreach (Transform item in questsAndTile)
                {
                    Debug.Log(item.name);
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (valueBillboardTag)
            {
                isTriggered = false;

                // var billboardParent = this.transform.parent.gameObject;
                // var questsAndTile = this.gameObject.GetComponentInChildren<Transform>();

                // Debug.Log(billboardParent.name);
            }
        }
    }
}