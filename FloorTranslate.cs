using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTranslate : MonoBehaviour {
    public GameObject FloorOn;
    public GameObject FloorNext;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.position.z>FloorOn.transform.position.z+32)
        {
            FloorOn.transform.position = new Vector3(0, 0, FloorNext.transform.position.z + 32);
            GameObject temp = FloorOn;
            FloorOn = FloorNext;
            FloorNext = temp;
        }
	}
}
