using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    public FadeScreen fadeScreen;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BackToMenu()
    {
        fadeScreen.FadeOut();

        StartCoroutine(FadeTimer());
    }

    public IEnumerator FadeTimer()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(0);
    }
}
