using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public GameObject start;
    public GameObject controls;
    public GameObject credits;
    public GameObject exit;
    public GameObject back;

    public GameObject creditsText;
    public GameObject controlsText;

    public FadeScreen fade;


    public void Start()
    {
        fade = FindFirstObjectByType<FadeScreen>();
  
    }
    public void Back()
    {
        start.SetActive(true);
        controls.SetActive(true);
        credits.SetActive(true);
        exit.SetActive(true);
        controlsText.SetActive(false);
        creditsText.SetActive(false);
        back.SetActive(false);
    }

    public void ControlsUI()
    {
        start.SetActive(false);
        controls.SetActive(false);
        credits.SetActive(false);
        exit.SetActive(false);

        back.SetActive(true);
        controlsText.SetActive(true);
    }

    public void CreditsUI()
    {
        start.SetActive(false);
        controls.SetActive(false);
        credits.SetActive(false);
        exit.SetActive(false);

        back.SetActive(true);
        creditsText.SetActive(true);
    }

    public void ExitUI()
    {
        Application.Quit();
        Debug.Log("Game Quit");
    }
    public void GoToNextScene()
    {
        StartCoroutine(WaitForFade());
    }

    public IEnumerator WaitForFade()
    {
        Debug.Log("Waiting...");
       
        yield return new WaitForSeconds(10f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }



}
