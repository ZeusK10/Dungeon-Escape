using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

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
    [SerializeField]
    private GameObject joystick;
    public int Health { get; set; }

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        playerAnimScript = GetComponent<PlayerAnimation>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _swordSpriterenderer = transform.GetChild(1).GetComponent<SpriteRenderer>();
        Health = 4;
    }

    void Update()
    {

        if (isJumping == true && CrossPlatformInputManager.GetButtonDown("B_Button")) 
        {
            isAttacking = true;
            playerAnimScript.Attack();
            StartCoroutine(WaitForAttack());
        }

        if (isAttacking==false && GameManager.Instance.isPlayerDead == false)
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

        if ((Input.GetKeyDown(KeyCode.Space)||CrossPlatformInputManager.GetButtonDown("A_Button")) && isJumping==true)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);
            playerAnimScript.Jump(true);
            StartCoroutine(CheckGroundRoutine());
        }

        //float horizontalInput = Input.GetAxisRaw("Horizontal") * _speed;
        float horizontalInput = CrossPlatformInputManager.GetAxis("Horizontal");
        float joystickPosition = joystick.GetComponent<RectTransform>().anchoredPosition.x;
        if (Mathf.Abs(joystickPosition) > 25)
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
        if(GameManager.Instance.isPlayerDead==false)
        {
            Health--;
            UIManager.Instance.UpdateLives(Health);
            if (Health < 1)
            {
                GameManager.Instance.isPlayerDead = true;
                playerAnimScript.Dead();
            }
        }
        
    }

    public void GetGems(int gems)
    {
        diamonds += gems;
        UIManager.Instance.UpdateGemsCount(diamonds);
        UIManager.Instance.OpenShop(diamonds);
    }
}
