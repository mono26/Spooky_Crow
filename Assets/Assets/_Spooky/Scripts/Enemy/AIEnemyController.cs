using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AI;
using UnityEngine.UI;

[RequireComponent (typeof(NavMeshAgent))]
public class AIEnemyController : AIController
{
    //Cada habilidad debe de contener su cooldown propio
    public EnemyInfo enemyInfo;
    public float ability1CDTimer;
    public float ability2CDTimer;     

    public AIState enemyCurrentState;
    //Estado de hacer nada, para que siempre el estado a cambiar sea diferente a este.
    public AIState enemyRemainState;
    public AIState enemyOriginalState;
    //Catching
    public Animator enemyAnimator;
    public Slider enemyHealthBar;
    public NavMeshAgent enemyNavMesh;
    //Referencia al target del objeto, player o house se le puede preguntar al game manager por medio del singleton.
    public Transform enemyTarget;
    //Es un game object para poder flipear el sprite con el scale sin alterar el collider del parent.
    public Transform enemySprite;

    //Numero de veces que se hace el update por segundo.
    public float enemyUpdateRate = 2f;

    public float stoleValue;
    //Valor por default porque cuando empieza no ha robado
    public bool enemyFinishStealing = false;

    public float health;      //Debe de ser sacada del enemyInfo para ser almacenada aqui. Nunca modificar los valores del enemyInfo!

    //Metodos de unity
    private void OnEnable()
    {
        Initialize();
        StartNavMeshForFirstTime();
    }
    private void Awake()
    {
        enemyNavMesh = GetComponent<NavMeshAgent>();
        enemyAnimator = transform.GetComponent<Animator>();
    }
    private void Start()
    {
        Initialize();
        StartNavMeshForFirstTime();
    }
    private void Update()
    {
        if(ability1CDTimer > 0)
        {
            ability1CDTimer -= Time.deltaTime;
            objectCDTimer1 = ability1CDTimer;
        }
        if (ability2CDTimer > 0)
        {
            ability2CDTimer -= Time.deltaTime;
            objectCDTimer2 = ability2CDTimer;
        }
    }

    public override void Initialize()
    {
        SearchForTarget();
        SetStartCurrentState();
        SetOriginalHealthValues();
        SetParentVariables();
        enemyInfo.InitializeInfo();
    }

    //Metodos privados
    private void SetParentVariables()
    {
        this.objectAnimator = enemyAnimator;
        this.objectFinishedStealing = enemyFinishStealing;
        this.objectInfo = enemyInfo;
        this.objectNavMesh = enemyNavMesh;
        this.objectOriginalState = enemyOriginalState;
        this.objectRemainState = enemyRemainState;
        this.objectSprite = enemySprite;
        this.objectTarget = enemyTarget;
        objectUpdateRate = enemyUpdateRate;
    }
    private void SetOriginalHealthValues()
    {
        health = enemyInfo.enemyHealthPoints;
        enemyHealthBar.maxValue = health;
        enemyHealthBar.value = health;
    }
    private void SetStartCurrentState()
    {
        enemyCurrentState = enemyOriginalState;
        this.objectCurrentState = enemyCurrentState;
    }
    private void StartNavMeshForFirstTime()
    {
        enemyNavMesh.isStopped = false;
        enemyNavMesh.updateRotation = false;
        enemyNavMesh.SetDestination(enemyTarget.position);
        StartCoroutine(UpdateState());
    }
    private void SearchForTarget()
    {
        //Execute SearchTargetCode;
        switch (enemyInfo.enemyType)
        {
            case (EnemyInfo.EnemyType.STEALER):
                {
                    enemyTarget = GameManager.Instance.houseStealPoints[Random.Range(0, GameManager.Instance.houseStealPoints.Length)];
                    break;
                }
            case (EnemyInfo.EnemyType.ATACKER):
                {
                    enemyTarget = GameManager.Instance.playerSpooky.transform;
                    break;
                }
            case (EnemyInfo.EnemyType.BOSS):
                {
                    //Assigna el target al inicio dependiendo del tipo
                    enemyTarget = GameManager.Instance.playerSpooky.transform;
                    break;
                }
        }
    }

    //Eventos Publicos
    public override void ChangeTarget(Transform newTarget)
    {
        enemyTarget = newTarget;
        objectTarget = newTarget;
    }
    public override void TransitionToState(AIState nextState)
    {
        if(nextState != enemyRemainState)
        {
            enemyCurrentState = nextState;
        }
    }
    public override void SetCD1(float cooldown)
    {
        ability1CDTimer = cooldown;
        objectCDTimer1 = ability1CDTimer;
    }
    public override void SetCD2(float cooldown)
    {
        ability2CDTimer = cooldown;
        objectCDTimer2 = ability2CDTimer;
    }
    public void Animate()
    {
        if (!enemyTarget)
        {
            return;
        }
        if (enemyInfo.enemyType == EnemyInfo.EnemyType.BOSS)
        {
            if (transform.position.x <= enemyTarget.position.x)
            {
                enemySprite.localScale = new Vector3(-1, 1, 1);
            }
            else if (transform.position.x > enemyTarget.position.x)
            {
                enemySprite.localScale = new Vector3(1, 1, 1);
            }
        }
        else
        {
            if (transform.position.x <= enemyTarget.position.x)
            {
                enemySprite.localScale = new Vector3(1, 1, 1);
            }
            else if (transform.position.x > enemyTarget.position.x)
            {
                enemySprite.localScale = new Vector3(-1, 1, 1);
            }
        }
    }
    public override IEnumerator UpdateState()
    {
        if (enemyTarget == null)
        {
            yield return false;
        }
        enemyCurrentState.UpdateState(this);
        Animate();
        yield return new WaitForSeconds(1 / enemyUpdateRate); //Numero de Updates por segundo.
        StartCoroutine(UpdateState());
    }
    public void TakeDamage(float damage)      //Este metodo se usa con el send mesagge para el daño y mirar si murio
    {
        Debug.Log("Estoy recibiendo daño");
        health -= damage;
        enemyHealthBar.value = health;
        if (health <= 0)
        {
            //Drop o aumentar la plata.
            Die();
        }
    }
    public void Die()
    {
		//Rotacion aleatoria para humo explosion
		float randomRot = Random.Range (0,360);
		//var explotion = Instantiate(smokeExplosion , transform.position  , Quaternion .Euler(90,0,randomRot ));
        WaveSpawner.Instance.gameNumberOfEnemies--;
        var drop = DropPool.Instance.GetDrop();
        drop.transform.position = this.transform.position;
        drop.GetComponent<Drop>().SetReward(enemyInfo.enemyReaward);
        StopAllCoroutines();
        PoolsManagerEnemies.Instance.ReleaseObject(this.gameObject);
    }
}
