using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Singleton part
    private static GameManager instance;
    public static GameManager Instance
    {
        get { return instance; }
    }

    public GameObject[] enemyPrefabs;
    public Transform[] spawnPoints;     //Array donde se almacenan los spawnPoints.
    public int rondas;

    public GameObject player;   //Referencia al jugador para que puedan acceder a los targets.
    public GameObject house;    ////Referencia a la casa para que puedan acceder a los targets.

    void Awake()
    {
        if (instance == null)
        {
            instance = this;    //Parte del singleton en donde se asigna la unica instancia de la clase
        }
        else
            Destroy(gameObject);

        //Si ambas referencias no han sido asignadas por en el editor las debe de encontrar.
        if (player == null || house == null)
        {
            player = GameObject.FindGameObjectWithTag("Crow");
            house = GameObject.FindGameObjectWithTag("House");
        }
    }
    void Start()
    {

    }

}
