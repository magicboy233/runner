using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetCollider : MonoBehaviour {

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Coin")
        {

            //Destroy(other.gameObject);
            //PlayerController.instance.QuickMove();
            StartCoroutine(HitCoin(other.gameObject));
        }
    }

    IEnumerator HitCoin(GameObject coin)
    {
        bool isLoop = true;
        while(isLoop)
        {
            coin.transform.position = Vector3.Lerp(coin.transform.position,
                    PlayerController.instance.transform.position, Time.deltaTime * 10);
            if(Vector3.Distance(coin.transform.position, PlayerController.instance.transform.position)<0.3f)
            {
                isLoop = false;
            }
            yield return null;
        }

    }
}
