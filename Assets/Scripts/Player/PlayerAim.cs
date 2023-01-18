using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    private Vector3 _mousePosition; 
    [SerializeField] private Transform aimTransform;
    private Vector3 aimDirection;
    private float aimAngle;

    void Update()
    {
        MousePosition();
    }
    
    private void FixedUpdate()
    {
       AimInput();
    }

    private void MousePosition()
    {
        _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void AimInput()
    {
        aimDirection = (_mousePosition - transform.position).normalized;
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
