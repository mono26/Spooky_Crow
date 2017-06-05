using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PlayerMove : MonoBehaviour
{
    public Vector3 clickPoint;
    public Vector3 velocityVector;
    public bool isMoving;

    public float my_Speed = 1;     //Is going to be in m/s and is initialized 1
    public float turnSmooth = 1;

    public NavMeshAgent my_NavMeshAgent;
    public Animator my_Animator;
    public Transform my_Sprite;

    public float maxHorizontal;
    public float maxVertical;

    private Ray my_Ray;
    private RaycastHit my_RayHit;
    public LayerMask clickMoveLayer;

    void Awake()
    {
        my_NavMeshAgent = GetComponent<NavMeshAgent>();
        my_Animator = transform.GetComponent<Animator>();
    }
    void Start()
    {
        clickPoint = transform.position;
        my_NavMeshAgent.isStopped = false;
        my_NavMeshAgent.updateRotation = false;
        my_NavMeshAgent.speed = my_Speed;
        my_NavMeshAgent.acceleration = my_Speed;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && GameManager.Instance.selectPlayer)
        {
            ClickToMovePlayer();                         
        }
        Animate();

        if (Vector3.Distance(transform.position, clickPoint) < my_NavMeshAgent.stoppingDistance && isMoving)
        {
            Debug.Log(Vector3.Distance(transform.position, clickPoint));
            StopPlayer();
        }
    }
    void FixedUpdate()
    {
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
    void ClickToMovePlayer()
    {
        //Necesito mirar que el click no sea encima del jugador para que no se mueva y se deseleccione.
        //Primero seleccionar y luego mover.
        if (GameManager.Instance.selectPlayer)
        {
            my_Ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(my_Ray, out my_RayHit, 50.0f))
            {
                if (Vector3.Distance(transform.position, my_RayHit.point) > 5f)
                {
                    clickPoint = my_RayHit.point;
                    clickPoint.y = 0f;
                    GameManager.Instance.DeselectPlayer();
                }
                //Todos los posibles colliders a los cuales le puedo hacer touch
            }
            my_NavMeshAgent.ResetPath();
            my_NavMeshAgent.isStopped = false;
            isMoving = true;
            my_NavMeshAgent.SetDestination(clickPoint);
            my_Animator.SetBool("Walking", true);
        }
        else return;
    }
    public void StopPlayer()
    {
        my_Animator.SetBool("Walking", false);
        //my_NavMeshAgent.SetDestination(transform.position);
        //my_NavMeshAgent.isStopped = true;
        //isMoving = false;
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
