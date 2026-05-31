using UnityEngine;

public class HallwayDeduction : MonoBehaviour
{

    [SerializeField]
    private NPCPicker _npcPicker;


    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("H_NPC"))
        {
       ;
           // _npcPicker.EndOfPath();
        }
        else
        {

        }
        
    }
}
