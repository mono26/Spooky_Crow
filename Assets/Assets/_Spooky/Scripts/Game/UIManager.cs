using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    //Singleton part
    private static UIManager instance;
    public static UIManager Instance
    {
        get { return instance; }
    }

    //Esta es informacion para toda la programacion del plantpoint y sus funciones
    public Transform buildUI;
    public Transform plantUI;
    public GameObject buildCanvas;
    public GameObject plantCanvas;
    public PlantPoint currentPlantPoint;

    public SceneFader sceneFader;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;    //Parte del singleton en donde se asigna la unica instancia de la clase
        }
        else
            Destroy(gameObject);
    }

    //Metodos para el manejo de los plantpoints y la UI
    public void SelectPlantPoint(PlantPoint plantPoint)     //Metodo que se llamara cada vez que el jugador haga click sobre un plant point.
    {
        if (currentPlantPoint == plantPoint)
        {
            DeselectPlantPoint();
            return;
        }
        currentPlantPoint = plantPoint;
        UIManager.Instance.SetPlantPoint(currentPlantPoint);
    }
    public void SelectBuildPoint(PlantPoint plantPoint)     //Metodo que se llamara cada vez que el jugador haga click sobre un plant point.
    {
        if (currentPlantPoint == plantPoint)
        {
            DeselectBuildPoint();
            return;
        }
        currentPlantPoint = plantPoint;
        SetBuildPoint(currentPlantPoint);
    }
    public void DeselectPlantPoint()        //Function for deselection the plantpoint
    {
        currentPlantPoint = null;
        HidePlantUI();
    }
    public void DeselectBuildPoint()        //Function for deselection the plantpoint
    {
        currentPlantPoint = null;
        HideBuildUI();
    }
    public void SetPlantPoint(PlantPoint plantPoint)
    {
        //Si el plantPoint tiene una planta se activa el plantpointUI con la informacion de la planta.
        currentPlantPoint = plantPoint;
        plantUI.position = currentPlantPoint.transform.position;
        plantCanvas.SetActive(true);
    }
    public void SetBuildPoint(PlantPoint plantPoint)
    {
        //Cuadno el plant poin esta vacio para sacar el buildUI
        currentPlantPoint = plantPoint;
        buildUI.position = currentPlantPoint.transform.position;
        buildCanvas.SetActive(true);
    }
    public void HideBuildUI()
    {
        buildCanvas.SetActive(false);
    }
    public void HidePlantUI()
    {
        plantCanvas.SetActive(false);
    }

    //Esta parte del script esta dedicada a las funiones de la UI de las plantas. Tanto para la UI de la
    //planta como el UI de construccion.
    public void PurchasePlant(PlantBluePrint bluePrint)
    {
        if (GameManager.Instance.playerMoney >= bluePrint.price)
        {
            currentPlantPoint.BuildPlant(bluePrint);
            DeselectBuildPoint();
        }
        else return;
    }
    public void Upgrade()
    {
        if (GameManager.Instance.playerMoney > currentPlantPoint.plantBluePrint.upgradePrice)
            currentPlantPoint.UpgradePlant();
        else return;
    }
    public void Sell()
    {
        currentPlantPoint.SellPlant();
        DeselectPlantPoint();
    }

    //Parte del script que manejara las funciones de los botones para el manejo de los niveles.
    public void Retry()
    {
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        sceneFader.FadeTo("MainMenu");
    }
    public void Continue()
    {
        /*PlayerPrefs.SetInt("levelReached", levelToUnlock);
        sceneFader.FadeTo(nextLevel);*/
    }
}
