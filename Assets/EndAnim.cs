using UnityEngine;

public class EndAnim : MonoBehaviour
{
    public Laptop laptop;

    public void Start()
    {
        laptop = GetComponent<Laptop>();
    }

    public void Stop()
    {
        laptop.EndAnim();
    }
    //hi
}
