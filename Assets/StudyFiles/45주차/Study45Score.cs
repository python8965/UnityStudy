using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Study45Score : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {

            var audioSource = FindAnyObjectByType<AudioSource>();
            var clip = Resources.Load<AudioClip>("pop.mp3");


            audioSource.clip = clip;
            audioSource.Play();

            Destroy(this.gameObject);
        }
    }
}
