using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private InputMaster inputMaster;
    private PlayerInput playerInput;
    private Rigidbody2D RB;
    private string tempActionMap;

    List<string> actionMaps = new List<string>();

    public float moveSpeed;

    // Start is called before the first frame update
    void Awake()
    {
        inputMaster = new InputMaster();
        RB = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        actionMaps.Add("Player Controls 1");
        actionMaps.Add("Player Controls 2");
        actionMaps.Add("Player Controls 3");
        tempActionMap = actionMaps[Random.Range(0, actionMaps.Count)];
    }

    private void OnMove(InputValue inputValue)
    {
        RB.velocity = inputValue.Get<Vector2>() * moveSpeed;

    }
    private void OnSwitchAction()
    {
        while (tempActionMap == playerInput.currentActionMap.name)
            tempActionMap = actionMaps[Random.Range(0, actionMaps.Count)];

        playerInput.SwitchCurrentActionMap(tempActionMap);        
    }
}
