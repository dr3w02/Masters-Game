using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public string sceneName;

    public void GoodEnding()
    {
       
        SceneManager.LoadScene(sceneName);
      
    }
}
