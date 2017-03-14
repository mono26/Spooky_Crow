using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyInfo : MonoBehaviour {
    public enum EnemyType
    {
        ATACKER, STEALER, BOSS
    }

    public EnemyType my_Type;
    public string name;
    public int index;   //Variable para almacenar la informacion de los pools.

    [SerializeField]
    private int health;
    [SerializeField]
    private int speed;
}
