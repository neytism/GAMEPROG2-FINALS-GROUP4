using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public static event Action shoot;
    public static event Action stopShoot;
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            shoot?.Invoke();
        }

        if (Input.GetButtonUp("Fire1"))
        {
            stopShoot?.Invoke();
        }
    }
}
