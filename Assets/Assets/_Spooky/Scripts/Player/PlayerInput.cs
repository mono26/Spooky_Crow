using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float vertical;      //Valor para el input vertical dependiendo del acelerometro
    public float horizontal;      //Valor para el input horizontal dependiendo del acelerometro
    public float movSpeed;

    private PlayerMove my_PlayerMove;

    private void Awake()
    {
        my_PlayerMove = GetComponent<PlayerMove>();
    }
	// Use this for initialization
	void Start ()
    {
        vertical = 0.0f;
        horizontal = 0.0f;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {   
        vertical = Input.GetAxis("Horizontal") * movSpeed;
        horizontal = Input.GetAxis("Vertical") * movSpeed;
    }
}
