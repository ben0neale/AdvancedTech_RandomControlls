using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseController : MonoBehaviour
{
    [SerializeField] Toggle KMToggle;
    [SerializeField] Toggle ControllerToggle;
    [SerializeField] Toggle WiiToggle;

    public static int KM = 1;
    public static int Controller = 1;
    public static int Wii = 1;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("UseKM"))
        {
            if (PlayerPrefs.GetInt("UseKM") == 1)
                KMToggle.isOn = true;
            else
                KMToggle.isOn = false;
            KM = PlayerPrefs.GetInt("UseKM");
        }
        if (PlayerPrefs.HasKey("UseController"))
        {
            if (PlayerPrefs.GetInt("UseController") == 1)
                ControllerToggle.isOn = true;
            else
                ControllerToggle.isOn = false;
            Controller = PlayerPrefs.GetInt("UseController");
        }
        if (PlayerPrefs.HasKey("UseWii"))
        {
            if (PlayerPrefs.GetInt("UseWii") == 1)
                WiiToggle.isOn = true;
            else
                WiiToggle.isOn = false;
            Wii = PlayerPrefs.GetInt("UseWii");
        }
    }
    public void ToggleWii()
    {
        if (WiiToggle.isOn)
            Wii = 1;
        else
            Wii = -1;

        PlayerPrefs.SetInt("UseWii", Wii);
    }

    public void ToggleKM()
    {
        if (KMToggle.isOn)
            KM = 1;
        else
            KM = -1;
        PlayerPrefs.SetInt("UseKM", KM);
    }

    public void ToggleController()
    {
        if (ControllerToggle.isOn)
            Controller = 1;
        else
            Controller = -1;
        PlayerPrefs.SetInt("UseController", Controller);
    }
}
