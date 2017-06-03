using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerFire : MonoBehaviour
{
    [SerializeField]
    private Vector3 clickPoint;
    [SerializeField]
    private PlayerMove my_Player;

    [SerializeField]
    private float shootTime;

    private float firingRate = 0.5f;

    private Ray my_Ray;
    private RaycastHit my_RayHit;

    void Awake()
    {
        my_Player = GetComponent<PlayerMove>();
    }
    void Update()
    {
        shootTime += Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && shootTime >= firingRate)
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            if (!my_Player.isMoving)
                ClickToFire();
            else
                my_Player.StopPlayer();
        }
        else
            return;
    }
    void ClickToFire()
    {
        my_Ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(my_Ray, out my_RayHit, 50.0f))
        {
            clickPoint = my_RayHit.point;
            clickPoint.y = 0f;
            //Todos los posibles colliders a los cuales le puedo hacer touch
        }
        if(Vector3.Distance(transform.position, my_RayHit.point) > 5f)
        {
            Shoot();    //Disparar al my_hit.point
        }
    }
    void Shoot()      //Se le debe de pasar la informacion del hit point para que la bala sea dirigida al centro del objeto
    {
        shootTime = 0.0f;
        Rigidbody bullet = BulletsPool.Instance.GetBullet();
        //Si el bool de plant es falso y el player veradero es que la bala fue disparada por un jugador o enemigo.
        bullet.GetComponent<BulletController>().plant = false;       //Para que el bulletcontroller sepa como moverse
        bullet.GetComponent<BulletController>().player = true;
        bullet.GetComponent<BulletController>().my_Point = clickPoint;
        bullet.transform.position = this.transform.position;
        bullet.transform.rotation = this.transform.rotation;
    }
}
