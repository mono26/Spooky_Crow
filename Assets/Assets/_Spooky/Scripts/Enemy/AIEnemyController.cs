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
    public EnemyInfo my_EnemyInfo;
    public float ability1CDTimer;
    public float ability2CDTimer;     

    public AIState enemyCurrentState;
    public AIState EnemyRemainState;    //Estado de hacer nada, para que siempre el estado a cambiar sea diferente a este.
    public AIState enemyOriginalState;
    //Catching
    public Animator my_Animator;
    public Slider my_HealthBar;
    public NavMeshAgent my_NavMeshAgent;
    public Transform my_Target;        //Referencia al target del objeto, player o house se le puede preguntar al game manager por medio del singleton.
    public Transform my_Sprite;        //Es un game object para poder flipear el sprite con el scale sin alterar el collider del parent.

    public float updateRate = 2f;

    public float stoleValue;
    public bool finishStealing = false;     //Valor por default porque cuando empieza no ha robado

    public int health;      //Debe de ser sacada del enemyInfo para ser almacenada aqui. Nunca modificar los valores del enemyInfo!

	//Particles && VFX
	public GameObject feathersParticle;
	public GameObject smokeExplosion;
	public float[] smokeRotations;

    void Awake()
    {
        my_NavMeshAgent = GetComponent<NavMeshAgent>();
        my_Animator = transform.GetComponent<Animator>();


    }
    private void Start()
    {

    }
    // Use this for initialization
    void OnEnable ()
    {
        InitializeEnemy();

        if (!my_Target)
        {
            SearchForEnemy();
        }
    }

    public void InitializeEnemy()
    {
        SearchForEnemy();

        enemyCurrentState = enemyOriginalState;

        my_NavMeshAgent.isStopped = false;
        my_NavMeshAgent.updateRotation = false;
        my_NavMeshAgent.SetDestination(my_Target.position);
        StartCoroutine(UpdateDestination());

        health = my_EnemyInfo.enemyHealthPoints;
        my_HealthBar.maxValue = health;
        my_HealthBar.value = health;
    }
    private void SearchForEnemy()
    {
        if (!my_Target)
        {
            Debug.Log("There is no target for me" + this.name);
            //Execute SearchTargetCode;
            switch (my_EnemyInfo.my_Type)
            {
                case (EnemyInfo.EnemyType.STEALER):
                    {
                        my_Target = GameManager.Instance.stealPoints[Random.Range(0, 5)].transform;
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
    public void Animate()
    {
        if (!my_Target)
        {
            return;
        }
        if(my_EnemyInfo.my_Type == EnemyInfo.EnemyType.BOSS)
        {
            if (transform.position.x <= my_Target.position.x)
            {
                my_Sprite.localScale = new Vector3(-1, 1, 1);
            }
            else if (transform.position.x > my_Target.position.x)
            {
                my_Sprite.localScale = new Vector3(1, 1, 1);
            }
        }
        else
        {
            if (transform.position.x <= my_Target.position.x)
            {
                my_Sprite.localScale = new Vector3(1, 1, 1);
            }
            else if (transform.position.x > my_Target.position.x)
            {
                my_Sprite.localScale = new Vector3(-1, 1, 1);
            }
        }
    }
    public override void TransitionToState(AIState nextState)
    {
        if(nextState != EnemyRemainState)
        {
            enemyCurrentState = nextState;
        }
    }
    IEnumerator UpdateDestination()
    {
        if (my_Target == null)
        {
            yield return false;
        }
        my_NavMeshAgent.SetDestination(my_Target.position);
        enemyCurrentState.UpdateState(this);
        Animate();
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
        if (objectCDTimer1 <= 0)
        {
            objectCDTimer1 = my_EnemyInfo.enemyAbility1.abilityCooldown;
        }
        else
            return;
    }
    public void Ability2()     //Esta es la abilidad ranged, contenida en myPlantInfo
    {
        if (ability2CDTimer <= 0)
        {
            ability2CDTimer = objectCDTimer1 = my_EnemyInfo.enemyAbility2.abilityCooldown;
            my_Animator.SetTrigger("Ability2");
        }
        else
            return;
    }
    public override void CastAbility2()
    {
        my_EnemyInfo.enemyAbility2.Ability(this);
    }
    public override void CastAbility1()
    {
        my_EnemyInfo.enemyAbility1.Ability(this);
    }
    public void Die()
    {
		//Rotacion aleatoria para humo explosion
		float randomRot = Random.Range (0,360);
		var explotion = Instantiate(smokeExplosion , transform.position  , Quaternion .Euler(90,0,randomRot ));
        WaveSpawner.Instance.my_NumberOfEnemies--;
        var drop = DropPool.Instance.GetDrop();
        drop.transform.position = this.transform.position;
        drop.GetComponent<Drop>().SetReward(my_EnemyInfo.enemyReaward);
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
