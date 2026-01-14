using UnityEngine;

public class ApplyButtonAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void PlayApplyAnimation()
    {
        animator.SetTrigger("Apply");
    }
}
