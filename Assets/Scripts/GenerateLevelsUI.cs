using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class LevelData
{
    public string title;
    public int starsGained;
    public bool unlocked;
    public bool secretUnlocked;
    public Sprite levelImage;
}

public class ToSerialize
{
    public LevelData[] levelsData;
}

public class GenerateLevelsUI : MonoBehaviour
{
    public GameObject cubePrefab;
    public float distanceBetweenCubes = 50f;
    public Vector2 offset = Vector2.zero;
    public GameObject[] cubes;

    public LevelData[] levelsData;
    public LevelData[] defaultLevelsData;
    public int currentLevel = 0;

    private const string PlayerPrefKey = "LevelProgress";

    void Start()
    {
        LoadLevelProgress();
        Time.timeScale = 1f;
        cubes = new GameObject[levelsData.Length + 1];
        GenerateCubes();
    }

    void GenerateCubes()
    {
        for (int i = 1; i <= levelsData.Length; i++)
        {
            GameObject cube = Instantiate(cubePrefab, transform);
            SquareLevelUI squareLevelUI = cube.GetComponent<SquareLevelUI>();
            squareLevelUI.levelData = levelsData[i - 1];
            squareLevelUI.UpdateLevelData();
            cubes[i] = cube;
            RectTransform cubeRectTransform = cube.GetComponent<RectTransform>();

            Vector2 position = new Vector2(i * distanceBetweenCubes + offset.x, offset.y);

            cubeRectTransform.anchoredPosition = position;
        }
    }

    void SaveLevelProgress()
    {
        string progressData = JsonUtility.ToJson(new ToSerialize { levelsData=levelsData } );
        PlayerPrefs.SetString(PlayerPrefKey, progressData);
        PlayerPrefs.Save();
        Debug.Log("Saving level progress");
        Debug.Log(progressData);
    }

    void LoadLevelProgress()
    {
        if (PlayerPrefs.HasKey(PlayerPrefKey))
        {
            string progressData = PlayerPrefs.GetString(PlayerPrefKey);
            if(progressData == "{}")
            {
                levelsData = defaultLevelsData;
                SaveLevelProgress();
                return;
            }
            ToSerialize savedLevelsData = JsonUtility.FromJson<ToSerialize>(progressData);
            for (int i = 0; i < savedLevelsData.levelsData.Length; i++)
            {
                levelsData[i].starsGained = savedLevelsData.levelsData[i].starsGained;
                levelsData[i].unlocked = savedLevelsData.levelsData[i].unlocked;
                levelsData[i].secretUnlocked = savedLevelsData.levelsData[i].secretUnlocked;
            }
        }
        else
        {
            levelsData = defaultLevelsData;
            SaveLevelProgress();
        }
    }
}
