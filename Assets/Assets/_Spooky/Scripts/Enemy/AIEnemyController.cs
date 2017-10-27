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
    public float meleeTimer;
    public float basicTimer;  
    public float specialTimer;   

    //Informacion para los estados.
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
    //Melee collider para el enemigo
    public Collider enemyMeleeCollider;
    //Numero de veces que se hace el update por segundo.
    public float enemyUpdateRate = 2f;

    public Transform enemyLootPosition;
    public float stoleValue;
    //Valor por default porque cuando empieza no ha robado
    public bool enemyFinishStealing = false;

    public float enemyHealth;      //Debe de ser sacada del enemyInfo para ser almacenada aqui. Nunca modificar los valores del enemyInfo!

    public GameObject feathersParticle;
    public GameObject explosionVFX;
    public GameObject lootObject;

    public SoundPlayer enemySoundPlayer;

    //Metodos de unity
    private void OnDrawGizmos()
    {
        //Shoot Range
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, enemyInfo.enemyRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, enemyInfo.enemyMeleeRange);
    }
    private void OnEnable()
    {
        Initialize();
        StartNavMeshForFirstTime();
    }
    private void Awake()
    {
        enemyNavMesh = GetComponent<NavMeshAgent>();
        enemyAnimator = transform.GetComponent<Animator>();
        enemySoundPlayer = this.GetComponent<SoundPlayer>();
    }
    private void Update()
    {
        if(meleeTimer > 0)
        {
            meleeTimer -= Time.deltaTime;
            this.objectMeleeTimer = meleeTimer;
        }
        if (basicTimer > 0)
        {
            basicTimer -= Time.deltaTime;
            this.objectBasicTimer = basicTimer;
        }
        if (specialTimer > 0)
        {
            specialTimer -= Time.deltaTime;
            this.objectSpecialTimer = basicTimer;
        }
    }

    public override void Initialize()
    {
        SearchForTarget();
        SetStartCurrentState();
        SetOriginalHealthValues();
        SetParentVariables();
        enemyInfo.InitializeInfo();
        if (enemyMeleeCollider != null)
        {
            enemyMeleeCollider.gameObject.SetActive(false);
        }
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
        this.lootPosition = enemyLootPosition;
        this.objectTarget = enemyTarget;
        this.objectUpdateRate = enemyUpdateRate;
        this.soundPlayer = enemySoundPlayer;
        if (enemyMeleeCollider != null)
        {
            this.objectMeleeCollider = enemyMeleeCollider;
        }
    }
    private void SetOriginalHealthValues()
    {
        enemyHealth = enemyInfo.enemyHealthPoints;
        enemyHealthBar.maxValue = enemyHealth;
        enemyHealthBar.value = enemyHealth;
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
        this.enemyTarget = newTarget;
        this.objectTarget = newTarget;
        this.enemyNavMesh.isStopped = true;
    }
    public override void TransitionToState(AIState nextState)
    {
        if(nextState != enemyRemainState)
        {
            enemyCurrentState = nextState;
        }
    }
    public override void SetMeleeCoolDown(float cooldown)
    {
        meleeTimer = cooldown;
        objectMeleeTimer = meleeTimer;
    }
    public override void SetBasicCoolDown(float cooldown)
    {
        basicTimer = cooldown;
        objectBasicTimer = basicTimer;
    }
    public override void SetSpecialCoolDown(float cooldown)
    {
        specialTimer = cooldown;
        objectSpecialTimer = specialTimer;
    }
    public void Flip()
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
        Flip();
        yield return new WaitForSeconds(1 / enemyUpdateRate); //Numero de Updates por segundo.
        StartCoroutine(UpdateState());
    }
    public void TakeDamage(float damage)      //Este metodo se usa con el send mesagge para el daño y mirar si murio
    {
        var feathersP = Instantiate(feathersParticle  , transform.position, Quaternion.Euler(-90,0,0));
        enemyHealth -= damage;
        enemyHealthBar.value = enemyHealth;
        if (enemyHealth <= 0)
        {
            //Drop o aumentar la plata.
            Die();
        }
    }
    public void Die()
    {
		//Rotacion aleatoria para humo explosion
		float randomRot = Random.Range (0,360);
		var explotion = Instantiate(explosionVFX , transform.position  , Quaternion .Euler(90,0,randomRot ));
        WaveSpawner.Instance.gameNumberOfEnemies--;
        var drop = PoolsManagerDrop.Instance.GetObject(enemyInfo.enemyDrop.dropIndex, this.transform);
        drop.GetComponent<EnemyDrop>().SetReward(enemyInfo.enemyReward);
        if(lootObject)
        {
            //sacar el loot del parent
            lootObject.transform.parent = null;
        }
        StopAllCoroutines();
        PoolsManagerEnemies.Instance.ReleaseEnemy(this.gameObject);
        //enemySoundPlayer.PlayClip();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PlantMeleeCollider"))
        {
            TakeDamage(other.GetComponentInParent<AIPlantController>().plantInfo.objectDamage);
        }
        if(other.CompareTag("Bullet"))
        {
            TakeDamage(other.GetComponent<BulletController>().bulletInfo.objectDamage);
        }
    }
}
