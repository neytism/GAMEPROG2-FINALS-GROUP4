using System;
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

    private float _distanceToPlayer;
    [SerializeField] private GameObject _lineRenderer;
    private float _laserLength;
    
    private void OnEnable()
    {
        _lineRenderer.SetActive(false);
    }

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
        ChangeLaserLength();
    }
    
    private void FixedUpdate()
    {
        AimInput();
    }
    
    public void Chase() //follow player 
    {
        Vector2 direction = _target.position - transform.position;
        direction.Normalize();
        _distanceToPlayer = Vector2.Distance(transform.position, _target.position);
        if (!_kb.isKnockBack)
        {
            
            if(_distanceToPlayer > stoppingRange)
            {
                // If enemy is far away, he moves closer.
                transform.position = Vector2.MoveTowards(transform.position, _target.position, speed * Time.deltaTime);
            }else if(_distanceToPlayer < stoppingRange && Vector2.Distance(transform.position, _target.position) > retreatRange)
            {
                // If enemy is close enough to the player, enemy stops.
                transform.position = this.transform.position;
            }else if (_distanceToPlayer < retreatRange)
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
            ShowLaser();
            btwShotInterval = startBtwShotInterval;  // adds interval between shots
        } else
        {
            btwShotInterval -= Time.deltaTime;
        }
        
    }

    private void ShowLaser()
    {
        _lineRenderer.SetActive(true);
        StartCoroutine(HideLaser());
    }
    
    IEnumerator HideLaser()
    {
        yield return new WaitForSeconds(.5f);
        GameObject bullet = ObjectPool.Instance.GetObject(projectile, enemyFirePoint.position);
        bullet.SetActive(true);
        bullet.GetComponent<Rigidbody2D>().AddForce(enemyFirePoint.right * enemyFireForce,ForceMode2D.Impulse);
        _lineRenderer.SetActive(false);
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

    private void ChangeLaserLength()
    {
        Vector3 endPos = _lineRenderer.transform.position + (_lineRenderer.transform.right * (_distanceToPlayer - 0.5f));
        _lineRenderer.GetComponent<LineRenderer>().SetPositions(new Vector3[] {_lineRenderer.transform.position,endPos});
    }
}
