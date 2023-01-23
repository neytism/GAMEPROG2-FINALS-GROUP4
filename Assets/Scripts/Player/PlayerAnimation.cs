using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    //disabling weapon for death animation
    [SerializeField] private GameObject _weaponHolder;
    
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private SpriteRenderer _sprite;
    private PlayerMovement _playerMovement;


    private bool _isWalking;
    private bool _isRunning;

    private void OnEnable()
    {
        PlayerHurt.ReduceHealth += Hurt;
        PlayerHealth.PlayerDeath += Death;
        UserInterfaceManager.KillSelf += Death;
    }

    private void OnDisable()
    {
        PlayerHurt.ReduceHealth -= Hurt;
        PlayerHealth.PlayerDeath -= Death;
        UserInterfaceManager.KillSelf -= Death;
    }

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
        _playerMovement = GetComponent<PlayerMovement>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        AnimatePlayerMovement();
    }

    private void AnimatePlayerMovement()
    {
        if (_rigidbody2D.velocity.x < 0 && _rigidbody2D.velocity.y < 0)
        {
            //quadrant 3
            _sprite.flipX = true;
           CheckSpeed();
            
        }
        else if (_rigidbody2D.velocity.x > 0 && _rigidbody2D.velocity.y > 0)
        {
            //quadrant 1
            _sprite.flipX = false;
            CheckSpeed();
        }
        else if (_rigidbody2D.velocity.x < 0 && _rigidbody2D.velocity.y > 0)
        {
            //quadrant 2
            _sprite.flipX = true;
            CheckSpeed();
        }
        else if (_rigidbody2D.velocity.x > 0 && _rigidbody2D.velocity.y < 0)
        {
            //quadrant 4
            _sprite.flipX = false;
            CheckSpeed();
        }
        else if (_rigidbody2D.velocity.y > 0)
        {
            // up
            CheckSpeed();
        }
        else if (_rigidbody2D.velocity.y < 0)
        {
            // down
            CheckSpeed();
        }else if (_rigidbody2D.velocity.x < 0)
        {
            // left
            _sprite.flipX = true;
            CheckSpeed();
        }
        else if (_rigidbody2D.velocity.x > 0)
        {
            // right
            _sprite.flipX = false;
            CheckSpeed();
        }
        else
        {
            //not moving
            _animator.SetBool("isWalking", false);
            _animator.SetBool("isRunning", false);
        }

    }

    private void CheckSpeed()
    {
        if(_playerMovement.isRunning)
        {
            _animator.SetBool("isRunning", true);
            _animator.SetBool("isWalking", false);
        }
        else
        {
            _animator.SetBool("isRunning", false);
            _animator.SetBool("isWalking", true);
        }
    }

    private void Hurt()
    {
        _animator.SetTrigger("Hurt");
    }

    private void Death()
    {
        _weaponHolder.SetActive(false);
        _animator.SetTrigger("Death");
    }
}
