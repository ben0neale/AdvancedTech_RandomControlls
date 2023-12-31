using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [SerializeField] GameObject TextController;
    GameObject Player;
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    public void LoadCurrentScene()
    {
        Player.GetComponent<PlayerController>().RestartLevel();
    }
    public void LoadLevel()
    {
        if (PlayerPrefs.GetInt("UseKM") != -1 || PlayerPrefs.GetInt("UseController") != -1 || PlayerPrefs.GetInt("UseWii") != -1)
            SceneManager.LoadScene(1);      
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadInfo()
    {
        SceneManager.LoadScene(2);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void StartLevel()
    {
        GameObject.FindGameObjectWithTag("PreGame").gameObject.SetActive(false);
        StartCoroutine(TextController.GetComponent<Timer>().Count());
    }
    public void LoadControlSchemes()
    {
        SceneManager.LoadScene(3);
    }
}
