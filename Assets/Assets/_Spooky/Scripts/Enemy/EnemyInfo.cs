using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AIComponents/EnemyInfo")]
[System.Serializable]
public class EnemyInfo : Info {
    public enum EnemyType
    {
        ATACKER, STEALER, BOSS
    }

    public EnemyType enemyType;
    public string enemyName;
    //Variable para almacenar la informacion de los pools.
    public int enemyIndex;

    //Variables para los stats
    public float enemyMoveSpeed;       //Esta es la velocidad que tendra el navMesh
    public int enemyMeleeDamage;
    public float enemyMeleeSpeed;
    public float enemyBasicSpeed;
    public float enemySpecialSpeed;
    public int enemyReward;

    public int enemyHealthPoints;        //Los puntos de vida que tendra en controller del enemigo
    public EnemyDrop enemyDrop;

    //Para las abilidades de cada enemigo
    public AIAbility enemyMeleeAbility;
    public AIAbility enemyNormalAbility;
    public AIAbility enemyEspecialAbility;

    //Vriables que contienen los rangos necesarios
    public float enemyRange;
    public float enemyMeleeRange;

    public override void InitializeInfo()
    {
        if(enemyMeleeAbility != null)
        {
            enemyMeleeAbility.InitializeAbility();
        }
        if(enemyNormalAbility != null)
        {
            enemyNormalAbility.InitializeAbility();
        }
        if(enemyEspecialAbility != null)
        {
            enemyEspecialAbility.InitializeAbility();
        }
        SetParentVariables();
        InitializeCooldowns();
    }
    private void SetParentVariables()
    {
        this.objectName = enemyName;
        this.objectIndex = enemyIndex;
        this.objectDamage = enemyMeleeDamage;
        this.objectMovementSpeed = enemyMoveSpeed;
        this.objectRange = enemyRange;
        this.objectMeleeRange = enemyMeleeRange;
        if(enemyMeleeAbility != null)
        {
            this.objectMeleeAbility = enemyMeleeAbility;
        }
        if(enemyNormalAbility != null)
        {
            this.objectBasicAbility = enemyNormalAbility;
        }
        if (enemyEspecialAbility != null)
        {
            this.objectSpecialAbility = enemyEspecialAbility;
        }
    }
    public override void InitializeCooldowns()
    {
        if(enemyMeleeSpeed > 0)
        {
            this.objectMeleeCooldown = enemyMeleeSpeed;
        }
        if(enemyBasicSpeed > 0)
        {
            this.objectBasicCooldown = enemyBasicSpeed;
        }
        if(enemySpecialSpeed > 0)
        {
            this.objectSpecialCooldown = enemySpecialSpeed;
        }
    }
}
