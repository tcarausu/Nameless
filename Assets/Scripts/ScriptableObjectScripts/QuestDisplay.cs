using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestDisplay : MonoBehaviour
{
    public QuestItem questItem;

    public Text   description, difficulty, title;
    public Image questTaskDestination;

    // Start is called before the first frame update
    void Start()
    {
        title.text = questItem.title;
        difficulty.text = questItem.difficulty;
        description.text = questItem.description;

        questTaskDestination.sprite = questItem.questTarget;
    }

}
