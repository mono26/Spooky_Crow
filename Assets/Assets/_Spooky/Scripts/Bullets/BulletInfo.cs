using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AIComponents/BulletInfo")]
[System.Serializable]
public class BulletInfo : Info {
    public enum BulletType
    {
        FISICAL, MAGIC, HARD
    }
    public BulletType my_Type;
    public string bulletName;
    public int bulletIndex;   //Variable para almacenar la informacion de los pools.
    public float bulletSpeed;
    public int bulletDamage;

    public BulletInfo()
    {
        objectName = bulletName;
        objectIndex = bulletIndex;
        objectSpeed = bulletSpeed;
    }
}
