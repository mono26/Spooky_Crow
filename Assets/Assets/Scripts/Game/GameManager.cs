using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public PlantBluePrint plantToBuild;       //La torre que se va a construir luego de dar click a uno de los botones.
    public PlantPoint selectedPlantPoint;
    public PlantUI my_PlantUI;
    public int rondas;

    public GameObject player;   //Referencia al jugador para que puedan acceder a los targets.
    public GameObject house;    ////Referencia a la casa para que puedan acceder a los targets.

    public Slider my_HealthBar;
    public Text my_MoneyText;
    public int playerMoney = 400;       //El GameManager es el que va a tener la informacion del dinero y lo mismo de la vida
    public int houseHealth = 800;

    public bool selectPlayer;

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
        my_HealthBar.maxValue = houseHealth;
        my_HealthBar.value = houseHealth;
        my_MoneyText.text = "$:" + playerMoney; 
    }
    public void SetPlantToBuild(PlantBluePrint plant)       //Metodo que usa el shopmanager para cambiar la planta que se va a construir
    {
        //Solo debe de haber uno al tiempo, no puede existir blueprint si se tiene un plantpoint seleccionado
        plantToBuild = plant;
        DeselectPlantPoint();
    }
    public PlantBluePrint GetPlantToBuild()     //Metodo que se comunica con el pool para sacar la planta y retornarla en donde se uso.
    {
        return plantToBuild;
    }
    public void SelectPlantPoint(PlantPoint plantPoint)     //Metodo que se llamara cada vez que el jugador haga click sobre un plant point.
    {
        if(selectedPlantPoint == plantPoint)
        {
            DeselectPlantPoint();
            return;
        }
        selectedPlantPoint = plantPoint;
        plantToBuild = null;
        DeselectPlayer();
        my_PlantUI.SetPlantPoint(plantPoint);
    }
    public void SelectPlayer()
    {
        if(selectPlayer == true)        //Metodo que se llamara para poder mover el jugador con el click
        {
            return;
        }
        plantToBuild = null;
        selectPlayer = true;
        DeselectPlantPoint();
    }
    public void DeselectPlantPoint()        //Function for deselection the plantpoint
    {
        selectedPlantPoint = null;
        my_PlantUI.HidePlantUI();
    }
    public void DeselectPlayer()        //Function for deselection the plantpoint
    {
        selectPlayer = false;
    }
    public void Steal(int stole)
    {
        houseHealth -= stole;
        my_HealthBar.value = houseHealth;
        if (houseHealth <= 0)
        {
            //GameOver Code
        }
    }
}
