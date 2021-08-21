using Assets.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovmentScript : MonoBehaviour
{
    // Start is called before the first frame update

    private Animator animator;
    private Rigidbody2D body;

    public float Speed = 5f;

    public Transform GroundPositionCheck;

    public LayerMask GroundLayer;


    private bool jumped;

    private bool isGrounded;
    public float JumpPower = 7f;
    void Start()
    {

    }
    void Update()
    {
        CheckIfGrounded();
        PlayerJump();
    }


    private void Awake()
    {
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        PlayerWalk();
    }

    private void PlayerWalk()
    {
        var h = Input.GetAxisRaw("Horizontal");
        if (h > 0) //right
        {
            body.velocity = new Vector2(Speed, body.velocity.y);
            ChangeDirection(Direction.Right);
        }

        else if (h < 0)
        {
            body.velocity = new Vector2(-Speed, body.velocity.y);
            ChangeDirection(Direction.Left);

        }
        else
        {
            body.velocity = new Vector2(0f, body.velocity.y);

        }

        animator.SetInteger("Speed", Mathf.Abs((int)body.velocity.x));
    }



    private void ChangeDirection(Direction direction)
    {
        var tempScale = transform.localScale;

        tempScale.x = (int)direction;

        transform.localScale = tempScale;
    }

    void CheckIfGrounded()
    {
        isGrounded = Physics2D.Raycast(GroundPositionCheck.position, Vector2.down, 0.1f, GroundLayer);

        if (isGrounded)
        {
            // and we jumped before
            if (jumped)
            {

                jumped = false;

                animator.SetBool("Jump", false);
            }
        }

    }

    void PlayerJump()
    {
        if (isGrounded)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                jumped = true;
                body.velocity = new Vector2(body.velocity.x, JumpPower);

                animator.SetBool("Jump", true);
            }
        }
    }
}
