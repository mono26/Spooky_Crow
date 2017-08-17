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
    public AIState enemyRemainState;    //Estado de hacer nada, para que siempre el estado a cambiar sea diferente a este.
    public AIState enemyOriginalState;
    //Catching
    public Animator enemyAnimator;
    public Slider enemyHealthBar;
    public NavMeshAgent enemyNavMesh;
    public Transform enemyTarget;        //Referencia al target del objeto, player o house se le puede preguntar al game manager por medio del singleton.
    public Transform enemySprite;        //Es un game object para poder flipear el sprite con el scale sin alterar el collider del parent.

    public float updateRate = 2f;

    public float stoleValue;
    public bool enemyFinishStealing = false;     //Valor por default porque cuando empieza no ha robado

    public int health;      //Debe de ser sacada del enemyInfo para ser almacenada aqui. Nunca modificar los valores del enemyInfo!

	//Particles && VFX
	public GameObject feathersParticle;
	public GameObject smokeExplosion;
	public float[] smokeRotations;

    public AIEnemyController()
    {
        objectAnimator = enemyAnimator;
        objectCDTimer1 = ability1CDTimer;
        objectCDTimer2 = ability2CDTimer;
        objectCurrentState = enemyCurrentState;
        objectFinishedStealing = enemyFinishStealing;
        objectInfo = enemyInfo;
        objectNavMesh = enemyNavMesh;
        objectOriginalState = enemyOriginalState;
        objectRemainState = enemyRemainState;
        objectSprite = enemySprite;
        objectTarget = enemyTarget;
    }
    void Awake()
    {
        enemyNavMesh = GetComponent<NavMeshAgent>();
        enemyAnimator = transform.GetComponent<Animator>();
    }
    private void Start()
    {
        
    }
    // Use this for initialization
    void OnEnable ()
    {
        InitializeEnemy();

        if (!enemyTarget)
        {
            SearchForEnemy();
        }
    }

    public void InitializeEnemy()
    {
        SearchForEnemy();

        enemyCurrentState = enemyOriginalState;

        enemyNavMesh.isStopped = false;
        enemyNavMesh.updateRotation = false;
        enemyNavMesh.SetDestination(enemyTarget.position);
        StartCoroutine(UpdateDestination());

        health = enemyInfo.enemyHealthPoints;
        enemyHealthBar.maxValue = health;
        enemyHealthBar.value = health;
    }
    private void SearchForEnemy()
    {
        if (!enemyTarget)
        {
            Debug.Log("There is no target for me" + this.name);
            //Execute SearchTargetCode;
            switch (enemyInfo.my_Type)
            {
                case (EnemyInfo.EnemyType.STEALER):
                    {
                        enemyTarget = GameManager.Instance.stealPoints[Random.Range(0, 5)].transform;
                        break;
                    }
                case (EnemyInfo.EnemyType.ATACKER):
                    {
                        enemyTarget = GameManager.Instance.player.transform;
                        break;
                    }
                case (EnemyInfo.EnemyType.BOSS):
                    {
                        //Assigna el target al inicio dependiendo del tipo
                        enemyTarget = GameManager.Instance.player.transform;
                        break;
                    }
            }
        }
    }
    public void Animate()
    {
        if (!enemyTarget)
        {
            return;
        }
        if(enemyInfo.my_Type == EnemyInfo.EnemyType.BOSS)
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
    public override void TransitionToState(AIState nextState)
    {
        if(nextState != enemyRemainState)
        {
            enemyCurrentState = nextState;
        }
    }
    IEnumerator UpdateDestination()
    {
        if (enemyTarget == null)
        {
            yield return false;
        }
        enemyNavMesh.SetDestination(enemyTarget.position);
        enemyCurrentState.UpdateState(this);
        Animate();
        yield return new WaitForSeconds(1 / updateRate); //Numero de Updates por segundo.
        StartCoroutine(UpdateDestination());
    }
    public void TakeDamage(int damage)      //Este metodo se usa con el send mesagge para el daño y mirar si murio
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
    public override void CastAbility2()
    {
        if(ability2CDTimer <= 0)
        {
            enemyInfo.enemyAbility2.Ability(this);
        }
    }
    public override void CastAbility1()
    {
        if (ability1CDTimer <= 0)
        {
            enemyInfo.enemyAbility1.Ability(this);
        }
    }
    public void Die()
    {
		//Rotacion aleatoria para humo explosion
		float randomRot = Random.Range (0,360);
		var explotion = Instantiate(smokeExplosion , transform.position  , Quaternion .Euler(90,0,randomRot ));
        WaveSpawner.Instance.my_NumberOfEnemies--;
        var drop = DropPool.Instance.GetDrop();
        drop.transform.position = this.transform.position;
        drop.GetComponent<Drop>().SetReward(enemyInfo.enemyReaward);
        PoolsManagerEnemies.Instance.ReleaseObject(this.gameObject);
    }
    public void Escape()
    {
        WaveSpawner.Instance.my_NumberOfEnemies--;
        PoolsManagerEnemies.Instance.ReleaseObject(this.gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            TakeDamage(other.GetComponent<BulletController>().daño);
            PoolsManagerBullets.Instance.ReleaseBullet(other.gameObject);
			Instantiate (feathersParticle, transform.position, Quaternion .Euler (-90,0,0));
			feathersParticle.GetComponent <ParticleSystem > ().Play ();

        }
        if (other.CompareTag("RunAwayPoint"))
        {
            Escape();
        }
        else return;
    }

		
}
