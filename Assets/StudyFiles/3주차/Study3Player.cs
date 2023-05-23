using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Study3Player : MonoBehaviour
{
    private Rigidbody _rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        var move = Input.inputString switch
        {
            "w" or "W" => Vector3.forward,
            "a" or "A" => Vector3.left,
            "s" or "S" => Vector3.back,
            "d" or "D" => Vector3.right,
            _ => Vector3.zero
        };
        
        _rigidbody.AddTorque(move * Time.deltaTime * 60, ForceMode.Impulse);
    }
}
