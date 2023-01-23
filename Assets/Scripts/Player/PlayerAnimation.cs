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
        // Get the direction of the player's movement
        Vector2 direction = _rigidbody2D.velocity.normalized;

        // Check the direction and set the animation state accordingly
        if (direction != Vector2.zero)
        {
            _animator.SetBool("isIdle", false);
            CheckSpeed();
            if(direction.x < 0)
            {
                _sprite.flipX = true;
            }
            else if(direction.x > 0)
            {
                _sprite.flipX = false;
            }
        }
        else
        {
            _animator.SetBool("isIdle", true);
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
