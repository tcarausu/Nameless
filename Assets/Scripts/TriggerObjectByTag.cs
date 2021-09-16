using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerObjectByTag : MonoBehaviour
{
    [SerializeField] private GameObject closed { get; set; }
    [SerializeField] private GameObject opened { get; set; }
    [SerializeField] private List<GameObject> takeQuests;

    private bool valueChest, valueBillboardTag;

    private bool isTriggered { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        valueChest = gameObject.CompareTag("Chest");
        valueBillboardTag = gameObject.CompareTag("BillboardTag");

        var objs = this.gameObject.GetComponentInChildren<GameObject>();
        Debug.Log("s");
        var s = this.gameObject.GetComponentInChildren<GameObject>().name;
        Debug.Log(s);

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
            if (valueChest)
            {
                closed.SetActive(false);
                opened.SetActive(true);
            }

            if (valueBillboardTag)
            {
                isTriggered = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (valueChest)
            {
                closed.SetActive(true);
                opened.SetActive(false);
            }

            if (valueBillboardTag)
            {
                isTriggered = false;
            }
        }
    }
}