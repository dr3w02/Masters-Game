using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

[System.Serializable]

public class Haptic 
{

    [Range(0.0f, 1.0f)]
    public float intensity;
    public float duration;

    public void TriggerHaptic(BaseInteractionEventArgs eventArgs)
    {
        if (eventArgs == null) return;
        if (eventArgs.interactorObject is UnityEngine.XR.Interaction.Toolkit.Interactors.XRBaseInputInteractor controllerInteractor)
        {
            TriggerHaptic(controllerInteractor.xrController);
        }
    }

   

    public void TriggerHaptic(XRBaseController controller)
    {
        if (controller == null) return;

        if (intensity > 0)
            controller.SendHapticImpulse(intensity, duration);
    }



}

public class HapticInteractable : MonoBehaviour
{
    public Haptic hapticOnActive;
    public Haptic hapticHoverEntered;
    public Haptic hapticHoverExited;
    public Haptic hapticSelectEntered;
    public Haptic hapticSelectExited;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        XRBaseInteractable interactable = GetComponent<XRBaseInteractable>();

        interactable.activated.AddListener(hapticOnActive.TriggerHaptic);

        interactable.hoverEntered.AddListener(hapticHoverEntered.TriggerHaptic);
        interactable.hoverExited.AddListener(hapticHoverExited.TriggerHaptic);
        interactable.selectEntered.AddListener(hapticSelectEntered.TriggerHaptic);
        interactable.selectExited.AddListener(hapticSelectExited.TriggerHaptic);
    }

}
