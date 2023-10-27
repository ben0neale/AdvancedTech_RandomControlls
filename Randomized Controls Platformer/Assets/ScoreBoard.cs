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
        ScoreboardText.text = "Scoreboard";
    }

    public void ScoreboardUpdate(float time)
    {
        ScoreboardText.text = ScoreboardText.text + "\n" + time.ToString().Substring(0,4);
    }
}
