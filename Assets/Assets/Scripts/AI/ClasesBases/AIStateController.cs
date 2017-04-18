using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AI;

[RequireComponent (typeof(Rigidbody), typeof(NavMeshAgent), typeof(AIAtack))]
public class AIStateController : MonoBehaviour
{
    public EnemyInfo enemyInfo;

    public AIState currentState;
    public AIState remainState;    //Estado de hacer nada, para que siempre el estado a cambiar sea diferente a este.
    //Catching
    public AIAtack aiAtack;
    public AIAnimations aiAnimations;
    public Rigidbody my_RigidBody;     //Referecia al rigidBody del objeto
    public NavMeshAgent my_NavMeshAgent;
    public Transform my_Target;        //Referencia al target del objeto, player o house se le puede preguntar al game manager por medio del singleton.

    public float updateRate = 2f;
    public float stopingDistanceProportion;

    public bool canMove = true;
    public bool aiActive = true;

    public int health;
    public float turnSmooth;    //Valor para el smooth cuando se rota el objeto por medio de los quaternions

    void Awake()
    {
        my_RigidBody = GetComponent<Rigidbody>();
        aiAtack = GetComponent<AIAtack>();
        my_NavMeshAgent = GetComponent<NavMeshAgent>();

        health = enemyInfo.health;
    }
	// Use this for initialization
	void OnEnable ()
    {
		if(!my_Target)
        {
            Debug.Log("There is no target for me" + this.name);
            //Execute SearchTargetCode;
            switch (enemyInfo.my_Type)
            {
                case (EnemyInfo.EnemyType.STEALER):
                    {
                        my_Target = GameManager.Instance.house.transform;
                        break;
                    }
                case (EnemyInfo.EnemyType.ATACKER):
                    {
                        my_Target = GameManager.Instance.player.transform;
                        break;
                    }
                case (EnemyInfo.EnemyType.BOSS):
                    {
                        //Assigna el target al inicio dependiendo del tipo
                        my_Target = GameManager.Instance.player.transform;
                        break;
                    }
            }
        }
        my_NavMeshAgent.SetDestination(my_Target.position);
        my_NavMeshAgent.isStopped = false;
        StartCoroutine(UpdateDestination());
    }
	
	// Update is called once per frame
	void Update ()
    {
        Debug.DrawRay(transform.position, transform.forward.normalized * 1.5f, Color.red);

        if (!aiActive)
            return;
        if (my_Target == null)
        {
            Debug.Log("No hay target");
            return;
        }
        if (my_NavMeshAgent.pathPending)
        {
            Debug.Log("PathPending");
            return;
        }

        if (my_NavMeshAgent.remainingDistance < my_NavMeshAgent.stoppingDistance * stopingDistanceProportion)
        {
            //Stop Code para que dejer de moverse
        }

        currentState.UpdateState(this);

	}
    void FixedUpdate()
    {

    }
    public void LookForRunAwayPoint()
    {
        if (!my_Target.CompareTag("RunAwayPoint"))
        {
            var runIndex = Random.Range(0, GameManager.Instance.runAwayPoints.Length);
            my_Target = GameManager.Instance.runAwayPoints[runIndex];
        }
        else
            return;
    }
    public void Move(float speed)       //Metodo para mover el personaje en la direccion del target
    {
        if (my_NavMeshAgent.isStopped == false)
        {
            Quaternion targetLookRotation = Quaternion.LookRotation(my_NavMeshAgent.desiredVelocity);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetLookRotation, turnSmooth);
        }
        else
            return;
    }
    public void TransitionToState(AIState nextState)
    {
        if(nextState != remainState)
        {
            currentState = nextState;
        }
    }

    IEnumerator UpdateDestination()
    {
        if (my_Target == null)
        {
            yield return false;
        }
        my_NavMeshAgent.SetDestination(my_Target.position);
        yield return new WaitForSeconds(1 / updateRate); //Numero de Updates por segundo.
        StartCoroutine(UpdateDestination());
    }

    public IEnumerator StartStealAndRun()   //Se ejecuta cuando encuentra a la casa y esta en el rango de ella
    {
        my_NavMeshAgent.isStopped = true;
        //Primero debo de ejecutar la animacion de robar y los metodos etc.
        //Buscar el punto de runaway
        if (!my_Target.CompareTag("RunAwayPoint"))
        {
            LookForRunAwayPoint();
        }
        yield return new WaitForSeconds(1.5f);
        canMove = true;
        my_NavMeshAgent.isStopped = false;      //Se le dice al agent que se mueva luego de que el codigo se haya ejecutado
    }

    public void TakeDamage(int damage)      //Este metodo se usa con el send mesagge para el daño y mirar si murio
    {
        Debug.Log("Estoy recibiendo daño");
        health -= damage;
        if (health <= 0)
        {
            PoolsManager.Instance.ReleaseObject(this.gameObject);
        }
    }
}
