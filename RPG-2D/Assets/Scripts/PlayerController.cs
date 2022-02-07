using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private Animator animator;
    [SerializeField]
    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        rb2d.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * speed;
        animator.SetFloat("MoveX", rb2d.velocity.x);
        animator.SetFloat("MoveY", rb2d.velocity.y);

        if(Input.GetAxisRaw("Horizontal") == 1 || 
           Input.GetAxisRaw("Horizontal") == -1 ||
           Input.GetAxisRaw("Vertical") == 1 ||
           Input.GetAxisRaw("Vertical") == -1)
        {
            animator.SetFloat("LastMoveX", Input.GetAxisRaw("Horizontal"));
            animator.SetFloat("LastMoveY", Input.GetAxisRaw("Vertical"));
        }
    }
}
