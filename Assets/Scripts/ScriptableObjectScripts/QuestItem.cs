using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestItem", menuName = "Items/QuestItem")]
public class QuestItem : ScriptableObject
{

    public Sprite questTarget;
    public string title,difficulty, description;
    // public float timeToComplete;

}
