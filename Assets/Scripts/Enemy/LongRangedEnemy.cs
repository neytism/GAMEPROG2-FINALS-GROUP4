using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongRangedEnemy : MonoBehaviour
{
    public float speed;
    public float stoppingRange;
    public float retreatRange;

    private float btwShotInterval;
    public float startBtwShotInterval;

    public GameObject projectile;
    private Transform _target;
    private EnemyKnockBack _kb;
    
    [SerializeField] private Transform aimTransform;
    private Vector3 aimDirection;
    private float aimAngle;
    public Transform enemyFirePoint;
    public float enemyFireForce;

    void Start()
    {
        _target = FindObjectOfType<PlayerMovement>().GetComponent<Transform>();
        _kb = GetComponent<EnemyKnockBack>();
        btwShotInterval = startBtwShotInterval;
    }

    void Update()
    {
        Chase();
        Fire();
    }
    
    private void FixedUpdate()
    {
        AimInput();
    }
    
    public void Chase() //follow player 
    {
        Vector2 direction = _target.position - transform.position;
        direction.Normalize();
        
        if (!_kb.isKnockBack)
        {
            
            if(Vector2.Distance(transform.position, _target.position) > stoppingRange)
            {
                // If enemy is far away, he moves closer.
                transform.position = Vector2.MoveTowards(transform.position, _target.position, speed * Time.deltaTime);
            }else if(Vector2.Distance(transform.position, _target.position) < stoppingRange && Vector2.Distance(transform.position, _target.position) > retreatRange)
            {
                // If enemy is close enough to the player, enemy stops.
                transform.position = this.transform.position;
            }else if (Vector2.Distance(transform.position, _target.position) < retreatRange)
            {
                // If player gets closen the gap of the retreat range, the enemy move farther away.
                transform.position = Vector2.MoveTowards(transform.position, _target.position, -speed * Time.deltaTime);
            }
        }
        
    }

    private void Fire()
    {
        
        if(btwShotInterval <= 0)
        {
            GameObject bullet = ObjectPool.Instance.GetObject(projectile, enemyFirePoint.position);
            bullet.SetActive(true);
            bullet.GetComponent<Rigidbody2D>().AddForce(enemyFirePoint.right * enemyFireForce,ForceMode2D.Impulse);
            btwShotInterval = startBtwShotInterval;  // adds interval between shots
        } else
        {
            btwShotInterval -= Time.deltaTime;
        }
        
    }
    
    //aim control
    private void AimInput()
    {
        aimDirection = (_target.position - transform.position).normalized;
        aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;  //faces towards target
        aimTransform.eulerAngles = new Vector3(0, 0, aimAngle);
        
        FlipWeapon();
    }

    private void FlipWeapon()   
    {
        //flip weapon image if aiming to left or right
        Vector3 aimLocalScale = Vector3.one;
        if (aimAngle > 90 || aimAngle < -90)
        {
            aimLocalScale.y = -1f;
        }
        else
        {
            aimLocalScale.y = +1f;
        }
        aimTransform.localScale = aimLocalScale;
    }
}
