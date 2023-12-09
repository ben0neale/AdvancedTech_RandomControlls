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
    [SerializeField] GameObject Controller;
    [SerializeField] GameObject Keyboard;
    [SerializeField] GameObject WiiRemote;
    [SerializeField] GameObject highscoreTable;
    [SerializeField] GameObject GameStateController;
    Animator animator;
    GameStateController GameStateRef;
    Timer TimerRef;
    bool CanMove = false;

    private InputMaster inputMaster;
    private PlayerInput playerInput;
    private Rigidbody2D RB;
    private GroundCheck groundCheckRef;

    private string tempActionMap;

    List<string> actionMaps = new List<string>();
    List<string> controls = new List<string>();
    public List<TextMeshProUGUI> controlsText;

    public float moveSpeed;
    public float maxSpeed;
    private float x;
    public float jumpHeight;

    WiimoteApi.Wiimote wiiRemote;
    int num;
    bool wiiControls1 = false;
    bool wiiControls2 = false;

    int random;
    public bool KeyboardOnly;
    HighScoreTable HST;


    // Start is called before the first frame update
    void Awake()
    {
        GameStateRef = GameStateController.GetComponent<GameStateController>();
        RB = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();

        groundCheckRef = GetComponentInChildren<GroundCheck>();
        TimerRef = TimerOBJ.GetComponent<Timer>();

        HST = highscoreTable.GetComponent<HighScoreTable>();

        highscoreTable.SetActive(false);
        
        animator = GetComponent<Animator>();
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

        controls.Add("Controls - Wii Remote:\nRight - D-Pad Right\nLeft - D-Pad Left\nJump - 2");
        controls.Add("Controls - Wii Remote:\nRight - D-Pad Left\nLeft - D-Pad Right\nJump - B");

        controlsText[0].text = controls[0];
        controlsText[1].text = controls[0];
        //tempActionMap = actionMaps[Random.Range(0, actionMaps.Count)];

        InitWiiRemotes();
        OnSwitchAction();
    }

    private void Update()
    {
        if (WiimoteApi.WiimoteManager.Wiimotes.Count != 0)
        {
            do
            {
                num = wiiRemote.ReadWiimoteData();
            } while (num > 0);
        }

        if (TimerRef.CountDownFinished)
            CanMove = true;
        else
            CanMove = false;

        animator.SetFloat("speed", Mathf.Abs(x));
    }
    private void FixedUpdate()
    {
        if (CanMove)
        {
            Movement();
        }
        if (WiimoteApi.WiimoteManager.Wiimotes.Count != 0 && CanMove)
        {
            WiiRemoteMovement();
            WiiRemoteJump();
        }
    }

    void InitWiiRemotes()
    {
        WiimoteApi.WiimoteManager.FindWiimotes();
        if (WiimoteApi.WiimoteManager.Wiimotes.Count != 0)
        {
            wiiRemote = WiimoteApi.WiimoteManager.Wiimotes[0];
            foreach (WiimoteApi.Wiimote remote in WiimoteApi.WiimoteManager.Wiimotes)
            {
                print("Connected");
                remote.SendPlayerLED(true, false, true, false);
            }

            wiiRemote.SetupIRCamera(WiimoteApi.IRDataType.BASIC);
        }

    }

    void WiiRemoteMovement()
    {
        if (wiiControls1)
        {
            if (wiiRemote.Button.d_up)
                x = -1;
            else if (wiiRemote.Button.d_down)
                x = 1;
            else
                x = 0;
        }
        else if (wiiControls2)
        {
            if (wiiRemote.Button.d_up)
                x = 1;
            else if (wiiRemote.Button.d_down)
                x = -1;
            else
                x = 0;
        }
    }

    void WiiRemoteJump()
    {
        if (wiiRemote.Button.two)
        {
            Jump();
        }
    }

    void Movement()
    {
        if (RB.velocity.x < maxSpeed)
        {
            RB.AddRelativeForce(new Vector2(x * moveSpeed, 0));
        }
       // RB.velocity = new Vector2(x * moveSpeed, RB.velocity.y);
        if (transform.position.y < -8)
        {
            Respawn();
        }
    }

    void Respawn()
    {
        transform.position = startSpot.transform.position;
    }

    void Jump()
    {
        if (groundCheckRef.grounded)
        {
            animator.Play("Jump");
            RB.velocity = new Vector2(RB.velocity.x, jumpHeight);
        }
    }

    void FinishedWithWiiRemotes()
    {
        foreach (WiimoteApi.Wiimote remote in WiimoteApi.WiimoteManager.Wiimotes)
        {
            WiimoteApi.WiimoteManager.Cleanup(remote);
        }
    }

    private void OnMove(InputValue inputValue)
    {
        if (!wiiControls1 && !wiiControls2)
        {
            x = inputValue.Get<Vector2>().x;
        }
    }
    private void OnSwitchAction()
    {
        print(UseController.KM);
        print(UseController.Controller);
        print(UseController.Wii);
        if (KeyboardOnly)
        {
            random = Random.Range(0, 3);
            playerInput.SwitchCurrentActionMap(actionMaps[random]);
        }
        else
        {
            x = 0;
            if (UseController.Wii)
                random = Random.Range(0, actionMaps.Count + 2);
            else
                random = Random.Range(0, actionMaps.Count);

            print(random.ToString());
            if (random < 3 && !UseController.KM)
            {
                random = -1;
                OnSwitchAction();
            }
            else if (random < 6 && !UseController.Controller)
            {
                random = -1;
                OnSwitchAction();
            }
            if (random < actionMaps.Count)
            {
                tempActionMap = actionMaps[random];
                wiiControls1 = false;
                wiiControls2 = false;

                while (tempActionMap == playerInput.currentActionMap.name)
                {
                    random = Random.Range(0, actionMaps.Count);
                    tempActionMap = actionMaps[random];
                }
                playerInput.SwitchCurrentActionMap(tempActionMap);
            }
            else
            {
                if (random == actionMaps.Count)
                {
                    wiiControls2 = false;
                    wiiControls1 = true;
                }
                else
                {
                    wiiControls1 = false;
                    wiiControls2 = true;
                }
            }
        }
        if (random < 3)
        {
            HST.highscoretableValue = 1;
        }
        else if (random < 6)
        {
            HST.highscoretableValue = 2;
        }
        else if (random < 8)
        {
            HST.highscoretableValue = 3;
        }

        controlsText[0].text = controls[random];
        controlsText[1].text = controls[random];
        UpdateSprite(random);       
    }

    void UpdateSprite(int _random)
    {
        if (_random <= 2)
        {
            Keyboard.SetActive(true);
            Controller.SetActive(false);
            WiiRemote.SetActive(false);
        }
        else if (_random <= 5)
        {
            Keyboard.SetActive(false);
            Controller.SetActive(true);
            WiiRemote.SetActive(false);
        }
        else
        {
            Keyboard.SetActive(false);
            Controller.SetActive(false);
            WiiRemote.SetActive(true);
        }
    }
    private void OnJump()
    {
        if (CanMove)
        {
            Jump();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Finish"))
        {
            transform.position = startSpot.transform.position;
            OnSwitchAction();
            highscoreTable.SetActive(true);
            TimerRef.ResetTimer();
        }
        else if (collision.gameObject.CompareTag("Respawn"))
        {
            Respawn();
        }
    }
}
