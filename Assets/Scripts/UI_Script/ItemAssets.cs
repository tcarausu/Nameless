using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{

    public static ItemAssets Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public Transform swItemWorld;

    //scriptObjects
    public WeaponItem swordWeapon, daggerWeapon, battleAxeWeapon, bowWeapon;
    public HealingItem potionItem;

}
