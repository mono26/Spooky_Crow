using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    [SerializeField]
    private Vector3 touchPosition = Vector3.zero;
    [SerializeField]
    private Vector3 targetPoint = Vector3.zero;
    [SerializeField]
    private Camera my_Camera;

    [SerializeField]
    private float shootTime;

    private float firingRate = 0.5f;
    private float bulletSpeed;
    void Start()
    {
        my_Camera = FindCamera();
        bulletSpeed = 8f;
    }
    void FixedUpdate()
    {
        shootTime += Time.deltaTime;
        if (Input.GetMouseButton(0) && shootTime >= firingRate)
        {
            touchPosition = Input.mousePosition;
            SummonRayCast();
        }
        else
            return;
    }
    Camera FindCamera()     //Para encontrar la camara
    {
        if (GetComponent<Camera>())
            return GetComponent<Camera>();
        else
            return Camera.main;
    }
    void SummonRayCast()
    {
        RaycastHit my_hit = new RaycastHit();
        if (Physics.Raycast(my_Camera.ScreenPointToRay(touchPosition), out my_hit, 100.0f))
        {
            targetPoint = my_hit.point;
            Shoot();    //Disparar al my_hit.point
            //Todos los posibles colliders a los cuales le puedo hacer touch
        }
    }

    void Shoot()      //Se le debe de pasar la informacion del hit point para que la bala sea dirigida al centro del objeto
    {
        shootTime = 0.0f;
        var direction = (targetPoint - transform.position).normalized;
        direction.y = 0;
        //Acceder al pool de las balas para darle la direccion al rigidbody
        Rigidbody bullet = BulletsPool.Instance.GetBullet();
        bullet.transform.position = transform.position;
        bullet.AddForce(direction * bullet.GetComponent<BulletController>().my_Info.speed, ForceMode.Impulse);
    }
}
