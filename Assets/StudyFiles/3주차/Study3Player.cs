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
        var move = new Vector3();

        if (Input.GetKey(KeyCode.W))
        {
            move += Vector3.forward;
        }

        if (Input.GetKey(KeyCode.S))
        {
            move += Vector3.back;
        }

        if (Input.GetKey(KeyCode.A))
        {
            move += Vector3.left;
        }

        if (Input.GetKey(KeyCode.D))
        {
            move += Vector3.right;
        }

        move.Normalize();

        _rigidbody.AddForce(move * Time.deltaTime * 10, ForceMode.Impulse);
    }
}
