using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSceneMananger : MonoBehaviour
{

    public GazeRayCast gazeRayCast;

    public MenuGaze menuGaze;

    public void Start()
    {
        gazeRayCast = GetComponent<GazeRayCast>();
        menuGaze = GetComponent<MenuGaze>();

        SceneManager.sceneLoaded += OnSceneLoaded;
        UpdateComponents(SceneManager.GetActiveScene().name);

      
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UpdateComponents(scene.name);
    }
    void UpdateComponents(string sceneName)
    {
        bool isMenu;

        if (sceneName == "MenuScene")
        {
            isMenu = true;
        }
        else
        {
            isMenu = false;
        }

        if (menuGaze != null)
        {
            menuGaze.enabled = isMenu;
            menuGaze.alreadySelected = false;
        }

        if (gazeRayCast != null)
        {
            gazeRayCast.enabled = !isMenu;
        }


    }
    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
