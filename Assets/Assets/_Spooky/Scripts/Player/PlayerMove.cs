using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PlayerMove : MonoBehaviour
{
    public Vector3 clickPoint;
    public Vector3 velocityVector;
<<<<<<< HEAD:Assets/Assets/_Spooky/Scripts/Player/PlayerMove.cs
    public bool isMoving;
=======
    public bool moving;
>>>>>>> origin/master:Assets/Assets/_Spooky/Scripts/Player/PlayerMove.cs

    public float my_Speed = 1;     //Is going to be in m/s and is initialized 1
    public float turnSmooth = 1;

    private Rigidbody my_RigidBody;
    public NavMeshAgent my_NavMeshAgent;
    public Animator my_Animator;
    public Transform my_Sprite;
    public float stopingDistanceProportion;

    public float maxHorizontal;
    public float maxVertical;

    private Ray my_Ray;
    private RaycastHit my_RayHit;
    public LayerMask clickMoveLayer;

    void Awake()
    {
        my_RigidBody = GetComponent<Rigidbody>();
        my_NavMeshAgent = GetComponent<NavMeshAgent>();
        my_Animator = transform.GetComponent<Animator>();
    }
    void Start()
    {
        clickPoint = transform.position;
        my_NavMeshAgent.isStopped = false;
        my_NavMeshAgent.updateRotation = false;
        my_NavMeshAgent.speed = my_Speed;
    }
    private void Update()
    {
        if (my_NavMeshAgent.remainingDistance < my_NavMeshAgent.stoppingDistance * stopingDistanceProportion)
        {
            StopPlayer();
        }
        Debug.DrawRay(transform.position, transform.forward.normalized * 1.5f, Color.blue);
        if (GameManager.Instance.selectPlayer)
        {
            if (Input.GetMouseButtonDown(0))
            {
                ClickToMovePlayer();
                my_Animator.SetBool("Walking", true);
                my_NavMeshAgent.SetDestination(clickPoint);
                my_NavMeshAgent.isStopped = false;
<<<<<<< HEAD:Assets/Assets/_Spooky/Scripts/Player/PlayerMove.cs
                isMoving = true;
=======
                moving = true;
>>>>>>> origin/master:Assets/Assets/_Spooky/Scripts/Player/PlayerMove.cs
            }                            
        }
        Animate();
    }
    void FixedUpdate()
    {
        ClampPosition();
        //velocityVector = my_RigidBody.velocity;
    }
    public void Animate()
    {
<<<<<<< HEAD:Assets/Assets/_Spooky/Scripts/Player/PlayerMove.cs
        if (!isMoving)
=======
        if (!moving)
>>>>>>> origin/master:Assets/Assets/_Spooky/Scripts/Player/PlayerMove.cs
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
    void ClickToMovePlayer()
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
            if(Vector3.Distance(transform.position, my_RayHit.point) > 5f)
            {
                clickPoint = my_RayHit.point;
                clickPoint.y = 0f;
                GameManager.Instance.DeselectPlayer();
            }
            //Todos los posibles colliders a los cuales le puedo hacer touch
        }
    }
<<<<<<< HEAD:Assets/Assets/_Spooky/Scripts/Player/PlayerMove.cs
    public void StopPlayer()
    {
        my_Animator.SetBool("Walking", false);
        my_NavMeshAgent.SetDestination(transform.position);
        my_NavMeshAgent.isStopped = true;
        isMoving = false;
=======
    void StopPlayer()
    {
        my_Animator.SetBool("Walking", false);
        my_NavMeshAgent.isStopped = true;
        moving = false;
>>>>>>> origin/master:Assets/Assets/_Spooky/Scripts/Player/PlayerMove.cs
    }
    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        Debug.Log("Le di click al player");
        GameManager.Instance.SelectPlayer();
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
        forces += Vector3.forward * horizontal * my_Speed;
        forces += Vector3.right * vertical * my_Speed;
        my_RigidBody.position += forces * Time.deltaTime;

    }
    void ClampPosition()        //Method for clamping character inside moving plane
    {
        my_RigidBody.position = new Vector3
            (
                Mathf.Clamp(my_RigidBody.position.x, -maxHorizontal, maxHorizontal),
                my_RigidBody.position.y,
                Mathf.Clamp(my_RigidBody.position.z, -maxVertical, maxVertical)
            );
    }
}
