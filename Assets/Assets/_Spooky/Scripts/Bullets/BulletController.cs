using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : AIController
{
    public float bulletTime;
    public float specialTime;

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
        SetCD1(bulletInfo.objectCooldown1);
    }
    private void Update()
    {
        if (bulletTime > 0)
        {
            bulletTime -= Time.deltaTime;
            objectCDTimer1 = bulletTime;
        }
        if (specialTime > 0)
        {
            specialTime -= Time.deltaTime;
            objectCDTimer2 = specialTime;
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
    public override void SetCD1(float cooldown)
    {
        bulletTime = cooldown;    // este es el valor que asigne en el editor
        objectCDTimer1 = cooldown;
    }
    public override void SetCD2(float cooldown)
    {
        specialTime = cooldown;
        objectCDTimer2 = cooldown;
    }
    public override IEnumerator UpdateState()
    {
        bulletInfo.objectAbility1.Ability(this);
        yield return new WaitForSeconds(1 / bulletUpdateRate); //Numero de Updates por segundo.
        StartCoroutine(UpdateState());
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<AIEnemyController>().TakeDamage(bulletInfo.objectDamage);
            if(bulletInfo.objectAbility1 != null)
            {
                SetEffects();
                SetCD1(0);
                SetCD2(objectInfo.objectCooldown2);
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
        if(bulletInfo.objectAbility1 != null)
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
        if (bulletInfo.objectSpecialEffects != null)
        {
            for (int specialEffect = 0; specialEffect < bulletInfo.objectSpecialEffects.Length; specialEffect++)
                bulletInfo.objectSpecialEffects[specialEffect].SetActive(true);
        }
        else return;
    }
    private void DeactivateEspecialEffects()
    {
        if (bulletInfo.objectSpecialEffects != null)
        {
            for (int i = 0; i < bulletInfo.objectSpecialEffects.Length; i++)
                bulletInfo.objectSpecialEffects[i].SetActive(false);
        }
        else return;
    }
    private void ActivateBulletSprite()
    {
        bulletInfo.objectMainSprite.SetActive(true);
    }
    private void DeactivateBulletSprite()
    {
        Debug.Log("Desactivar el sprite de la bala");
        bulletInfo.objectMainSprite.SetActive(false);
    }
}
