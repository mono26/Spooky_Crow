using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : AIController
{
    public int daño;
    //public float speed;
   // public Vector3 movingDirection;
    public float deadTimer;

    public Info bulletInfo;

   // public Rigidbody my_RigidBody;

    private void Awake()
    {
       // my_RigidBody = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        deadTimer -= Time.deltaTime;
        if(deadTimer < 0)
        {
            PoolsManagerBullets.Instance.ReleaseBullet(gameObject);
       }
    }
    public void SetTimer()
    {

        deadTimer = 10f;    // este es el valor que asigne en el editor
 
    }
  /*  public void AddForce()
    {
        my_RigidBody.AddForce(movingDirection * speed,ForceMode.Impulse);
    }*/
}
