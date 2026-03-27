using UnityEngine;

public class StartMenu : MonoBehaviour
{
    public GameObject start;
    public GameObject controls;
    public GameObject credits;
    public GameObject exit;
    public GameObject back;


    public void StartScreen()
    {
        start.SetActive(true);
        controls.SetActive(true);
        credits.SetActive(true);
        exit.SetActive(true);
        back.SetActive(false);
    }

    public void ControlsUI()
    {
        start.SetActive(false);
        controls.SetActive(false);
        credits.SetActive(false);
        exit.SetActive(false);
        back.SetActive(true);
    }

    public void CreditsUI()
    {
        start.SetActive(false);
        controls.SetActive(false);
        credits.SetActive(false);
        exit.SetActive(false);
        back.SetActive(true);
    }

    public void ExitUI()
    {
        Application.Quit();
        Debug.Log("Game Quit");
    }

    

}
