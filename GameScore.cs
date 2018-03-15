using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScore : MonoBehaviour {

    public int coin;
    public static GameScore instance;
	// Use this for initialization
	void Start () {
        coin = 0;
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
