using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlantController : AIController
{
    //Informacion especifica de esta planta
    public PlantInfo plantInfo;
    public float meleeTimer;
    public float basicTimer;
    public float specialTimer;
    //Informacion de los estados de la planta
    public AIState plantCurrentState;
    public AIState plantRemainState;
    public AIState plantOriginalState;
    //Informacion para el funcionamiento
    public Animator plantAnimator;
    public Transform plantTarget;
    public Transform plantSprite;
    public Transform plantShootPoint;
    public Collider plantMeleeCoollider;

    public float plantForce;
    public float plantUpdateRate;
    public float plantHealth;

    public SoundPlayer plantSoundPlayer;

    private void OnDrawGizmos()
    {
        //Shoot Range
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, plantInfo.plantShootRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, plantInfo.plantMeleeRange);
    }
    private void OnEnable()
    {
        Initialize();
        StartCoroutine(UpdateState());
    }
    private void Awake()
    {
        plantAnimator = transform.GetComponent<Animator>();
        plantSoundPlayer = this.GetComponent<SoundPlayer>();
    }
    private void Update()
    {
        if (meleeTimer > 0)
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
            this.objectSpecialTimer = specialTimer;
        }
    }

    //Metodos del Padre
    public override void Initialize()
    {
        SetStartCurrentState();
        SetParentVariables();
        plantInfo.InitializeInfo();
        if (plantMeleeCoollider != null)
        {
            plantMeleeCoollider.gameObject.SetActive(false);
        }
    }

    //Metodos privados
    private void SetParentVariables()
    {
        this.objectAnimator = plantAnimator;
        this.objectInfo = plantInfo;
        this.objectSprite = plantSprite;
        this.objectTarget = plantTarget;
        this.objectOriginalState = plantOriginalState;
        this.objectRemainState = plantRemainState;
        this.objectForce = plantForce;
        this.objectUpdateRate = plantUpdateRate;
        this.soundPlayer = plantSoundPlayer;
        if (plantMeleeCoollider != null)
        {
            this.objectMeleeCollider = plantMeleeCoollider;
        }
    }
    private void SetStartCurrentState()
    {
        plantCurrentState = plantOriginalState;
        this.objectCurrentState = plantCurrentState;
    }

    //Sobreescritura del padre
    public override IEnumerator UpdateState()
    {
        plantCurrentState.UpdateState(this);
        Animate();
        yield return new WaitForSeconds(1 / plantUpdateRate);
        StartCoroutine(UpdateState());
    }
    public override void ChangeTarget(Transform newTarget)
    {
        plantTarget = newTarget;
        this.objectTarget = newTarget;
    }
    public override void SetMeleeCoolDown(float cooldown)
    {
        meleeTimer = cooldown;
        this.objectMeleeTimer = meleeTimer;
    }
    public override void SetBasicCoolDown(float cooldown)
    {
        basicTimer = cooldown;
        this.objectBasicTimer = basicTimer;
    }
    public override void SetSpecialCoolDown(float cooldown)
    {
        specialTimer = cooldown;
        this.objectSpecialTimer = specialTimer;
    }
    public void Animate()
    {
        if(!plantTarget)
        {
            return;
        }
        if (transform.position.x <= plantTarget.position.x)
        {
            plantSprite.localScale = new Vector3(-1,1,1);
        }
        else if (transform.position.x > plantTarget.position.x)
        {
            plantSprite.localScale = new Vector3(1, 1, 1);
        }
    }
    public override void TransitionToState(AIState nextState)
    {
        if (nextState != plantRemainState)
        {
            plantCurrentState = nextState;
        }
    }
    public void TakeDamage(float damage)
    {
        plantHealth -= damage;
        if (plantHealth <= 0)
        {
            //Drop o aumentar la plata.
            Die();
        }
    }
    public void Die()
    {
        StopAllCoroutines();
        PoolsManagerPlants.Instance.ReleasePlant(this.gameObject);
    }
}
