using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlantPoint : MonoBehaviour
{
    public PlantBluePrint my_PlantBluePrint;
    public GameObject my_Plant;
    public SpriteRenderer my_Rederer;

    [SerializeField]
    private Color noMoneyColor;
    [SerializeField]
    private Color yesMoneyColor;
    [SerializeField]
    private Color startColor;

    void Start()
    {
    }
    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (my_Plant && my_PlantBluePrint)       //Si ya hay una planta y un blueprint se selecciona el nodo
        {
            Debug.Log("Ya hay una planta");
            GameManager.Instance.SelectPlantPoint(this);
            //Ejecutar codigo para poder subir de nivel o vender la torre
        }
        if (!my_Plant && !my_PlantBluePrint)
        {
            Debug.Log("No hay planta");
            GameManager.Instance.SelectBuildPoint(this);
            //Ejecutar codigo para que salga el canvas con los botones.
        }
    }
    public void BuildPlant(PlantBluePrint blueprint)       //Luego de que se tenga una planta seleccionada cuando se escoja un nodo se construira ahi
    {
        GameObject plant = PoolsManagerPlants.Instance.GetPlant(blueprint.plant.my_PlantInfo.plantIndex);
        plant.transform.position = transform.position;
        my_Plant = plant;
        my_PlantBluePrint = blueprint;
        GameManager.Instance.playerMoney -= blueprint.price;
        GameManager.Instance.gameMoneyText.text = "$:" + GameManager.Instance.playerMoney;
    }
    public void SellPlant()
    {
        GameManager.Instance.playerMoney += my_PlantBluePrint.price;
        my_PlantBluePrint = null;
        PoolsManagerPlants.Instance.ReleasePlant(my_Plant);
        my_Plant = null;
        GameManager.Instance.gameMoneyText.text = "$:" + GameManager.Instance.playerMoney;
    }
    public void UpgradePlant()
    {
        GameManager.Instance.playerMoney -= my_PlantBluePrint.upgradePrice;
        PoolsManagerPlants.Instance.ReleasePlant(my_Plant);
        my_Plant = PoolsManagerPlants.Instance.GetPlant(my_PlantBluePrint.upgradePlant.my_PlantInfo.plantIndex);
    }
    public void DestroyPlant()
    {
        my_PlantBluePrint = null;
        PoolsManagerPlants.Instance.ReleasePlant(my_Plant);
        my_Plant = null;
    }
}
