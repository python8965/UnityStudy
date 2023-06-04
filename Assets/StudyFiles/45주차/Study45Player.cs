using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Study45Player : MonoBehaviour
{
    private Rigidbody _rigidbody;

    public string scenes = "last";

    private bool colliding = false;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        
        if (GameObject.FindWithTag("Score") == null)
        {
            if (scenes == "last")
            {
                FindObjectOfType<Text>().text = "Clear";
            }
            else
            {
                SceneManager.LoadScene(scenes);
            }
        }
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

        Debug.Log(colliding);

        if (Input.GetKey(KeyCode.Space) && colliding)
        {
            _rigidbody.AddForce(Vector3.up * Time.deltaTime * 200, ForceMode.Impulse);
        }

        move.Normalize();

        _rigidbody.AddForce(move * Time.deltaTime * 10, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        colliding = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        colliding = false;
    }
}
