using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    atacker, stealer, boss
}
public class EnemyInfo : MonoBehaviour {

    public EnemyType my_Type;
    private int health;

}
