using UnityEngine;
using UnityEngine.EventSystems;

public class PlantPoint : MonoBehaviour
{
    public PlantBluePrint plantBluePrint;
    public GameObject currentPlant;

    void Start()
    {
    }
    public void OnClicked()
    {
        //Si ya hay una planta y un blueprint se selecciona el nodo
        if (currentPlant && plantBluePrint)
        {
            UIManager.Instance.SelectPlantPoint(this);
            //Ejecutar codigo para poder subir de nivel o vender la torre
        }
        if (!currentPlant && !plantBluePrint)
        {
            UIManager.Instance.SelectBuildPoint(this);
            //Ejecutar codigo para que salga el canvas con los botones.
        }
    }
    public void BuildPlant(PlantBluePrint blueprint)       //Luego de que se tenga una planta seleccionada cuando se escoja un nodo se construira ahi
    {
        GameObject plant = PoolsManagerPlants.Instance.GetPlant(blueprint.plant.plantInfo.plantIndex);
        plant.transform.position = transform.position;
        currentPlant = plant;
        plantBluePrint = blueprint;
        GameManager.Instance.playerMoney -= blueprint.price;
        //GameManager.Instance.gameMoneyText.text = "$:" + GameManager.Instance.playerMoney;
    }
    public void SellPlant()
    {
        GameManager.Instance.playerMoney += plantBluePrint.price;
        plantBluePrint = null;
        PoolsManagerPlants.Instance.ReleasePlant(currentPlant);
        currentPlant = null;
        GameManager.Instance.gameMoneyText.text = "$:" + GameManager.Instance.playerMoney;
        UIManager.Instance.DeselectPlantPoint();
    }
    public void UpgradePlant()
    {
        GameManager.Instance.playerMoney -= plantBluePrint.upgradePrice;
        PoolsManagerPlants.Instance.ReleasePlant(currentPlant);
        currentPlant = PoolsManagerPlants.Instance.GetPlant(plantBluePrint.upgradePlant.plantInfo.plantIndex);
        currentPlant.transform.position = transform.position;
        UIManager.Instance.DeselectPlantPoint();
    }
    public void DestroyPlant()
    {
        plantBluePrint = null;
        PoolsManagerPlants.Instance.ReleasePlant(currentPlant);
        currentPlant = null;
    }
}
