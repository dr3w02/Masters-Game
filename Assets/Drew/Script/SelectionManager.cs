using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;


public class SelectionManager : MonoBehaviour
{
    [SerializeField] private Material highlightMaterial;
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private string selectableTag = "Selectable";

    private Transform _selection;
    public GameObject ghost;

    public AudioSource GhostAudio;

    public PlayableDirector timeline;

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward, Color.green);


        RaycastHit hit;

        Ray ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray, out hit, 4000f))
        {
            var selection = hit.transform;
            if (selection.CompareTag(selectableTag))
            {
                var selectionRenderer = selection.GetComponent<Renderer>();

               // if (selectionRenderer != null)
               // {
                    Debug.Log("hit something");
                //selectionRenderer.material = highlightMaterial;

                ghost.SetActive(false);
                GhostAudio.Pause();
                if (timeline != null)
                {
                   timeline.Pause();
                }
               

                _selection = selection;
            }
        }


        else
        {

            if (_selection != null)
            {
                Debug.Log("hit nothing");
            
                //var selectionRenderer = _selection.GetComponent<Renderer>();
                //selectionRenderer.material = defaultMaterial;
                _selection = null;


                ghost.SetActive(true);
                GhostAudio.Play();
                if (timeline != null)
                {
                    timeline.Play();
                }

            }

        }







    }
}
