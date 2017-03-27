using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AIComponents/BulletInfo")]
[System.Serializable]
public class BulletInfo : ScriptableObject {
    public enum BulletType
    {
        FISICAL, MAGIC, HARD
    }
    public BulletType my_Type;
    public string bulletName;
    public int index;   //Variable para almacenar la informacion de los pools.
    public float speed;
}
