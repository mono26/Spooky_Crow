using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AI;
using UnityEngine.UI;

[RequireComponent (typeof(Rigidbody), typeof(NavMeshAgent))]
public class AIEnemyController : MonoBehaviour
{
    public EnemyInfo my_EnemyInfo;
    public float cdTimer1;
    public float cdTimer2;

    [SerializeField]
    public float cdTime1;
    [SerializeField]
    public float cdTime2;

    public AIEnemyState currentState;
    public AIEnemyState remainState;    //Estado de hacer nada, para que siempre el estado a cambiar sea diferente a este.
    //Catching
    public Animator my_Animator;
    public Slider my_HealthBar;
    public Rigidbody my_RigidBody;     //Referecia al rigidBody del objeto
    public NavMeshAgent my_NavMeshAgent;
    public Transform my_Target;        //Referencia al target del objeto, player o house se le puede preguntar al game manager por medio del singleton.
    public Collider my_MeleeCollider;
    public Transform my_Sprite;        //Es un game object para poder flipear el sprite con el scale sin alterar el collider del parent.


    public float updateRate = 2f;
    public float stopingDistanceProportion;

    public float stoleValue;
    public bool finishStealing = false;     //Valor por default porque cuando empieza no ha robado
    public bool aiActive = true;

    public int health;      //Debe de ser sacada del enemyInfo para ser almacenada aqui. Nunca modificar los valores del enemyInfo!
    public float turnSmooth;    //Valor para el smooth cuando se rota el objeto por medio de los quaternions

    void Awake()
    {
        my_RigidBody = GetComponent<Rigidbody>();
        my_NavMeshAgent = GetComponent<NavMeshAgent>();
        my_Animator = transform.GetComponent<Animator>();

        if (!my_Target)
        {
            Debug.Log("There is no target for me" + this.name);
            //Execute SearchTargetCode;
            switch (my_EnemyInfo.my_Type)
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
    }
    private void Start()
    {
        if (!my_Target)
        {
            Debug.Log("There is no target for me" + this.name);
            //Execute SearchTargetCode;
            switch (my_EnemyInfo.my_Type)
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
    }
    // Use this for initialization
    void OnEnable ()
    {
        my_NavMeshAgent.SetDestination(my_Target.position);
        my_NavMeshAgent.isStopped = false;
        my_NavMeshAgent.updateRotation = false;
        StartCoroutine(UpdateDestination());

        health = my_EnemyInfo.health;
        my_HealthBar.maxValue = health;
        my_HealthBar.value = health;
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
        if (cdTimer1 > 0)
        {
            cdTimer1 -= Time.deltaTime;
        }
        if (cdTimer2 > 0)
        {
            cdTimer2 -= Time.deltaTime;
        }
        currentState.UpdateState(this);
        Animate();

	}
    public void Animate()
    {
        if (!my_Target)
        {
            return;
        }
        if (transform.position.x <= my_Target.position.x)
        {
            my_Sprite.localScale = new Vector3(1, 1, 1);
        }
        else if (transform.position.x > my_Target.position.x)
        {
            my_Sprite.localScale = new Vector3(-1, 1, 1);
        }
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
    public void TransitionToState(AIEnemyState nextState)
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
    public void TakeDamage(int damage)      //Este metodo se usa con el send mesagge para el daño y mirar si murio
    {
        Debug.Log("Estoy recibiendo daño");
        health -= damage;
        my_HealthBar.value = health;
        if (health <= 0)
        {
            //Drop o aumentar la plata.
            Die();
        }
    }
    public void Ability1()     //Esta es la abilidad melee, contenida en myPlantInfo
    {
        if (cdTimer1 <= 0)
        {
            my_EnemyInfo.ability1.Ability(this.gameObject);
            cdTimer1 = cdTime1;
        }
        else
            return;
    }
    public void Ability2()     //Esta es la abilidad ranged, contenida en myPlantInfo
    {
        if (cdTimer2 <= 0)
        {
            cdTimer2 = cdTime2;
            my_Animator.SetTrigger("Ability2");
        }
        else
            return;
    }
    private void CastAbility2()
    {
        my_EnemyInfo.ability2.Ability(this.gameObject);
    }
    private void CastAbility1()
    {
        my_EnemyInfo.ability1.Ability(this.gameObject);
    }
    public void Die()
    {
<<<<<<< HEAD:Assets/Assets/_Spooky/Scripts/Enemy/ClasesBases_Enemy/AIEnemyController.cs
        WaveSpawner.Instance.my_NumberOfEnemies--;
=======
>>>>>>> origin/master:Assets/Assets/_Spooky/Scripts/Enemy/ClasesBases_Enemy/AIEnemyController.cs
        GameManager.Instance.GiveMoney(my_EnemyInfo.reward);
        PoolsManager.Instance.ReleaseObject(this.gameObject);
    }
}
