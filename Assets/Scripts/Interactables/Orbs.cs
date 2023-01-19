using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbs : MonoBehaviour
{
    public static event Action<float> ExperienceCollected;
    public static event Action EnergyOrbCollected;
    
    public float xpAmount = 1;
    
    public float speed;
    private float distance;

    private GameObject _player;
    private Rigidbody2D _rb;

    private bool _isFollow;

    private void OnEnable()
    {
        _isFollow = false;
    }

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _rb = GetComponent<Rigidbody2D>();
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("PickUpTrigger"))
        {
            if (this.gameObject.tag.Equals("Experience"))
            {
                ExperienceCollected?.Invoke(xpAmount);
                gameObject.GetComponent<TrailRenderer>().Clear();
            } else if (this.gameObject.tag.Equals("EnergyOrb"))
            {
                EnergyOrbCollected?.Invoke();
            }
            
            gameObject.SetActive(false);
            
        }
        
        if (col.gameObject.tag.Equals("PickUpRadius"))
        {
            _isFollow = true;
        }

    }
    
    private void Update()
    {
        if (_isFollow)
        {
            Chase();
        }
    }
    
    public void Chase() //follow player while facing target
    {
        Vector2 direction = _player.transform.position - transform.position;
        direction.Normalize();

        transform.position = Vector2.MoveTowards(transform.position, _player.transform.position, speed * Time.deltaTime);
    }
}

//script must be attached to xp orb object
//every enemy drops this