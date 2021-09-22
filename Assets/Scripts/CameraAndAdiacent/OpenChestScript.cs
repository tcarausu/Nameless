using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChestScript : MonoBehaviour
{
    [SerializeField] private GameObject chestClosed;
    [SerializeField] private GameObject chestOpen;

    public static bool chestIsOpen;
    private bool valueChest;

    void Start()
    {
        valueChest = gameObject.CompareTag("Chest");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            if (chestIsOpen)
            {
                CloseChest();
            }
            else
            {
                OpenChest();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (valueChest)
            {
                var objs = this.gameObject.GetComponentInChildren<Transform>();
                var parent = this.transform.parent.gameObject;
                Debug.Log(parent.name);
                OpenChest();
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (valueChest)
            {
                var objs = this.gameObject.GetComponentInChildren<Transform>();
                var parent = this.transform.parent.gameObject;
                Debug.Log(parent.name);
                CloseChest();
            }
        }
    }

    public void OpenChest() 
    {
        chestOpen.SetActive(true);
        chestClosed.SetActive(false);
        chestIsOpen = false;
    }

    public void CloseChest()
    {
        chestOpen.SetActive(false);
        chestClosed.SetActive(true);
        chestIsOpen = true;
    }
}