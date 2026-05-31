using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    public FadeScreen fadeScreen;


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
