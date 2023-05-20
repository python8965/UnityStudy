using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeGame : MonoBehaviour
{
    public GameObject Bullet;
    
    public int Duration = 10;

    private float Level = 1;

    private float count = 0;
    // Start is called before the first frame update
    

    private void FixedUpdate()
    {
        Level = (float)Math.Log(Time.time) + 1;
        count += Level;
        if (count > Duration)
        {
            var a= Instantiate(Bullet);
            a.gameObject.AddComponent<DodgeBullet>();
            a.gameObject.tag = "Bullet";
            count = 0;
        }
        
    }
}
