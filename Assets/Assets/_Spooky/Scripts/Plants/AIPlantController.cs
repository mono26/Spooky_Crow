using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlantController : AIController
{
    public PlantInfo plantInfo;
    public float ability1CDTimer;
    public float ability2CDTimer;

    public AIState plantCurrentState;
    public AIState plantRemainState;
    public AIState plantOriginalState;

    public Animator plantAnimator;
    public Transform plantTarget;
    public Transform plantSprite;
    public Transform plantShootPoint;

    public float plantForce;
    public float plantUpdateRate;

    private void OnEnable()
    {
        Initialize();
        StartCoroutine(UpdateState());
    }
    private void Awake()
    {
        plantAnimator = transform.GetComponent<Animator>();
    }
    private void Update()
    {
        if (ability1CDTimer > 0)
        {
            ability1CDTimer -= Time.deltaTime;
            objectCDTimer1 = ability1CDTimer;
        }
        if (ability2CDTimer > 0)
        {
            ability2CDTimer -= Time.deltaTime;
            objectCDTimer2 = ability2CDTimer;
        }
        if(plantTarget != null && !plantTarget.gameObject.activeInHierarchy)
        {
            plantTarget = null;
        }
    }

    //Metodos del Padre
    public override void Initialize()
    {
        SetStartCurrentState();
        SetParentVariables();
        plantInfo.InitializeInfo();
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
        objectUpdateRate = plantUpdateRate;
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
        objectTarget = newTarget;
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
}
