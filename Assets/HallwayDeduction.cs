using UnityEngine;

public class HallwayDeduction : MonoBehaviour
{

    [SerializeField]
    private NPCPicker _npcPicker;


    private void Start()
    {
        _npcPicker = FindFirstObjectByType<NPCPicker>();
            Debug.Log("GameObect Tag" + gameObject.tag);
    }
   
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("ahhhh" + other.gameObject.tag);

        if (other.CompareTag("H_NPC"))
        {
            Debug.Log("Done");
            _npcPicker.EndOfPath();
        }
        else
        {

        }
        
    }
}
