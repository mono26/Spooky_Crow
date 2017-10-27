using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : AIController
{
    public float bulletTime;
    public float specialTime;

    public GameObject bulletSprite;
    public GameObject[] bulletEffects;

    //Numero de veces que se hace el update por segundo.
    public float bulletUpdateRate;

    public Info bulletInfo;

    public Rigidbody bulletRigidBody;

    private void Awake()
    {
       bulletRigidBody = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        Initialize();
        ActivateBulletSprite();
        DeactivateEspecialEffects();
    }
    private void OnEnable()
    {
        Initialize();
        ActivateBulletSprite();
        DeactivateEspecialEffects();
        SetBasicCoolDown(bulletInfo.objectBasicCooldown);
    }
    private void Update()
    {
        if (bulletTime > 0)
        {
            bulletTime -= Time.deltaTime;
            objectMeleeTimer = bulletTime;
        }
        if (specialTime > 0)
        {
            specialTime -= Time.deltaTime;
            objectBasicTimer = specialTime;
        }
        if(bulletTime <= 0 && specialTime <= 0)
        {
            PoolsManagerBullets.Instance.ReleaseBullet(gameObject);
            //Pool de efectos especiales o al mismo pool de las balas.
        }
    }
    private void SetParentVariables()
    {
        this.objectInfo = bulletInfo;
    }
    public override void Initialize()
    {
        SetParentVariables();
        bulletInfo.InitializeInfo();
    }
    public override void SetSpecialCoolDown(float cooldown)
    {
        specialTime = cooldown;
        objectBasicTimer = cooldown;
    }
    public override void SetBasicCoolDown(float cooldown)
    {
        bulletTime = cooldown;
        objectMeleeTimer = cooldown;
    }
    public override IEnumerator UpdateState()
    {
        bulletInfo.objectBasicAbility.Ability(this);
        yield return new WaitForSeconds(1 / bulletUpdateRate); //Numero de Updates por segundo.
        StartCoroutine(UpdateState());
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if(bulletInfo.objectBasicAbility != null)
            {
                SetEffects();
                SetMeleeCoolDown(0);
                SetSpecialCoolDown(objectInfo.objectSpecialCooldown);
                StartCoroutine(UpdateState());
            }
            else
            {
                SetEffects();
                PoolsManagerBullets.Instance.ReleaseBullet(gameObject);
            }
        }
        else return;
    }
    private void SetEffects()
    {
        //Aqui se setean los effectos y si tiene abilidad tambien.
        if(bulletInfo.objectBasicAbility != null)
        {
            //Se saca desde el pool de los efectos para ponerlos en donde se pone.
            bulletRigidBody.velocity = Vector3.zero;
            //Instantiate(bulletInfo.objectAbility1.spriteEffect, transform.position, Quaternion.identity);
            ActivateEspecialEffects();
            DeactivateBulletSprite();
        }
        else
        {
            //Instantiate (feathersParticle, transform.position, Quaternion .Euler (-90,0,0));
            //feathersParticle.GetComponent <ParticleSystem > ().Play ();
        }
    }
    private void ActivateEspecialEffects()
    {
        if (bulletEffects != null)
        {
            for (int specialEffect = 0; specialEffect < bulletEffects.Length; specialEffect++)
            {
                bulletEffects[specialEffect].SetActive(true);
            }
        }
        else return;
    }
    private void DeactivateEspecialEffects()
    {
        if (bulletEffects != null)
        {
            for (int specialEffect = 0; specialEffect < bulletEffects.Length; specialEffect++)
            {
                bulletEffects[specialEffect].SetActive(false);
            }
        }
        else return;
    }
    private void ActivateBulletSprite()
    {
        bulletSprite.SetActive(true);
    }
    private void DeactivateBulletSprite()
    {
        bulletSprite.SetActive(false);
    }
}
