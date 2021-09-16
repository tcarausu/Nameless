using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inventory : MonoBehaviour
{
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;
    private Transform image;
    private PlayerBehaviourScript player;

    private void Start()
    {
        itemSlotContainer = transform.Find("itemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("itemSlotTemplate");
    }


    public void SetPlayer(PlayerBehaviourScript player)
    {
        this.player = player;
    }

    private void Inventory_OnItemListChanged(object sender, System.EventArgs e)
    {
        // RefreshInventoryItems();
    }

    // private void RefreshInventoryItems()
    // {
    //     foreach (Transform child in itemSlotContainer)
    //     {
    //         if (child == itemSlotTemplate) continue;
    //         Destroy(child.gameObject);
    //     }
    //
    //
    //     int x = 0;
    //     int y = 0;
    //     float itemSlotCellSize = 30f;
    //     foreach (Item item in inventory.GetItemList())
    //     {
    //         RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate).GetComponent<RectTransform>();
    //         itemSlotRectTransform.gameObject.SetActive(true);
    //         itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, y * itemSlotCellSize);
    //         Image image = itemSlotRectTransform.Find("image").GetComponent<Image>();
    //         image.sprite = item.GetSprite();
    //
    //
    //         x++;
    //         if(x > 4)
    //         {
    //             x = 0;
    //             y++;
    //         }
    //     }
    // }
}
