using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Physics : MonoBehaviour
{
    public Vector3 mousePosition;

    private Rigidbody2D my_RigidBody;

    void Awake()
    {
        my_RigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CastRay();      //Se necesita saber la posicion de el click
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
        forces += Vector2.right * forward;
        forces += Vector2.up * horizontal;
        my_RigidBody.AddForce(forces * Time.deltaTime);
    }
}
