using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Vector3 clickPoint = Vector3.zero;
    public Vector3 velocityVector;

    public float speed = 1;     //Is going to be in m/s and is initialized 1
    public float turnSmooth = 1;

    private Rigidbody my_RigidBody;
    private Camera my_Camera;

    void Awake()
    {
        my_RigidBody = GetComponent<Rigidbody>();
    }
    void Start()
    {
        my_Camera = FindCamera();
    }
    private void Update()
    {
        Debug.DrawRay(transform.position, transform.forward.normalized * 1.5f, Color.blue);
    }
    void FixedUpdate()
    {
        ClampPosition();
        //velocityVector = my_RigidBody.velocity;
        if (Input.GetMouseButton(0))
        {
            SummonRayCast();
        }
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
        RaycastHit my_RayHit = new RaycastHit();
        if (Physics.Raycast(my_Camera.ScreenPointToRay(Input.mousePosition), out my_RayHit, 50.0f, 12))
        {
            clickPoint = my_RayHit.point;
            clickPoint.y = 0;
            //Todos los posibles colliders a los cuales le puedo hacer touch
        }
        RotatePlayer();    //Disparar al my_hit.point
    }
    void RotatePlayer()
    {
        var direccion = clickPoint - transform.position;
        direccion.y = 0.0f;
        Quaternion lookRotation = Quaternion.LookRotation(direccion);

        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, turnSmooth);
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
