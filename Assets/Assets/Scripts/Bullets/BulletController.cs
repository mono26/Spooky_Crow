
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    //Esta classe contiende todo lo necesario para crear los diferentes tipos de balas
    public BulletInfo my_Info;
    public Rigidbody my_RigidBody;

    public GameObject my_Target;
    public Vector3 my_Point;        //Solo sera asignado por el jugador para que las balas que el disppare vayan al click.

    public bool player;
    public bool plant;

    public int index;
	// Use this for initialization
	void Awake ()
    {
        my_RigidBody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (!player && plant)
        {
            MoveTowardsTarget();
        }
        else if (player && !plant)
        {
            MoveTowardsPoint();
        }
        else
            BulletsPool.Instance.ReleaseBullet(my_RigidBody);
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Enemy"))
        {
            //Se debe de hacer lo necesario: daño, return to pool, etc.
            col.gameObject.SendMessage("TakeDamage", my_Info.damage);
            BulletsPool.Instance.ReleaseBullet(my_RigidBody);          
        }
        if (col.CompareTag("Shreder"))
        {
            //Se debe de hacer lo necesario: daño, return to pool, etc.
            BulletsPool.Instance.ReleaseBullet(my_RigidBody);
        }
    }
    void MoveTowardsPoint()
    {
        var dist = transform.position - my_Point;
        if (dist.sqrMagnitude < 0.15)
        {
            BulletsPool.Instance.ReleaseBullet(my_RigidBody);
        }
        else
        {
            my_RigidBody.position = Vector3.MoveTowards(transform.position, my_Point, my_Info.speed * Time.fixedDeltaTime);
        }
    }
    void MoveTowardsTarget()        //Este metodo sera usado cuando la bala sea disparada desde una torre
    {
        if (my_Target && my_Target.activeInHierarchy)
        {
            my_RigidBody.position = Vector3.MoveTowards(transform.position, my_Target.transform.position, my_Info.speed * Time.fixedDeltaTime);
        }
        else if(my_Target && !my_Target.activeInHierarchy)
        {
            BulletsPool.Instance.ReleaseBullet(my_RigidBody);
        }
    }
}
