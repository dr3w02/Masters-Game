using TMPro;
using UnityEngine;

public class SanityUI : MonoBehaviour
{
     public SanityScore _sanityScore;
    [SerializeField] private TextMeshProUGUI sanityText;

    void Start()
    {
       
    }

    void Update()
    {
        sanityText.text = "Sanity: " + Mathf.RoundToInt(_sanityScore.sanity);
    }
}
