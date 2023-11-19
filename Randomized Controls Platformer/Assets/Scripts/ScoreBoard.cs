using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ScoreboardText;

    // Start is called before the first frame update
    void Start()
    {
        ScoreboardText.text = "Scoreboard:";
    }

    public void ScoreboardUpdate(TextMeshProUGUI time)
    {
        ScoreboardText.text += "\n" + time.text;
    }
}
