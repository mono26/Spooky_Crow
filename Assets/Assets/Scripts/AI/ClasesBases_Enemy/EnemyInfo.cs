using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AIComponents/Enemy Info")]
[System.Serializable]
public class EnemyInfo : ScriptableObject {
    public enum EnemyType
    {
        ATACKER, STEALER, BOSS
    }
    public EnemyType my_Type;
    public string enemyName;
    public int index;   //Variable para almacenar la informacion de los pools.
    public float speed;
    public int health;
    public float lookRange;
    public float meleeRange;
    public float shootRange;
}
