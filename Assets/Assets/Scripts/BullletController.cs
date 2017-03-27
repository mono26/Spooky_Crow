using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullletController : MonoBehaviour
{
    //Esta classe contiende todo lo necesario para crear los diferentes tipos de balas
    public BulletInfo my_Info;
    public int index;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Enemy"))
        {
            //Se debe de hacer lo necesario: daño, return to pool, etc.
            PoolsManager.Instance.ReleaseObject(col.gameObject);
            BulletsPool.Instance.ReleaseBullet(col.GetComponent<Rigidbody>());          
        }
    }
}
