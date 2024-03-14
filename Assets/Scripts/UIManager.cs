using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Scene game, menu;
    public GameObject mainMenu, helpMenu, creditsMenu;

    // Start is called before the first frame update
    void Start()
    {
        //helpMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadMenu()
    {
        //string sceneIndex = menu.buildIndex;
        SceneManager.LoadScene(0);
    }

    public void HowToPlay()
    {
        mainMenu.SetActive(false);
        creditsMenu.SetActive(false);
        helpMenu.SetActive(true);
    }

    public void Credits()
    {
        mainMenu.SetActive(false);
        helpMenu.SetActive(false);
        creditsMenu.SetActive(true);
    }

    public void MainMenu()
    {
        helpMenu.SetActive(false);
        creditsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }
}
