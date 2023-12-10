using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class HighScoreTable : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    private List<Transform> highscoreEntryTransformList;
    private List<string> PlayerPrefOptions;
    [SerializeField] GameObject dropdownMenu;
    [SerializeField] GameObject GameStateController;
    GameStateController gameStateControllerref;
    

    public int highscoretableValue = 0;

    public float templateHeight = 20f;

    private void Awake()
    {
        gameStateControllerref = GameStateController.GetComponent<GameStateController>();
        PlayerPrefOptions = new List<string>() {"highscoreTable", "highscoreTable2", "highscoreTable3", "highscoreTable4"};
        entryContainer = transform.Find("HighScoreEntryContainer");
        entryTemplate = entryContainer.Find("HighScoreEntryTemp");

        entryTemplate.gameObject.SetActive(false);

        dropdownMenu.GetComponent<TMP_Dropdown>().onValueChanged.AddListener(delegate { SetHighScoreTableValue(dropdownMenu.GetComponent<TMP_Dropdown>().value); });
    }

    public void LoadHighScoreTable()
    {
        for (int i = 1; i < entryContainer.childCount; i++)
        {
            Destroy(entryContainer.GetChild(i).gameObject);
        }
        if (PlayerPrefs.HasKey(PlayerPrefOptions[highscoretableValue]))
        { 
            string jsonString = PlayerPrefs.GetString(PlayerPrefOptions[highscoretableValue]);
            Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

            //sort entry list by score
            for (int i = 0; i < highscores.highscoreEntryList.Count; i++)
            {
                for (int j = i + 1; j < highscores.highscoreEntryList.Count; j++)
                {
                    if (highscores.highscoreEntryList[j].time < highscores.highscoreEntryList[i].time)
                    {
                        //swap
                        HighScoreEntry temp = highscores.highscoreEntryList[i];
                        highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
                        highscores.highscoreEntryList[j] = temp;
                    }
                }
            }

            highscoreEntryTransformList = new List<Transform>();
            foreach (HighScoreEntry entry in highscores.highscoreEntryList)
            {
                CreateHighscoreEntryTransform(entry, entryContainer, highscoreEntryTransformList);
            }
        }
    }

    private void CreateHighscoreEntryTransform(HighScoreEntry highscoreEntry, Transform container, List<Transform> transformList)
    {
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        string rankString;
        switch (rank)
        {
            default:
                rankString = rank + "TH"; break;
            case 1: rankString = "1ST"; break;
            case 2: rankString = "2ND"; break;
            case 3: rankString = "3RD"; break;
        }
        string controlscheme = highscoreEntry.controls;

        entryTransform.Find("PosText").GetComponent<TextMeshProUGUI>().text = rankString + " (" + controlscheme + ")";

        float time = highscoreEntry.time;

        entryTransform.Find("ScoreText").GetComponent<TextMeshProUGUI>().text = time.ToString();

        string name = highscoreEntry.name;
        entryTransform.Find("NameText").GetComponent<TextMeshProUGUI>().text = name;

        transformList.Add(entryTransform);
    }

    public void AddHighscoreEntry(float time, string name, string controlScheme)
    {
        //Create highscore entry
        HighScoreEntry highscoreentry = new HighScoreEntry { time = time, name = name , controls = controlScheme};

        if (!PlayerPrefs.HasKey(PlayerPrefOptions[highscoretableValue]))
        {
            HighScoreEntry temp = new HighScoreEntry { time = 00, name = "Test" };
            PlayerPrefs.SetString(PlayerPrefOptions[highscoretableValue], JsonUtility.ToJson(temp));
        }

        //Load Saved Highscores
        string jsonString = PlayerPrefs.GetString(PlayerPrefOptions[highscoretableValue]);
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        //add new entry to highscores
        highscores.highscoreEntryList.Add(highscoreentry);

        //save updated highscores
        string Json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString(PlayerPrefOptions[highscoretableValue], Json);
        PlayerPrefs.SetString(PlayerPrefOptions[0], Json);
        PlayerPrefs.Save();
    }

    public void SetHighScoreTableValue(int value)
    {
        highscoretableValue = value;
        LoadHighScoreTable();       
    }
    public void ClearTable()
    {
        PlayerPrefs.DeleteKey(PlayerPrefOptions[highscoretableValue]);
        PlayerPrefs.SetString(PlayerPrefOptions[highscoretableValue], JsonUtility.ToJson(""));
        LoadHighScoreTable();
    }

    private class Highscores
    {
        public List<HighScoreEntry> highscoreEntryList;
    }

    [System.Serializable]
    private class HighScoreEntry
    {
        public float time;
        public string name;
        public string controls;
    }

}
