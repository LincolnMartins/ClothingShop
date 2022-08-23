using UnityEngine;

public class ClothAnimController : MonoBehaviour
{
    private Animator animator;
    private Animator parentAnimator;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        parentAnimator = transform.parent.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Horizontal", parentAnimator.GetFloat("Horizontal"));
        animator.SetFloat("Vertical", parentAnimator.GetFloat("Vertical"));
        animator.SetFloat("Speed", parentAnimator.GetFloat("Speed"));
        animator.SetFloat("Idle_Horizontal", parentAnimator.GetFloat("Idle_Horizontal"));
        animator.SetFloat("Idle_Vertical", parentAnimator.GetFloat("Idle_Vertical"));
    }
}