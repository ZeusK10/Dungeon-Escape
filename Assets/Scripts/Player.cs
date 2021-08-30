using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private Rigidbody2D _rigidbody;
    [SerializeField]
    private float _jumpForce = 5f;
    [SerializeField]
    private float _speed = 3.0f;
    private PlayerAnimation playerAnimScript;
    private SpriteRenderer _spriteRenderer;


    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        playerAnimScript = GetComponent<PlayerAnimation>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        Movement();
    }

    void Movement()
    {

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded() == true)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);
        }

        float horizontalInput = Input.GetAxisRaw("Horizontal") * _speed;
        Flip(horizontalInput);
        playerAnimScript.Walk(Mathf.Abs(horizontalInput));
        _rigidbody.velocity = new Vector2(horizontalInput, _rigidbody.velocity.y);
    }

    void Flip(float move)
    {
        if (move < 0)
        {
            _spriteRenderer.flipX = true;
        }
        else if (move > 0)
        {
            _spriteRenderer.flipX = false;
        }
    }

    bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down,0.8f,1<<6);
        
        if(hit.collider!=null)
        {
            
            return true;
        }
        return false;
    }
}
