using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerObjectByTag : MonoBehaviour
{
    private List<GameObject> takeQuests;

    private bool valueBillboardTag;

    private bool isTriggered { get; set; }

    private void Awake()
    {
        takeQuests = new List<GameObject>();
    }

    // Start is called before the first frame update
    void Start()
    {
        valueBillboardTag = gameObject.CompareTag("BillboardTag");


        getActiveQuestNotices();
    }


    // Update is called once per frame
    void Update()
    {
        if (isTriggered && Input.GetKeyDown(KeyCode.X))
        {
            if (takeQuests.Count > 0)
            {
                Destroy(takeQuests[0]);
                takeQuests.Remove(takeQuests[0]);
            }
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (valueBillboardTag)
            {
                isTriggered = true;

                getActiveQuestNotices();
            }
        }
    }

    private void getActiveQuestNotices()
    {
        var billboardParent = this.transform.parent.gameObject;
        var questsAndTile = billboardParent.GetComponentInChildren<Transform>();

        foreach (Transform item in questsAndTile)
        {
            var itemObject = item.gameObject;
            if (itemObject.activeSelf)
            {
                if (item.CompareTag("Notice"))
                {
                    if (!takeQuests.Contains(itemObject))
                    {
                        takeQuests.Add(itemObject);
                    }

                    print(item.name + true);
                }
            }
        }

        print(takeQuests.Count);
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