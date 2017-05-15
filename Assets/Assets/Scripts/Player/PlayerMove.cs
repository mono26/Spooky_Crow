using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMove : MonoBehaviour
{
    public Vector3 clickPoint;
    public Vector3 velocityVector;

    public float speed = 1;     //Is going to be in m/s and is initialized 1
    public float turnSmooth = 1;

    private Rigidbody my_RigidBody;

    private Ray my_Ray;
    private RaycastHit my_RayHit;
    public LayerMask clickMoveLayer;

    void Awake()
    {
        my_RigidBody = GetComponent<Rigidbody>();
    }
    void Start()
    {

    }
    private void Update()
    {
        Debug.DrawRay(transform.position, transform.forward.normalized * 1.5f, Color.blue);
        if (GameManager.Instance.selectPlayer)
        {
            if (Input.GetMouseButtonDown(0))
                ClickToMove();
        }
    }
    void FixedUpdate()
    {
        ClampPosition();
        //velocityVector = my_RigidBody.velocity;
        MoveTowardsPoint();
    }
    void SummonRayCast()
    {
        my_Ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(my_Ray, out my_RayHit, 50.0f))
        {
            clickPoint = my_RayHit.point;
            clickPoint.y = 0;
            //Todos los posibles colliders a los cuales le puedo hacer touch
        }
        RotatePlayer();    //Disparar al my_hit.point
    }
    void ClickToMove()
    {
        //Necesito mirar que el click no sea encima del jugador para que no se mueva y se deseleccione.
        //Primero seleccionar y luego mover.
        if(!GameManager.Instance.selectPlayer)
        {
            return;
        }
        my_Ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(my_Ray, out my_RayHit, 50.0f, clickMoveLayer))
        {
            if(Vector3.Distance(transform.position, my_RayHit.point) > 0.5)
            {
                clickPoint = my_RayHit.point;
                clickPoint.y = 0f;
                GameManager.Instance.DeselectPlayer();
            }
            //Todos los posibles colliders a los cuales le puedo hacer touch
        }
    }
    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        GameManager.Instance.SelectPlayer();
    }
    void RotatePlayer()
    {
        var direccion = clickPoint - transform.position;
        direccion.y = 0.0f;
        Quaternion lookRotation = Quaternion.LookRotation(direccion);

        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, turnSmooth);
    }
    void MoveTowardsPoint()
    {
        var dist = transform.position - clickPoint;
        if (dist.sqrMagnitude < 0.1)
        {
            return;
        }
        else
        {
            my_RigidBody.position = Vector3.MoveTowards(transform.position, clickPoint, speed * Time.fixedDeltaTime);
        }
    }
    public void MovePlayer(float vertical, float horizontal)       //Metodo usado para mover al jugador, giroscopio, test teclas
    {
        var forces = Vector3.zero;
        forces += Vector3.forward * horizontal * speed;
        forces += Vector3.right * vertical * speed;
        my_RigidBody.position += forces * Time.deltaTime;

    }
    void ClampPosition()        //Method for clamping character inside moving plane
    {
        my_RigidBody.position = new Vector3
            (
                Mathf.Clamp(my_RigidBody.position.x, -25f, 25f),
                my_RigidBody.position.y,
                Mathf.Clamp(my_RigidBody.position.z, -25.0f, 25.0f)
            );
    }
}
