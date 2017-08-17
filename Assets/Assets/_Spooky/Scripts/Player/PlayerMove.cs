using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using CnControls ;

public class PlayerMove : MonoBehaviour
{
    public Vector3 clickPoint;
    public bool isMoving;

    public float velocidadMovimiento = 1;     //Is going to be in m/s and is initialized 1
    public float turnSmooth = 1;

    //Componenetes de unity
    public NavMeshAgent my_NavMeshAgent;
    public Animator my_Animator;
    public Transform my_Sprite;
    public Rigidbody my_RigidBody;

    //Para clampear el valor maximo que se puede mover
    public float maxHorizontal;
    public float maxVertical;

    //Informacion para el raycast
    private Ray my_Ray;
    private RaycastHit my_RayHit;
    public LayerMask clickMoveLayer;

    void Awake()
    {
        my_NavMeshAgent = GetComponent<NavMeshAgent>();
        my_RigidBody = GetComponent<Rigidbody>();
        my_Animator = transform.GetComponent<Animator>();
    }
    void Start()
    {
        my_NavMeshAgent.isStopped = false;
        my_NavMeshAgent.updateRotation = false;
        my_NavMeshAgent.speed = velocidadMovimiento;
        my_NavMeshAgent.acceleration = velocidadMovimiento;
    }
    private void Update()
    {
        /*
        if (Input.GetMouseButtonDown(0))
        {
            ClickToMovePlayer();                         
        }
        if (Vector3.Distance(transform.position, my_NavMeshAgent.destination) < my_NavMeshAgent.stoppingDistance && isMoving == true)
        {
            my_Animator.SetBool("Walking", false);
            isMoving = false;
        }*/
        Animate();
    }
    void FixedUpdate()
    {
		float horizontal = CnInputManager.GetAxis ("Movement Horizontal");
		float vertical = CnInputManager.GetAxis ("Movement Vertical");

		if (horizontal != 0 || vertical != 0) {
			MovePosition (new Vector3 (horizontal , 0, vertical));

		}


        ClampPosition();
    }
    public void Animate()
    {
        if (!isMoving)
        {
            return;
        }
        if (transform.position.x <= clickPoint.x)
        {
            my_Sprite.localScale = new Vector3(1, 1, 1);
        }
        else if (transform.position.x > clickPoint.x)
        {
            my_Sprite.localScale = new Vector3(-1, 1, 1);
        }
    }
   /* void ClickToMovePlayer()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        //Necesito mirar que el click no sea encima del jugador para que no se mueva y se deseleccione.
        my_Ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(my_Ray, out my_RayHit, clickMoveLayer))
        {
            if (Vector3.SqrMagnitude(my_RayHit.point - transform.position) > 5f * 5f)
            {
                clickPoint = my_RayHit.point;
                clickPoint.y = 0f;
            }
            //Todos los posibles colliders a los cuales le puedo hacer touch
        }
        my_NavMeshAgent.ResetPath();
        my_NavMeshAgent.isStopped = false;
        isMoving = true;
        my_NavMeshAgent.SetDestination(new Vector3(clickPoint.x, transform.position.y, clickPoint.z));
        my_Animator.SetBool("Walking", true);
    }*/
    public void MovePosition(Vector3 direction)
    {
        my_RigidBody.position += direction * velocidadMovimiento * Time.fixedDeltaTime;
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
