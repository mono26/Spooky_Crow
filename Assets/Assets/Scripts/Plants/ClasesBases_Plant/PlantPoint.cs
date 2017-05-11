using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantPoint : MonoBehaviour
{
    public GameObject plant;
    public Renderer m_Renderer;
    public bool towerON;

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
        if (GameManager.Instance.plantToBuild == null)
        {
            return;
        }
        if (!towerON && plant == null)
        {
            if (GameManager.Instance.money < GameManager.Instance.plantToBuild.price)
            {
                return;
            }
            else
            {
                Debug.Log("No hay planta");
                GameManager.Instance.money -= GameManager.Instance.plantToBuild.price;
                GameManager.Instance.BuildPlantOn(this.transform);
                //Ejecutar codigo para comprar la torre
            }
        }
        if(towerON && plant != null)
        {
            Debug.Log("Ya hay una planta");
            //Ejecutar codigo para poder subir de nivel o vender la torre
        }
    }
    private void OnMouseEnter()
    {
        if (GameManager.Instance.plantToBuild == null)
        {
            return;
        }
        //Si hay una planta que va a ser construida el punto en donde se puede ver la planta 
        if (GameManager.Instance.money < GameManager.Instance.plantToBuild.price)
        {
            m_Renderer.material.color = noMoneyColor;
        }
        else if (GameManager.Instance.money >= GameManager.Instance.plantToBuild.price)
        {
            m_Renderer.material.color = yesMoneyColor;
        }

    }
    private void OnMouseExit()
    {
        m_Renderer.material.color = startColor;
    }
    public void SellPlant()
    {
        GameManager.Instance.money += plant.GetComponent<AIPlantController>().my_PlantInfo.price;
    }
    public void UpgradePlant()
    {

    }
}
