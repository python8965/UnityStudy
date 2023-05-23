using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class Study3Camera : MonoBehaviour
{
    // Start is called before the first frame update
    
    private Transform _transform;
    public Rigidbody target;
    void Start()
    {
        _transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        _transform.LookAt(target.transform);
    }
}
