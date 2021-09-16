using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//https://www.youtube.com/watch?v=WLDgtRNK2VE use scriptable objects
[CreateAssetMenu(fileName = "WeaponItem",menuName = "Items/WeaponItem")]
public class WeaponItem : ScriptableObject
{

    public Sprite sprite;
    public float fastAttackDamage, heavyAttackDamage,speed, rarity;

}
