using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {
    float rotateSpeed = 180;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(0, Time.deltaTime * rotateSpeed, 0);
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") 
        {
            GameScore.instance.coin++;
            Destroy(gameObject);
        }
    }
}
