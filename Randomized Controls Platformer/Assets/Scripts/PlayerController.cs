using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject startSpot;
    [SerializeField] GameObject TimerOBJ;
    Timer TimerRef;

    private InputMaster inputMaster;
    private PlayerInput playerInput;
    private Rigidbody2D RB;
    private GroundCheck groundCheckRef;

    private string tempActionMap;

    List<string> actionMaps = new List<string>();
    List<string> controls = new List<string>();
    public TextMeshProUGUI controlsText;

    public float moveSpeed;
    private float x;
    public float jumpHeight;

    // Start is called before the first frame update
    void Awake()
    {
        inputMaster = new InputMaster();
        RB = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();

        groundCheckRef = GetComponentInChildren<GroundCheck>();
        TimerRef = TimerOBJ.GetComponent<Timer>();
    }

    private void Start()
    {
        actionMaps.Add("Player Controls 1");
        actionMaps.Add("Player Controls 2");
        actionMaps.Add("Player Controls 3");

        actionMaps.Add("Controller Controls 1");
        actionMaps.Add("Controller Controls 2");
        actionMaps.Add("Controller Controls 3");

        controls.Add("Controls - Mouse And Keyboard:\nRight - D\nLeft - A\nJump - Space");
        controls.Add("Controls - Mouse And Keyboard:\nRight - A\nLeft - D\nJump - Space");
        controls.Add("Controls - Mouse And Keyboard:\nRight - W\nLeft - S\nJump - Space");

        controls.Add("Controls - Xbox Controller:\nRight - Stick Right\nLeft - Stick Left\nJump - A");
        controls.Add("Controls - Xbox Controller:\nRight - Stick Left\nLeft - Stick Right\nJump - Y");
        controls.Add("Controls - Xbox Controller:\nRight - Stick Up\nLeft - Stick Down\nJump - Right Trigger");

        controlsText.text = controls[0];
        tempActionMap = actionMaps[Random.Range(0, actionMaps.Count)];  
    }

    private void FixedUpdate()
    {
        RB.velocity = new Vector2(x * moveSpeed, RB.velocity.y);
        if (transform.position.y < -5)
        {
            transform.position = new Vector3(-3, 3, 0);
        }
    }

    private void OnMove(InputValue inputValue)
    {
        x = inputValue.Get<Vector2>().x;
    }
    private void OnSwitchAction()
    {
        int random = Random.Range(0, actionMaps.Count);
        tempActionMap = actionMaps[random];
        while (tempActionMap == playerInput.currentActionMap.name)
        {
            random = Random.Range(0, actionMaps.Count);
            tempActionMap = actionMaps[random];
        }

        playerInput.SwitchCurrentActionMap(tempActionMap);
        controlsText.text = controls[random];
    }
    private void OnJump()
    {
        if (groundCheckRef.grounded)
        {
            RB.velocity = new Vector2(RB.velocity.x, jumpHeight);
            print("jump");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Finish"))
        {
            transform.position = startSpot.transform.position;
            TimerRef.ResetTimer();
            OnSwitchAction();
        }
    }
}
