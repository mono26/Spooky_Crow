using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Physics : MonoBehaviour
{
    public Vector3 targetPosition;
    public Vector3 directionVector;
    public Vector2 velocityVector;
    public float speed = 1;     //Is going to be in m/s and is initialized 1
    public float bulletSpeed;
    public GameObject bulletPrefab;
    public float firingRate = 0.2f;
    public float firingTime = 0.00001f;

    private Rigidbody2D my_RigidBody;
    private Camera my_Camera;

    void Awake()
    {
        my_RigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");

        MovePlayer(vertical, horizontal);
        ClampPosition();
        //velocityVector = my_RigidBody.velocity;

        if (Input.GetMouseButtonDown(0))
        {
            InvokeRepeating("Fire", firingTime, firingRate);
        }

        if (Input.GetMouseButton(0))
        {
            my_Camera = FindCamera();

            RaycastHit hit;
            if (!Physics.Raycast(my_Camera.ScreenPointToRay(Input.mousePosition), out hit, 50.0f))
                return;

            targetPosition = hit.point;
            directionVector = targetPosition - transform.position;
            if (directionVector.magnitude > 1)
                directionVector = directionVector.normalized;
        }

        if (Input.GetMouseButtonUp(0))
        {
            CancelInvoke("Fire");
        }
    }

    void MovePlayer(float forward, float horizontal)       //Metodo usado para mover al jugador, giroscopio, test teclas
    {
        var forces = Vector2.zero;
        forces += Vector2.right * horizontal * speed;
        forces += Vector2.up * forward * speed;
        my_RigidBody.position += forces * Time.deltaTime;

    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawLine(transform.position, transform.position+new Vector3(1, 0, 0));
    }

    void ClampPosition()        //Method for clamping character inside moving plane
    {
        my_RigidBody.position = new Vector2
            (
                Mathf.Clamp(my_RigidBody.position.x, -7f, 7f),
                Mathf.Clamp(my_RigidBody.position.y, -5f, 5f)
            );
    }

    Camera FindCamera()     //Para encontrar la camara
    {
        if (GetComponent<Camera>())
            return GetComponent<Camera>();
        else
            return Camera.main;
    }

    void Fire()
    {
        //mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); //MONO
        //var localMousePosition = mousePosition; //MONO

        /*var direction = (localMousePosition - my_RigidBody.position).normalized;
        Debug.Log(direction);*/ //MONO

        GameObject bullet = Instantiate(bulletPrefab, my_RigidBody.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(directionVector.x, directionVector.y) * bulletSpeed, ForceMode2D.Impulse);
        //bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(mousePosition .x, mousePosition .y, 0);
        //bullet.GetComponent<Rigidbody2D>().position = new Vector3(mousePosition.x, mousePosition.y, 0);

    }
}
