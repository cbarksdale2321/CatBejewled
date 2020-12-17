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
        //Starts in Menu
        //Hides the Credits
        MenuPanel.SetActive(true);
        CreditsPanel.SetActive(false);

    }
     public void ShowCredits()
    {
        //Shows Credits when not in Menu
        //Hides Menu and the buttons 
        MenuPanel.SetActive(false);
        CreditsPanel.SetActive(true);
    }
    public void ShowMenuPanel()
    {
        //Shows Menu Options when not in Credits
        //Hides Credits Menu
        MenuPanel.SetActive(true);
        CreditsPanel.SetActive(false);
    }
    public void PlayGame()
    {
        //Plays the game...
        //Switch out the "SampleScene" with the game scene 
        SceneManager.LoadScene("djscene", LoadSceneMode.Single);
    }
    public void QuitGame()
    {
        //Closes Application
        Application.Quit();

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
