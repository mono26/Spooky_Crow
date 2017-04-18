using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AIComponents/BulletInfo")]
[System.Serializable]
public class TowerInfo : ScriptableObject {
    public enum TowerType
    {
        FISICAL, MAGIC, HARD
    }
    public TowerType my_Type;
    public string towerName;
    public int index;   //Variable para almacenar la informacion de los pools.
    public float speed;
    public int damage;
    public int price;
}
