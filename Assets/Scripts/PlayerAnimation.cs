using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator _playerAnim;
    void Start()
    {
        _playerAnim = GetComponentInChildren<Animator>();
    }

    public void Walk(float move)
    {
        _playerAnim.SetFloat("Walk", move);
    }
}
