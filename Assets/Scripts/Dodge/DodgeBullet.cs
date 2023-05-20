using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using Random = UnityEngine.Random;

public class DodgeBullet : MonoBehaviour
{
    // Start is called before the first frame update
    
    private Rigidbody _rigidbody;

    private readonly float MapSizeX = 10.0f;
    private readonly float MapSizeZ = 10.0f;

    private readonly float MapMargin = 1.0f;
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        var (x,y) = Random.Range(0, 3) switch
        {
            0 => (MapSizeX, Random.Range(-MapSizeZ, MapSizeZ)),
            1 => (-MapSizeX, Random.Range(-MapSizeZ, MapSizeZ)),
            2 => (Random.Range(-MapSizeX, MapSizeX), MapSizeZ),
            3 => (Random.Range(-MapSizeX, MapSizeX), -MapSizeZ),
            _ => (0, 0),
        };

        Vector2 pos = new(x, y);
        
        transform.position = new Vector3(x, 1, y);
        Vector2 target = new( -x, -y);
        
        Vector2 direction = target - pos;
        Vector2 direction_norm = direction.normalized * 10;
        
        _rigidbody.AddForce(new Vector3(direction_norm.x , 0, direction_norm.y ), ForceMode.VelocityChange);
        
    }

    public void Update()
    {
        if (transform.position.x < -MapSizeX - MapMargin * 2 || transform.position.x > MapSizeX + MapMargin * 2 ||
            transform.position.z < -MapSizeZ - MapMargin * 2 || transform.position.z > MapSizeZ + MapMargin * 2)
        {
            Destroy(gameObject);
        }
    }
}
