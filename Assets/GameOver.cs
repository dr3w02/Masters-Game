using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public string sceneName;

    public void GoodEnding()
    {
        StartCoroutine(WaitForFade());
    }

    public IEnumerator WaitForFade()
    {
        Debug.Log("Waiting...");

        yield return new WaitForSeconds(10f);

        SceneManager.LoadScene(sceneName);

    }
  
}
