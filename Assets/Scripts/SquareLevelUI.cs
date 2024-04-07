using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SquareLevelUI : MonoBehaviour
{
    public LevelData levelData;
    [SerializeField] private TextMeshProUGUI levelName;
    [SerializeField] private TextMeshProUGUI numberOfStars;
    [SerializeField] private Sprite lockIconUnlocked;
    [SerializeField] private Sprite lockIconLocked;
    [SerializeField] private GameObject square;

    public void UpdateLevelData()
    {
        if(!levelData.unlocked)
        {
            levelName.text = "???????";
            numberOfStars.text = "???";
            return;
        }
        square.GetComponent<SpriteRenderer>().sprite = levelData.levelImage;
        levelName.text = levelData.title;
        numberOfStars.text = levelData.starsGained.ToString() + " *";
    }
}
