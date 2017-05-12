using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlantPoint : MonoBehaviour
{
    public PlantBluePrint my_PlantBluePrint;
    public GameObject my_Plant;
    public Renderer my_Rederer;

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
            if (GameManager.Instance.money < GameManager.Instance.plantToBuild.price)
            {
                return;
            }
            else
            {
                Debug.Log("No hay planta");
                GameManager.Instance.money -= GameManager.Instance.plantToBuild.price;
                GameManager.Instance.BuildPlantOn(this);
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
        if (GameManager.Instance.money < GameManager.Instance.plantToBuild.price)
        {
            my_Rederer.material.color = noMoneyColor;
        }
        else if (GameManager.Instance.money >= GameManager.Instance.plantToBuild.price)
        {
            my_Rederer.material.color = yesMoneyColor;
        }

    }
    private void OnMouseExit()
    {
        my_Rederer.material.color = startColor;
    }
    public void SellPlant()
    {
        GameManager.Instance.money += my_PlantBluePrint.price;
        my_PlantBluePrint = null;
        PoolsManagerPlants.Instance.ReleaseObject(my_Plant);
        my_Plant = null;
    }
    public void UpgradePlant()
    {

    }
}
