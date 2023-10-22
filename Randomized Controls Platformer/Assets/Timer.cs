using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timer;
    private float time = 0;
    private int characterSize = 3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time = Time.realtimeSinceStartup;
        if (time > 99.9)
        {
            characterSize = 5;
        }
        else if (time > 9.9)
        {
            characterSize = 4;
        }
        timer.text = time.ToString().Substring(0,characterSize);   
    }
}
