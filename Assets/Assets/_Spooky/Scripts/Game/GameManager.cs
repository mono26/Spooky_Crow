using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Analytics;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    //Singleton part
    private static GameManager instance;
    public static GameManager Instance
    {
        get { return instance; }
    }
    //Puntos inportantes en el juego
    public Transform[] houseStealPoints;    ////Referencia a la casa para que puedan acceder a los targets.
    public Transform[] spawnPoints;     //Array donde se almacenan los spawnPoints.
    public Transform[] runAwayPoints;   //Array para la informacion de los puntos de escape para los ladrones.
    public IndicatorManager indicatorManager;

    //Referencia al jugador
    public GameObject playerSpooky;   //Referencia al jugador para que puedan acceder a los targets.

    public Image gameHealthBar;
    public Text gameMoneyText;
    public int playerMoney = 400;       //El GameManager es el que va a tener la informacion del dinero y lo mismo de la vida
    public float currentHouseHealth = 800;
    public float gameTime;

    //Variables privadas para el inicio del nivel
    [SerializeField]
    private float maxHouseHelath = 800;

    //Variables relacionadas con el fin del nivel
    public static bool GameIsOver;
    public GameObject gameOverUI;
    public GameObject completeLvlUI;

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
        LookForGameDependencies();
        indicatorManager = GetComponent<IndicatorManager>();
    }
    void Start()
    {
        gameHealthBar.fillAmount = currentHouseHealth/maxHouseHelath;
        gameMoneyText.text = "$:" + playerMoney; 
    }
    private void Update()
    {

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
            playerSpooky = GameObject.FindGameObjectWithTag("Spooky");
            var hPoints = GameObject.FindGameObjectsWithTag("StealPoint");
            houseStealPoints = new Transform[hPoints.Length];
            for (int i = 0; i < hPoints.Length; i++)
            {
                houseStealPoints[i] = hPoints[i].GetComponent<Transform>();
            }
        }
    }
    private void LookForGameDependencies()
    {
        gameMoneyText = GameObject.FindGameObjectWithTag("GameMoneyText").GetComponent<Text>();
        gameHealthBar = GameObject.FindGameObjectWithTag("GameHealthBar").GetComponent<Image>();
    }

    //Metdos publicos
    //Metodo para cuadno eljugador haga click en el suelo se deseleccione el Build o PlantUI
    public void ClickGroundToDeselect()
    {
        Debug.Log("Estoy haciendo el click");
        if (UIManager.Instance.currentPlantPoint != null)
        {
            UIManager.Instance.DeselectBuildPoint();
            UIManager.Instance.DeselectPlantPoint();
        }
        else return;
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
        gameOverUI.SetActive(true);
    }
    public void WinLevel()
    {
        Analytics.CustomEvent("LevelandTime", new Dictionary<string, object>
        {
            {"health", currentHouseHealth },
            {"time", gameTime}
        });
        GameIsOver = true;
        completeLvlUI.SetActive(true);
    }
    public void PauseGame()
    {
        //codigo para pausar el juego.
    }
}
