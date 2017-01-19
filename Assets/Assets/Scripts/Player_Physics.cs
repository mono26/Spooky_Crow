using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Physics : MonoBehaviour
{
    public Vector2 mousePosition;
    public Vector2 direction;
    public Vector2 velocityVector;
    public float speed = 1;     //Is going to be in m/s and is initialized 1
    public float bulletSpeed;
    public GameObject bulletPrefab;
    public float firingRate = 0.2f;

    private Rigidbody2D my_RigidBody;

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
        velocityVector = my_RigidBody.velocity;

        if (Input.GetMouseButton(0))
        {
            Fire();
            Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            // CastRay();      //Se necesita saber la posicion de el click
        }

        if (Input.GetMouseButtonUp(0))
        {
            CancelInvoke("Fire");
        }
    }

    void CastRay()
    {
        RaycastHit2D hit;
        Ray2D ray = new Ray2D(Input.mousePosition, Vector3.forward);
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

    void Fire()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var localMousePosition = mousePosition;

        var direction = (localMousePosition - my_RigidBody.position).normalized;
        Debug.Log(direction);

        GameObject bullet = Instantiate(bulletPrefab, my_RigidBody.position, transform.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(direction * bulletSpeed * Time.deltaTime);
        //bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(mousePosition .x, mousePosition .y, 0);
        //bullet.GetComponent<Rigidbody2D>().position = new Vector3(mousePosition.x, mousePosition.y, 0);

    }
}
