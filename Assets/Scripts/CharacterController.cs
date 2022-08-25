using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float movespeed = 3f;

    private Rigidbody2D rb;
    private Animator animator;

    private Vector2 movement;

    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        animator.SetFloat("Idle_Vertical", -1); //Set initial idle animation
    }

    // Update is called once per frame
    void Update()
    {
        //Get Controls Info
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        //Set animations
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        if(animator.GetFloat("Speed") != 0)
        {
            animator.SetFloat("Idle_Horizontal", movement.x);
            animator.SetFloat("Idle_Vertical", movement.y);
        }

    }

    // FixedUpdate is called after Update and is used to resolve physics
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * movespeed * Time.fixedDeltaTime); //Move Character
    }
}
