using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoController : MonoBehaviour
{
    public float jumpPower = 2f;
    public bool isDown = false;
    public bool isGround = true;
    // Start is called before the first frame update
    private Animator Anim;
    private Rigidbody2D rb;


    public Transform groundCheckPoint;
    public LayerMask whatIsGround; //ground인지 비교할 Mask

    void Start()
    {
        Anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        Anim.SetBool("isGround", true);
    }

    // Update is called once per frame
    void Update()
    {
        isGround = Physics2D.OverlapCircle(groundCheckPoint.position, 0.2f, whatIsGround);

        if (Input.GetKeyDown(KeyCode.Space) && isGround.Equals(true))
        {
            
            //rb.AddForce(Vector2.up * jumpPower , ForceMode2D.Impulse);
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            //Anim.SetBool("isGround", false);
        }

        Anim.SetBool("isGround", isGround);

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            isDown = !isDown;
            Anim.SetBool("isDown", true);
        }

        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            isDown = !isDown;
            Anim.SetBool("isDown", false);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            Anim.SetBool("isGround", true);
        }
    }
}
