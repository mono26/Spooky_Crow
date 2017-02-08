using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Vector3 targetPoint = Vector3.zero;
    public Vector2 velocityVector;
    public Vector3 touchPosition;

    public float speed = 1;     //Is going to be in m/s and is initialized 1
    public float rotationSpeed = 1;

    private Rigidbody2D my_RigidBody;
    private Camera my_Camera;

    void Awake()
    {
        my_RigidBody = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        my_Camera = FindCamera();
    }
    void FixedUpdate()
    {
        ClampPosition();
        //velocityVector = my_RigidBody.velocity;
        if (Input.GetMouseButton(0))
        {
            touchPosition = Input.mousePosition;
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
        RaycastHit my_hit = new RaycastHit();
        if (Physics.Raycast(my_Camera.ScreenPointToRay(touchPosition), out my_hit, 50.0f))
        {
            if (my_hit.collider.tag == "GameField")
            {
                targetPoint = my_hit.point;
                RotatePlayer();    //Disparar al my_hit.point
            }
            //Todos los posibles colliders a los cuales le puedo hacer touch
        }
    }
    void RotatePlayer()
    {
        var direccion = targetPoint - transform.position;
        direccion.z = 0.0f;

        var forwardVector = Vector3.RotateTowards(transform.right, direccion, rotationSpeed * Time.deltaTime, 0.0f);
        transform.right = forwardVector;
    }
    public void MovePlayer(float vertical, float horizontal)       //Metodo usado para mover al jugador, giroscopio, test teclas
    {
        var forces = Vector2.zero;
        forces += Vector2.right * horizontal * speed;
        forces += Vector2.up * vertical * speed;
        my_RigidBody.velocity += forces * Time.deltaTime;

    }
    void ClampPosition()        //Method for clamping character inside moving plane
    {
        my_RigidBody.position = new Vector2
            (
                Mathf.Clamp(my_RigidBody.position.x, -7f, 7f),
                Mathf.Clamp(my_RigidBody.position.y, -5f, 5f)
            );
    }
}
