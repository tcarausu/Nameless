using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openChest : MonoBehaviour
{

    public GameObject chestOpen;
    public GameObject chestClosed;

    public static bool chestIsOpen;


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

    public void CloseChest()
    {
        chestOpen.SetActive(true);
        chestClosed.SetActive(false);
        chestIsOpen = false;
    }

    public void OpenChest()
    {
        chestOpen.SetActive(false);
        chestClosed.SetActive(true);
        chestIsOpen = true;
    }

}
