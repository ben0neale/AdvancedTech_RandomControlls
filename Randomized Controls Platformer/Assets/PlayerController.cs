using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private InputMaster inputMaster;
    private PlayerInput playerInput;
    private Rigidbody2D RB;

    public float moveSpeed;

    // Start is called before the first frame update
    void Awake()
    {
        inputMaster = new InputMaster();
        RB = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMove(InputValue inputValue)
    {
        RB.velocity = inputValue.Get<Vector2>() * moveSpeed;

    }
    private void OnSwitchAction()
    {
        if (playerInput.currentActionMap.name == "Player Controls 1")
        {
            playerInput.SwitchCurrentActionMap("Player Controls 2");
        }
        else
        {
            playerInput.SwitchCurrentActionMap("Player Controls 1");
        }
        
    }
}
