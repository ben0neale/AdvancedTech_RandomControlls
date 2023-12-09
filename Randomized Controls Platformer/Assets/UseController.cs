using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseController : MonoBehaviour
{
    public static bool KM = true;
    public static bool Controller = true;
    public static bool Wii = true;

    public void ToggleWii()
    {
        Wii = !Wii;
    }

    public void ToggleKM()
    {
        KM = !KM;
    }

    public void ToggleController()
    {
        Controller = !Controller;
    }
}
