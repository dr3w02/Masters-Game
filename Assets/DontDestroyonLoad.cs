using UnityEngine;

public class DontDestroyonLoad : MonoBehaviour
{
    public static DontDestroyonLoad instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
  
}
