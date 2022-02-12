using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private Animator animator;
    [SerializeField]
    private float speed;
    public string transitionAreaName;

    public static PlayerController instance;

    private Vector3 bottomLeft;
    private Vector3 topRight;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
            instance = this;
        else
        {
            if(instance != this)
                Destroy(gameObject);
        }

        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        DontDestroyOnLoad(gameObject);
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

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, bottomLeft.x, topRight.x),
                                        Mathf.Clamp(transform.position.y, bottomLeft.y, topRight.y),
                                        transform.position.z);
    }

    public void SetBounds(Vector3 BL, Vector3 TR)
    {
        bottomLeft = BL + new Vector3(1, 1, 0)/2;
        topRight = TR + new Vector3(-1, -1, 0)/2;
    }
}
