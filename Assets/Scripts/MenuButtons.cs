using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public GameObject MenuPanel;
    public GameObject CreditsPanel;

    // Start is called before the first frame update
    void Start()
    {
        MenuPanel.SetActive(true);
        CreditsPanel.SetActive(false);

    }
     public void ShowCredits()
    {
        MenuPanel.SetActive(false);
        CreditsPanel.SetActive(true);
    }
    public void ShowMenuPanel()
    {
        MenuPanel.SetActive(true);
        CreditsPanel.SetActive(false);
    }
    public void PlayGame()
    {
        //Plays the game...Coming soon
        //SceneManager.LoadScene("GameStart", LoadSceneMode.Single);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
