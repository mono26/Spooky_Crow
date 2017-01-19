using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public Transform[] spawnPoints;     //Array donde se almacenan los spawnPoints.
    public int rondas;

    private int numberOfEnemies;

    Dictionary<string,GameObject> enemigos = new Dictionary<string,GameObject>();

    void Awake()
    {
        foreach (GameObject enemy in enemyPrefabs)      //Llenar el diccionario del nivel con los enemigos del nivel.
        {
            enemigos[enemy.name] = enemy;
        }
    }

    void Start()
    {
        Spawn();
    }

    void Spawn()        //Metodo para invocar los enemigos
    {
        if (rondas % 5 == 0)
        {
            foreach (Transform spawn in spawnPoints)
            {
                Instantiate(enemigos["Boss"],spawn.position,spawn.rotation);
            }
        }
    }

}
