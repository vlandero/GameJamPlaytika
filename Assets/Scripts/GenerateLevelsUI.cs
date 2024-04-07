using UnityEngine;

[System.Serializable]
public class LevelData
{
    public string title;
    public int starsGained;
    public bool unlocked;
    public bool secretUnlocked;
}

public class GenerateLevelsUI : MonoBehaviour
{
    public GameObject cubePrefab;
    public float distanceBetweenCubes = 50f;
    public Vector2 offset = Vector2.zero;
    public GameObject[] cubes;

    public LevelData[] levelsData;
    public int currentLevel = 0;

    private const string PlayerPrefKey = "LevelProgress";

    void Start()
    {
        // LoadLevelProgress();
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
        string progressData = JsonUtility.ToJson(levelsData);
        PlayerPrefs.SetString(PlayerPrefKey, progressData);
        PlayerPrefs.Save();
    }

    void LoadLevelProgress()
    {
        if (PlayerPrefs.HasKey(PlayerPrefKey))
        {
            string progressData = PlayerPrefs.GetString(PlayerPrefKey);
            LevelData[] savedLevelsData = JsonUtility.FromJson<LevelData[]>(progressData);
            for (int i = 0; i < savedLevelsData.Length; i++)
            {
                levelsData[i].starsGained = savedLevelsData[i].starsGained;
                levelsData[i].unlocked = savedLevelsData[i].unlocked;
                levelsData[i].secretUnlocked = savedLevelsData[i].secretUnlocked;
            }
        }
    }

    //private void OnDestroy()
    //{
    //    SaveLevelProgress();
    //}
}
