using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public GameObject Player;
    public float Height;
    public float Distance;
    Vector3 pos;
	// Use this for initialization
	void Start () {
        pos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
   
	}
    void LateUpdate()
    {
        //transform.position = new Vector3(Player.transform.position.x,
        //    Player.transform.position.y + Height,
        //    Player.transform.position.z - Distance);
        pos.x = Player.transform.position.x;
        pos.y = Mathf.Lerp(pos.y, Player.transform.position.y + Height, Time.deltaTime);
        pos.z = Player.transform.position.z - Distance;
        transform.position = pos;
    }
}
