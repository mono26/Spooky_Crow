using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public Vector3 targetPoint = Vector3.zero;
    public Vector3 velocityVector;

    public float speed = 1;     //Is going to be in m/s and is initialized 1
    public float rotationSpeed = 1;

    private Rigidbody my_RigidBody;
    private Camera my_Camera;
    private EnemyInfo my_Info;

    void Awake()
    {
        my_RigidBody = GetComponent<Rigidbody>();
    }
    void Start()
    {
        FindPlayer();
    }
    void CheckObjective()
    {
        var type = my_Info.my_Type;
        switch(type)
        {
            case (EnemyInfo.EnemyType.ATACKER):
                {
                    FindPlayer();
                    break;
                }
            case (EnemyInfo.EnemyType.STEALER):
                {
                    FindHouse();
                    break;
                }
        }
    }
    void FixedUpdate()
    {
        FindPlayer();
        //Calculamos la direccion enttre el enemigo y el jugador para pasar componentes al Move()
        var direccion = (targetPoint - transform.position).normalized;
        MoveEnemy(direccion.z, direccion.x);
        ClampPosition();
        velocityVector = my_RigidBody.velocity;
    }
    void RotateEnemy()
    {
        var direccion = targetPoint - transform.position;
        direccion.y = 0.0f;

        var forwardVector = Vector3.RotateTowards(transform.forward, direccion, rotationSpeed * Time.deltaTime, 0.0f);
        transform.forward = forwardVector;
    }
    public void MoveEnemy(float z, float x)       //Metodo usado para mover al jugador, giroscopio, test teclas
    {
        var forces = Vector3.zero;
        forces += Vector3.forward * z * speed;
        forces += Vector3.right * x * speed;
        my_RigidBody.velocity += forces * Time.deltaTime;

    }
    void ClampPosition()        //Method for clamping character inside moving plane
    {
        my_RigidBody.position = new Vector3(
                Mathf.Clamp(my_RigidBody.position.x, -7f, 7f),
                my_RigidBody.position.y,
                Mathf.Clamp(my_RigidBody.position.z,-7.0f,7.0f)
            );
    }
    void FindPlayer()
    {
        var player = GameObject.FindGameObjectWithTag("Crow");
        targetPoint = player.transform.position;
    }
    void FindHouse()
    {
        var house = GameObject.FindGameObjectWithTag("House");
        targetPoint = house.transform.position;
    }
}
