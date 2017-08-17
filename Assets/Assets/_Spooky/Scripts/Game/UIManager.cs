using System.Collections;
using System.Collections.Generic;
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

    //Esta parte del script esta dedicada a las funiones de la UI de las plantas. Tanto para la UI de la
    //planta como el UI de construccion.
    public void PurchasePlant(PlantBluePrint bluePrint)
    {
        if (GameManager.Instance.playerMoney > bluePrint.price)
        {
            currentPlantPoint.BuildPlant(bluePrint);
            HideBuildUI();
        }
        else return;
    }
    public void Upgrade()
    {
        if (GameManager.Instance.playerMoney > currentPlantPoint.my_PlantBluePrint.upgradePrice)
            currentPlantPoint.UpgradePlant();
        else return;
    }
    public void Sell()
    {
        currentPlantPoint.SellPlant();
        GameManager.Instance.DeselectPlantPoint();
    }
    public void SetPlantPoint(PlantPoint plantPoint)
    {
        currentPlantPoint = plantPoint;
        plantUI.position = currentPlantPoint.transform.position;
        plantCanvas.SetActive(true);
    }
    public void SetBuildPoint(PlantPoint plantPoint)
    {
        currentPlantPoint = plantPoint;
        buildUI.position = currentPlantPoint.transform.position;
        buildCanvas.SetActive(true);
    }
    public void HideBuildUI()
    {
        currentPlantPoint = null;
        buildCanvas.SetActive(false);
    }
    public void HidePlantUI()
    {
        currentPlantPoint = null;
        plantCanvas.SetActive(false);
    }

    //Parte del script que manejara las funciones de los botones para el manejo de los niveles.
    public void Retry()
    {
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        sceneFader.FadeTo("menu");
    }
    public void Continue()
    {
        /*PlayerPrefs.SetInt("levelReached", levelToUnlock);
        sceneFader.FadeTo(nextLevel);*/
    }
}
