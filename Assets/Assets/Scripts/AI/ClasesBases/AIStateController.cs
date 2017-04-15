using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[RequireComponent (typeof(Rigidbody), typeof(Seeker), typeof(AIAtack))]
public class AIStateController : MonoBehaviour
{
    public EnemyInfo enemyInfo;
    public Transform eyes;

    public AIState currentState;
    public AIState remainState;    //Estado de hacer nada, para que siempre el estado a cambiar sea diferente a este.
    //Catching
    public AIAtack aiAtack;
    public AIAnimations aiAnimations;
    public Rigidbody rigidBody;     //Referecia al rigidBody del objeto
    public Transform target;        //Referencia al target del objeto, player o house se le puede preguntar al game manager por medio del singleton.
    public Seeker seeker;       //Referencia al seeker del objeto
    //Seeker info y waypoint y su distancia.
    public float UpdateRate = 2f;
    public Path path;
    public bool pathIsEnded;
    public float nextWayPointDistance = 0.8f;       //Si la distancia es menor de 0.8 el objeto se deja de mover, random BUG!
    public int currentWayPoint = 0;

    public bool canMove = true;
    public bool aiActive = true;

    void Awake()
    {
        seeker = GetComponent<Seeker>();
        rigidBody = GetComponent<Rigidbody>();
        aiAtack = GetComponent<AIAtack>();
    }
	// Use this for initialization
	void OnEnable ()
    {
		if(!target)
        {
            Debug.Log("There is no target for me" + this.name);
            //Execute SearchTargetCode;
            switch (enemyInfo.my_Type)
            {
                case (EnemyInfo.EnemyType.STEALER):
                    {
                        target = GameManager.Instance.house.transform;
                        break;
                    }
                case (EnemyInfo.EnemyType.ATACKER):
                    {
                        target = GameManager.Instance.player.transform;
                        break;
                    }
                case (EnemyInfo.EnemyType.BOSS):
                    {
                        //Assigna el target al inicio dependiendo del tipo
                        target = GameManager.Instance.player.transform;
                        break;
                    }
            }
        }
        seeker.StartPath(transform.position, target.position, OnPathComplete);
        StartCoroutine(UpdatePath());
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!aiActive)
            return;
        if (target == null)
        {
            Debug.Log("No hay target");
            return;
        }
        if (path == null)
        {
            Debug.Log("No hay Path");
            return;
        }

        if (currentWayPoint >= path.vectorPath.Count)
        {
            if (pathIsEnded)
                return;
            Debug.Log("End of Path");
            pathIsEnded = true;
            return;
        }
        pathIsEnded = false;

        var dist = Vector3.Distance(transform.position, path.vectorPath[currentWayPoint]);
        if (dist < nextWayPointDistance)
        {
            currentWayPoint++;
            return;
        }

        currentState.UpdateState(this);
	}
    void FixedUpdate()
    {

    }
    public void LookForRunAwayPoint()
    {
        if (!target.CompareTag("RunAwayPoint"))
        {
            var runIndex = Random.Range(0, GameManager.Instance.runAwayPoints.Length);
            target = GameManager.Instance.runAwayPoints[runIndex];
        }
        else
            return;
    }
    public void Move(float speed)
    {
        if (canMove)
        {
            Vector3 direction = (path.vectorPath[currentWayPoint] - transform.position);
            direction = direction.normalized;
            direction.y = 0.0f;
            Debug.Log(direction);
            transform.forward = Vector3.Lerp(transform.forward, direction, Time.deltaTime);
            rigidBody.velocity += direction * speed * Time.deltaTime;
        }
        else
            return;
    }
    void OnDrawGizmos()
    {
        if (currentState != null && eyes != null)
        {
            Gizmos.color.Equals(currentState.sceneGuizmoColor);
            Gizmos.DrawWireSphere(eyes.position, enemyInfo.lookRange);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Bullet"))
            PoolsManager.Instance.ReleaseObject(gameObject);
    }
    public void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWayPoint = 0;
            Debug.Log("Se completo un path");
        }
    }
    public void TransitionToState(AIState nextState)
    {
        if(nextState != remainState)
        {
            currentState = nextState;
        }
    }

    IEnumerator UpdatePath()
    {
        if (target == null)
        {
            yield return false;
        }
        seeker.StartPath(transform.position, target.position, OnPathComplete);
        yield return new WaitForSeconds(1 / UpdateRate); //Numero de Updates por segundo.
        StartCoroutine(UpdatePath());
    }

    public IEnumerator StartStealAndRun()   //Se ejecuta cuando encuentra a la casa y esta en el rango de ella
    {
        canMove = false;
        //Primero debo de ejecutar la animacion de robar y los metodos etc.
        //Buscar el punto de runaway
        if (!target.CompareTag("RunAwayPoint"))
        {
            LookForRunAwayPoint();
        }
        yield return new WaitForSeconds(1.5f);
        canMove = true;
    }
}
