using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator _playerAnim;
    Animator _swordAnim;
    void Start()
    {
        _playerAnim = GetComponentInChildren<Animator>();
        _swordAnim = transform.GetChild(1).GetComponent<Animator>();
        //_swordAnim = this.transform.Find("Sword_Arc").GetComponent<Animator>();
    }

    public void Walk(float move)
    {
        _playerAnim.SetFloat("Walk", move);
        _playerAnim.SetBool("IsRunning", false);
    }

    public void Jump(bool isJumping)
    {
        _playerAnim.SetBool("IsJumping", isJumping);
    }

    public void Run(float move,bool isRunning)
    {
        _playerAnim.SetBool("IsRunning", isRunning);
        _playerAnim.SetFloat("Walk", move);
    }

    public void Attack()
    {
        _playerAnim.SetTrigger("Attack");
        _swordAnim.SetTrigger("SwordAnimation");
    }

    public void Dead()
    {
        _playerAnim.SetTrigger("Death");
    }
}
