using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerFire : MonoBehaviour
{
    [SerializeField]
    private Vector3 clickPoint;
    [SerializeField]
    private Camera my_Camera;

    [SerializeField]
    private float shootTime;

    private float firingRate = 0.5f;
    private float bulletSpeed;
    private const float sampleDistanceToPointer = 4f;
    private RaycastHit my_RayHit;

    void Start()
    {
        my_Camera = FindCamera();
        bulletSpeed = 8f;
    }
    void Update()
    {
        shootTime += Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && shootTime >= firingRate)
        {
            ClickToFire();
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
    void ClickToFire()
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out my_RayHit, 50.0f, 12))
        {
            clickPoint = my_RayHit.point;
            clickPoint.y = 0;
            //Todos los posibles colliders a los cuales le puedo hacer touch
        }
        Shoot();    //Disparar al my_hit.point
    }

    void Shoot()      //Se le debe de pasar la informacion del hit point para que la bala sea dirigida al centro del objeto
    {
        shootTime = 0.0f;
        var direction = clickPoint - transform.position;
        direction.y = 0;
        direction = direction.normalized;
        Debug.Log(direction);
        //Acceder al pool de las balas para darle la direccion al rigidbody
        Rigidbody bullet = BulletsPool.Instance.GetBullet();
        bullet.GetComponent<BulletController>().my_Parent = this.gameObject;      //Se le da el parent a la bala dependiento del objeto que la dispare
        bullet.GetComponent<BulletController>().my_Point = clickPoint;        //Se le da el mismo target que el parent
        bullet.position = transform.position;
        bullet.rotation = transform.rotation;
    }
}
