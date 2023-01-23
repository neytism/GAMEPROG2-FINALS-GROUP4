using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyAI : MonoBehaviour
{
    [SerializeField] private float _speed = 1;
    private Transform target;
    private EnemyKnockBack _kb;

    void OnEnable()
    {
        target = FindObjectOfType<PlayerMovement>().GetComponent<Transform>();
        _kb = GetComponent<EnemyKnockBack>();
    }

    private void Update()
    {
        Chase();
        ClampVelocity();
    }
    
    public void Chase() //follow player 
    {
        Vector2 direction = target.position - transform.position;
        direction.Normalize();
        
        if (!_kb.isKnockBack)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);
        }
        
    }
    
    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag.Equals("Enemy") || col.gameObject.tag.Equals("Player") || col.gameObject.tag.Equals("EnemyRanged") || col.gameObject.tag.Equals("EnemyLongRanged")) 
        {
            Chase();
        }
    }
    
    void ClampVelocity()
    {
        float maxVelocity = 5f;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Vector2 velocity = rb.velocity;
        velocity.x = Mathf.Clamp(velocity.x, -maxVelocity, maxVelocity);
        velocity.y = Mathf.Clamp(velocity.y, -maxVelocity, maxVelocity);
        rb.velocity = velocity;
    }
}
