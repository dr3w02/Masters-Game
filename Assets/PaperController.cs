using UnityEngine;

public class PaperController : MonoBehaviour
{
    private Animator paperAnim;
    public bool paperFall;
    public GameObject Paper;

    void Start()
    {
        Paper.SetActive(true);
        // Get the Animator component attached to the character
        paperAnim = GetComponent<Animator>();
    }

    public void PlayPaperAnim()
    {
     
       paperAnim.SetBool("PlayAnim", true);
     
    }

   
  
}
