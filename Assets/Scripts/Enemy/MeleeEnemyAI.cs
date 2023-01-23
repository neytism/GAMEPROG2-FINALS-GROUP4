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
            if (col.gameObject.tag.Equals("Player"))
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }
            Chase();
            
        }
    }
    
}
