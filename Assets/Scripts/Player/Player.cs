using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour,IDamageable
{
    public int diamonds;
    private Rigidbody2D _rigidbody;
    [SerializeField]
    private float _jumpForce = 5f;
    [SerializeField]
    private float _speed = 3.0f;
    private PlayerAnimation playerAnimScript;
    private SpriteRenderer _spriteRenderer;
    private SpriteRenderer _swordSpriterenderer;
    private bool isJumping,isAttacking;

    public int Health { get; set; } = 5;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        playerAnimScript = GetComponent<PlayerAnimation>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _swordSpriterenderer = transform.GetChild(1).GetComponent<SpriteRenderer>();
        
    }

    void Update()
    {

        if (isJumping == true && Input.GetMouseButtonDown(0))
        {
            isAttacking = true;
            playerAnimScript.Attack();
            StartCoroutine(WaitForAttack());
        }

        if (isAttacking==false)
        {
            Movement();
        }
        
        if(isJumping==false)
        {
            IsGrounded();        
        }

    }

    IEnumerator WaitForAttack()
    {
        yield return new WaitForSeconds(1.09f);
        isAttacking = false;
    }

    void Movement()
    {

        if (Input.GetKeyDown(KeyCode.Space) && isJumping==true)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);
            playerAnimScript.Jump(true);
            StartCoroutine(CheckGroundRoutine());
        }

        float horizontalInput = Input.GetAxisRaw("Horizontal") * _speed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            horizontalInput *= 2;
            Flip(horizontalInput);
            playerAnimScript.Run(Mathf.Abs(horizontalInput), true);
            _rigidbody.velocity = new Vector2(horizontalInput, _rigidbody.velocity.y);
        }
        else
        {
            Flip(horizontalInput);
            playerAnimScript.Walk(Mathf.Abs(horizontalInput));
            _rigidbody.velocity = new Vector2(horizontalInput, _rigidbody.velocity.y);
        }
    }

    IEnumerator CheckGroundRoutine()
    {
        yield return new WaitForSeconds(0.5f);
        isJumping = false;
    }

    void Flip(float move)
    {
        if (move < 0)
        {
            _spriteRenderer.flipX = true;

            _swordSpriterenderer.flipY = true;

            Vector3 newPos = _swordSpriterenderer.transform.localPosition;
            newPos.x = -0.77f;
            _swordSpriterenderer.transform.localPosition = newPos;
        }
        else if (move > 0)
        {
            _spriteRenderer.flipX = false;

            _swordSpriterenderer.flipY = false;

            Vector3 newPos = _swordSpriterenderer.transform.localPosition;
            newPos.x = 0.77f;
            _swordSpriterenderer.transform.localPosition = newPos;
        }
    }

    bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down,0.8f,1<<6);
        
        if(hit.collider!=null)
        {
            isJumping = true;
            playerAnimScript.Jump(false);
            return true;
        }
        return false;
    }

    public void Damage()
    {
        Health--;
        if(Health<1)
        {
            playerAnimScript.Dead();
        }
    }

    public void GetGems(int gems)
    {
        diamonds += gems;
    }
}
