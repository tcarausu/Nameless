using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealingItem", menuName = "Items/HealingItem")]
public class HealingItem : ScriptableObject
{

    public Sprite sprite;
    public float healingAmount, rarity;

}
