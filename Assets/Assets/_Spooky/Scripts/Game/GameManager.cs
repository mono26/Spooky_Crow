using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Analytics;

public class GameManager : MonoBehaviour
{
    //Singleton part
    private static GameManager instance;
    public static GameManager Instance
    {
        get { return instance; }
    }

    public Transform[] spawnPoints;     //Array donde se almacenan los spawnPoints.
    public Transform[] runAwayPoints;   //Array para la informacion de los puntos de escape para los ladrones.

    public PlantPoint selectedPlantPoint;

    public GameObject playerSpooky;   //Referencia al jugador para que puedan acceder a los targets.
    public GameObject[] houseStealPoints;    ////Referencia a la casa para que puedan acceder a los targets.

    public Image gameHealthBar;
    public Text gameMoneyText;
    public int playerMoney = 400;       //El GameManager es el que va a tener la informacion del dinero y lo mismo de la vida
    public float currentHouseHealth = 800;
    public float gameTime;
    [SerializeField]
    private float maxHouseHelath = 800;

    public static bool GameIsOver;

    public GameObject my_GameOverUI;
    public GameObject my_CompleteLevelUI;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;    //Parte del singleton en donde se asigna la unica instancia de la clase
        }
        else Destroy(gameObject);

        //Si ambas referencias no han sido asignadas por en el editor las debe de encontrar.
        LookForPlayerAndHousePoints();
        //Para buscar todos los runaway y spawn points
        LookForRunAwayPoints();
        LookForSpawnPoints();
    }
    void Start()
    {
        gameHealthBar.fillAmount = currentHouseHealth/maxHouseHelath;
        //gameMoneyText.text = "$:" + playerMoney; 
    }

    //Metodos privados 
    private void LookForSpawnPoints()
    {
        var sp = GameObject.FindGameObjectsWithTag("SpawnPoint");
        spawnPoints = new Transform[sp.Length];
        for (int i = 0; i < sp.Length; i++)
        {
            spawnPoints[i] = sp[i].GetComponent<Transform>();
        }
    }
    private void LookForRunAwayPoints()
    {
        var rp = GameObject.FindGameObjectsWithTag("RunAwayPoint");
        runAwayPoints = new Transform[rp.Length];
        for (int i = 0; i < rp.Length; i++)
        {
            runAwayPoints[i] = rp[i].GetComponent<Transform>();
        }
    }
    private void LookForPlayerAndHousePoints()
    {
        if (playerSpooky == null || houseStealPoints == null)
        {
            playerSpooky = GameObject.FindGameObjectWithTag("Crow");
            houseStealPoints = GameObject.FindGameObjectsWithTag("StealPoint");
        }
    }
    //Metodos publicos
    public void SelectPlantPoint(PlantPoint plantPoint)     //Metodo que se llamara cada vez que el jugador haga click sobre un plant point.
    {
        if(selectedPlantPoint == plantPoint)
        {
            DeselectPlantPoint();
            return;
        }
        selectedPlantPoint = plantPoint;
        UIManager.Instance.SetPlantPoint(selectedPlantPoint);
    }
    public void SelectBuildPoint(PlantPoint plantPoint)     //Metodo que se llamara cada vez que el jugador haga click sobre un plant point.
    {
        if (selectedPlantPoint == plantPoint)
        {
            DeselectBuildPoint();
            return;
        }
        selectedPlantPoint = plantPoint;
        UIManager.Instance.SetBuildPoint(selectedPlantPoint);
    }
    public void DeselectPlantPoint()        //Function for deselection the plantpoint
    {
        selectedPlantPoint = null;
        UIManager.Instance.HidePlantUI();
    }
    public void DeselectBuildPoint()        //Function for deselection the plantpoint
    {
        selectedPlantPoint = null;
        UIManager.Instance.HideBuildUI();
    }
    public void LoseHealth(int stole)
    {
        currentHouseHealth -= stole;
        gameHealthBar.fillAmount = currentHouseHealth/maxHouseHelath;
        if (currentHouseHealth <= 0)
        {
            //GameOver Code
            GameOver();
        }
    }
    public void GiveMoney(int reward)
    {
        playerMoney += reward;
        gameMoneyText.text = "$:" + playerMoney;
    }
    void GameOver()
    {
        Analytics.CustomEvent("LevelandWave", new Dictionary<string, object>
        {
            {"health", currentHouseHealth },
            {"lvl", PlayerPrefs.GetInt("levelReached", 1)}
        });
        GameIsOver = true;
        my_GameOverUI.SetActive(true);
    }
    public void WinLevel()
    {
        Analytics.CustomEvent("LevelandTime", new Dictionary<string, object>
        {
            {"health", currentHouseHealth },
            {"time", gameTime}
        });
        GameIsOver = true;
        my_CompleteLevelUI.SetActive(true);
    }
    public void PauseGame()
    {
        //codigo para pausar el juego.
    }
}
