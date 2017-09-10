using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using CnControls ;

public class PlayerMove : MonoBehaviour
{
    public bool isMoving;

    public float velocidadMovimiento = 1;     //Is going to be in m/s and is initialized 1
    public float turnSmooth = 1;
    public Vector3 lastPosition;

    //Componenetes de unity
    public Animator playerAnimator;
    public Transform playerSprite;
    public Rigidbody playerRigidBody;

    //Para clampear el valor maximo que se puede mover
    public float maxHorizontal;
    public float maxVertical;

    void Awake()
    {
        playerRigidBody = GetComponent<Rigidbody>();
        playerAnimator = transform.GetComponent<Animator>();
    }
    void Start()
    {

    }
    private void Update()
    {

    }
    void FixedUpdate()
    {
		float horizontal = CnInputManager.GetAxis ("Movement Horizontal");
		float vertical = CnInputManager.GetAxis ("Movement Vertical");

		if (horizontal != 0 || vertical != 0)
			MovePosition (new Vector3 (horizontal , 0, vertical));
        else isMoving = false;
        ClampPosition();
        Animate();
        lastPosition = transform.position;
    }
    public void Animate()
    {
        if (!isMoving)
        {
            return;
        }
        if (transform.position.x <= lastPosition.x)
        {
            playerSprite.localScale = new Vector3(-1, 1, 1);
        }
        else if (transform.position.x > lastPosition.x)
        {
            playerSprite.localScale = new Vector3(1, 1, 1);
        }
    }
    public void MovePosition(Vector3 direction)
    {
        isMoving = true;
        playerRigidBody.position += direction * velocidadMovimiento * Time.fixedDeltaTime;
    }
    void ClampPosition()        //Method for clamping character inside moving plane
    {
        transform.position = new Vector3
            (
                Mathf.Clamp(transform.position.x, -maxHorizontal, maxHorizontal),
                transform.position.y,
                Mathf.Clamp(transform.position.z, -maxVertical, maxVertical)
            );
    }
}
