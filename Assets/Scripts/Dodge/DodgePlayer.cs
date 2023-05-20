using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgePlayer : MonoBehaviour
{
    
    private Rigidbody _rigidbody;

    
    public float Hp = 100.0f;
    public float BulletDamage = 20.0f;
    public float Speed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            _rigidbody.AddForce(Vector3.forward * Speed, ForceMode.Impulse);
        }
        if (Input.GetKey(KeyCode.A))
        {
            _rigidbody.AddForce(Vector3.left * Speed, ForceMode.Impulse);
        }
        if (Input.GetKey(KeyCode.S))
        {
            _rigidbody.AddForce(Vector3.back * Speed, ForceMode.Impulse);
        }
        if (Input.GetKey(KeyCode.D))
        {
            _rigidbody.AddForce(Vector3.right * Speed, ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            Hp -= BulletDamage;
            if (Hp <= 0.0f)
            {
                Debug.Log($"플레이어가 총알에 맞아 죽음! Score {Time.time}");
                Destroy(this.gameObject);
            }
            Debug.Log($"플레이어가 총알에 맞음! Hp : {Hp}");
            Destroy(other.gameObject);
        }
    }
}
