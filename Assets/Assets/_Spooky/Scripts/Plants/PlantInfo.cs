using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AIComponents/PlantInfo")]
[System.Serializable]
public class PlantInfo : Info {
    public enum PlantType
    {
        FISICAL, MAGIC, HARD
    }
    public PlantType my_Type;
    public string plantName;
    public int plantIndex;   //Variable para almacenar la informacion de los pools.

    public float plantAtackSpeed;

    public AIAbility plantAbility1;
    public AIAbility plantAbility2;

    public float meleeRange;
    public float shootRange;

    public PlantInfo()
    {
        objectName = plantName;
        objectIndex = plantIndex;
        objectSpeed = plantAtackSpeed;
    }
}
