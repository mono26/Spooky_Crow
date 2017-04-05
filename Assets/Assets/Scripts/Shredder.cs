using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shredder : MonoBehaviour {
    void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Bullet"))
        {
            BulletsPool.Instance.ReleaseBullet(col.GetComponent<Rigidbody>());
        }
    }
}
