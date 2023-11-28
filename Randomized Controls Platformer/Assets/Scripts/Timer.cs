using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timer;
    [SerializeField] TextMeshProUGUI CountDown;
    [SerializeField] Canvas canvas;
    [SerializeField] GameObject TextInput;
    ScoreBoard scoreboardRef;
    [SerializeField] GameObject HighScoreTable;
    HighScoreTable highscoreTable;
    private float time = 0;
    private int characterSize = 3;
    public bool CountDownFinished = false;

    void Start()
    {
        time = 0;
        highscoreTable = HighScoreTable.GetComponent<HighScoreTable>();
        scoreboardRef = GetComponent<ScoreBoard>();
        StartCoroutine(Count());
    }

    // Update is called once per frame
    void Update()
    {
        if (CountDownFinished)
        {
            time += Time.deltaTime;
            if (time > 99.9)
            {
                characterSize = 5;
            }
            else if (time > 9.9)
            {
                characterSize = 4;
            }
            timer.text = time.ToString().Substring(0, characterSize);
        }       
    }

    public void ResetTimer()
    {
        //scoreboardRef.ScoreboardUpdate(timer);
        CountDownFinished = false;
        characterSize = 3;
        //time = 0;
        //StartCoroutine(Count());
    }

    public void UpdateLeaderboard(string name)
    {
        TextInput.SetActive(false);
        highscoreTable.AddHighscoreEntry(time, name);
        highscoreTable.LoadHighScoreTable();
    }
    IEnumerator Count()
    {
        TextMeshProUGUI obj = Instantiate(CountDown,canvas.transform);
        obj.text = "3";
        yield return new WaitForSeconds(1);
        obj.text = "2";
        yield return new WaitForSeconds(1);
        obj.text = "1";
        yield return new WaitForSeconds(1);
        CountDownFinished = true;
        Destroy(obj);
    }
        
}
