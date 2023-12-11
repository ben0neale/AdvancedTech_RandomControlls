using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timer;
    [SerializeField] GameObject PreGame;
    [SerializeField] TextMeshProUGUI CountDown;
    [SerializeField] Canvas canvas;
    [SerializeField] GameObject TextInput;
    ScoreBoard scoreboardRef;
    [SerializeField] GameObject HighScoreTable;
    HighScoreTable highscoreTable;
    GameObject Player;
    PlayerController playerref;
    private float time = 0;
    private int characterSize = 3;
    public bool CountDownFinished = false;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        playerref = Player.GetComponent<PlayerController>();
        PreGame.SetActive(true);
        time = 0;
        highscoreTable = HighScoreTable.GetComponent<HighScoreTable>();
        scoreboardRef = GetComponent<ScoreBoard>();
        //StartCoroutine(Count());
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
            else if (time > 99.9)
            {
                characterSize = 5;
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
        if (!playerref.wiiControls1 && !playerref.wiiControls2)
            highscoreTable.AddHighscoreEntry(time, name, Player.GetComponent<PlayerInput>().currentActionMap.name.Substring(11));
        else if (playerref.wiiControls1)
            highscoreTable.AddHighscoreEntry(time, name, "Controls 1");
        else if (playerref.wiiControls2)
            highscoreTable.AddHighscoreEntry(time, name, "Controls 2");
        highscoreTable.LoadHighScoreTable();
    }
    public IEnumerator Count()
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
