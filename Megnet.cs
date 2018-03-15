using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Megnet : MonoBehaviour {

    float rotateSpeed = 45;
    // Use this for initialization
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Destroy(gameObject);
            PlayerController.instance.MegnetMove();
        }
    }
}
