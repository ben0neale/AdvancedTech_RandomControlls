using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameStateController : MonoBehaviour
{
    public enum GameState
    {
        Menu,
        Play,
        Scoreboard
    }

    public GameState state;

    // Start is called before the first frame update
    void Start()
    {
        state = GameState.Play;
    }
}
