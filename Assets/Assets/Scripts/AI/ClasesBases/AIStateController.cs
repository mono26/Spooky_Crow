using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[RequireComponent (typeof(Rigidbody), typeof(Seeker))]
public class AIStateController : MonoBehaviour
{
    public EnemyInfo enemyInfo;
    public Transform eyes;

    public AIState currentState;
    public AIState remainState;    //Estado de hacer nada, para que siempre el estado a cambiar sea diferente a este.
    //Catching
    public AIAtack aiAtack;
    public Rigidbody rigidBody;     //Referecia al rigidBody del objeto
    public Transform target;        //Referencia al target del objeto, player o house se le puede preguntar al game manager por medio del singleton.
    public Seeker seeker;       //Referencia al seeker del objeto
    public float UpdateRate = 2f;
    public Path path;
    public bool pathIsEnded;
    public float nextWayPointDistance = 0.3f;
    public int currentWayPoint = 0;
    public bool aiActive;

    void Awake()
    {
        seeker = GetComponent<Seeker>();
        rigidBody = GetComponent<Rigidbody>();
        aiAtack = GetComponent<AIAtack>();
    }
	// Use this for initialization
	void Start ()
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
    public void Move()
    {
        Vector3 direction = (path.vectorPath[currentWayPoint] - transform.position);
        direction = direction.normalized;
        direction *= enemyInfo.speed/2 * Time.deltaTime;
        direction.y = 0.0f;
        rigidBody.velocity += direction;
    }

    void OnDrawGizmos()
    {
        if (currentState != null && eyes != null)
        {
            Gizmos.color.Equals(currentState.sceneGuizmoColor);
            Gizmos.DrawWireSphere(eyes.position, enemyInfo.lookSphereCastRadius);
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
        }
    }
    public void TransitionToState(AIState nextState)
    {
        if(nextState != remainState)
        {
            Debug.Log("Cambiando de estado");
            Debug.Log(nextState.name);
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
}
