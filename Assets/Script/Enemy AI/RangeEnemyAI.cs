using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemyAI : MonoBehaviour
{
    public float speed;
    public float stoppingRange;
    public float retreatRange;

    private float btwShotInterval;
    public float startBtwShotInterval;

    public GameObject projectile;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        btwShotInterval = startBtwShotInterval;
    }

    void Update()
    {
        // If enemy is far away, he moves closer.
        if(Vector2.Distance(transform.position, player.position) > stoppingRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        // If enemy is close enough to the player, enemy stops.
        } else if(Vector2.Distance(transform.position, player.position) < stoppingRange && Vector2.Distance(transform.position, player.position) > retreatRange)
        {
            transform.position = this.transform.position;
         // If player gets closen the gap of the retreat range, the enemy move farther away.
        }else if (Vector2.Distance(transform.position, player.position) < retreatRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
        }

        if(btwShotInterval <= 0)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            btwShotInterval = startBtwShotInterval;
        } else
        {
            btwShotInterval -= Time.deltaTime;
        }

    }

}
