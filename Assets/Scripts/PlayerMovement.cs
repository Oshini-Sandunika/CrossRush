using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D body;
    private Animator anim;
    private bool grounded;

    private void Awake()
    {
        //Grab reference for rigidbody and animator from object 
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        body.linearVelocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.linearVelocity.y);

        //Flip player when moving left and right
        if(horizontalInput > 0.01f)
        {
            transform.localScale = Vector3.one;
        }
        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-1,1,1);
        }

        if (Input.GetKey(KeyCode.Space) && grounded)
        {
            Jump();
        }

        //Set animator parameters
        anim.SetBool("Run", horizontalInput != 0);
        anim.SetBool("grounded", grounded);
    
    }

    private void Jump()
    {
        body.linearVelocity = new Vector2(body.linearVelocity.x, speed);
        grounded = false;
        anim.SetTrigger("jump");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            grounded = true;
        }
    }



}
