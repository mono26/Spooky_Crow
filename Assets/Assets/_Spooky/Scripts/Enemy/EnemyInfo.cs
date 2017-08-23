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
    public int enemyIndex;   //Variable para almacenar la informacion de los pools.

    //Variables para los stats
    public float enemyMoveSpeed;       //Esta es la velocidad que tendra el navMesh
    public float enemyAtackSpeed;

    public int enemyHealthPoints;        //Los puntos de vida que tendra en controller del enemigo
    public int enemyReaward;

    //Para las abilidades de cada enemigo
    public AIAbility enemyAbility1;
    public AIAbility enemyAbility2;

    //Vriables que contienen los rangos necesarios
    public float enemyRange;

    public override void InitializeInfo()
    {
        objectName = enemyName;
        objectIndex = enemyIndex;
        objectSpeed = enemyMoveSpeed;
        objectRange = enemyRange;
    }
}
