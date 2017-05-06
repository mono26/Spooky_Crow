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

    public GameObject[] enemyPrefabs;       //Informacion necesaria para que el GameManager llene las listas de los pools
    public Transform[] spawnPoints;     //Array donde se almacenan los spawnPoints.
    public Transform[] runAwayPoints;   //Array para la informacion de los puntos de escape para los ladrones.

    [SerializeField]
    private PlantBluePrint plantToBuild;       //La torre que se va a construir luego de dar click a uno de los botones.
    public int rondas;

    public GameObject player;   //Referencia al jugador para que puedan acceder a los targets.
    public GameObject house;    ////Referencia a la casa para que puedan acceder a los targets.

    public int money;       //El GameManager es el que va a tener la informacion del dinero y lo mismo de la vida
    public int health;

    public PlantPoint[] towerPoints;
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

        //Para buscar todos los runaway y spawn points
        var sp = GameObject.FindGameObjectsWithTag("SpawnPoint");
        var rp = GameObject.FindGameObjectsWithTag("RunAwayPoint");
        spawnPoints = new Transform[sp.Length];
        runAwayPoints = new Transform[rp.Length];
        for (int i = 0; i < sp.Length; i++)
        {
            spawnPoints[i] = sp[i].GetComponent<Transform>();
        }
        for (int i = 0; i < rp.Length; i++)
        {
            runAwayPoints[i] = rp[i].GetComponent<Transform>();
        }
    }
    void Start()
    {

    }
    public void BuildPlantOn()
    {
        if(money < plantToBuild.price)
        {
            return;
        }
    }
    public void SetPlantToBuild(PlantBluePrint plant)
    {
        plantToBuild = plant;
    }
}
