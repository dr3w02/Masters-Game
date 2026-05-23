using TMPro;
using UnityEngine;

namespace Script
{
    public class StartAnimationAtRandomFrame : MonoBehaviour
    {
        private Animator animator;
        private void Start()
        {
            animator = GetComponent<Animator>();

            var State = animator.GetCurrentAnimatorStateInfo(0);

            animator.Play(State.fullPathHash, 0, Random.Range(0f, 1f));

        }
    }
}
