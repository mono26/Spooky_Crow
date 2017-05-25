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
        if (GameManager.Instance.plantToBuild == null)
        {
            return;
        }
        if (!my_Plant && !my_PlantBluePrint)
        {
            if (GameManager.Instance.playerMoney < GameManager.Instance.plantToBuild.price)
            {
                return;
            }
            else
            {
                Debug.Log("No hay planta");
                BuildPlant(GameManager.Instance.GetPlantToBuild());
                //Ejecutar codigo para comprar la torre
            }
        }
    }
    private void OnMouseEnter()
    {
        if(EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        Debug.Log("MouseEnter");
        if (GameManager.Instance.plantToBuild == null)
        {
            return;
        }
        //Si hay una planta que va a ser construida el punto en donde se puede ver la planta 
        if (GameManager.Instance.playerMoney < GameManager.Instance.plantToBuild.price)
        {
            //my_Rederer.color = noMoneyColor;
        }
        else if (GameManager.Instance.playerMoney >= GameManager.Instance.plantToBuild.price)
        {
            //my_Rederer.color = yesMoneyColor;
        }

    }
    private void OnMouseExit()
    {
        //my_Rederer.color = startColor;
    }
    public void BuildPlant(PlantBluePrint blueprint)       //Luego de que se tenga una planta seleccionada cuando se escoja un nodo se construira ahi
    {
        if (GameManager.Instance.playerMoney < blueprint.price)
        {
            return;
        }
        GameObject plant = PoolsManagerPlants.Instance.GetObject(blueprint.plant.my_PlantInfo.index);
        plant.transform.position = transform.position;
        my_Plant = plant;
        my_PlantBluePrint = blueprint;
        GameManager.Instance.playerMoney -= blueprint.price;
        GameManager.Instance.my_MoneyText.text = "$:" + GameManager.Instance.playerMoney;
    }
    public void SellPlant()
    {
        GameManager.Instance.playerMoney += my_PlantBluePrint.price;
        my_PlantBluePrint = null;
        PoolsManagerPlants.Instance.ReleaseObject(my_Plant);
        my_Plant = null;
        GameManager.Instance.my_MoneyText.text = "$:" + GameManager.Instance.playerMoney;
    }
    public void UpgradePlant()
    {
        if (GameManager.Instance.playerMoney < my_PlantBluePrint.upgradePrice)
        {
            Debug.Log("Not enough money to upgrade that!");
            return;
        }

        GameManager.Instance.playerMoney -= my_PlantBluePrint.upgradePrice;
        PoolsManagerPlants.Instance.ReleaseObject(my_Plant);
        my_Plant = PoolsManagerPlants.Instance.GetObject(my_PlantBluePrint.upgradePlant.my_PlantInfo.index);

        
    }
}
