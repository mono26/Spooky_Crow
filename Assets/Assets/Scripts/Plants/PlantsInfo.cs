using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AIComponents/PlantInfo")]
[System.Serializable]
public class PlantInfo : ScriptableObject {
    public enum PlantType
    {
        FISICAL, MAGIC, HARD
    }
    public PlantType my_Type;
    public string towerName;
    public int index;   //Variable para almacenar la informacion de los pools.
    public float speed;
    public int damage;
    public int price;
    public AIAbility ability1;
    public AIAbility ability2;
    public float cdTimer1;
    public float cdTimer2;
    public float meleeRange;
    public float shootRange;
}
