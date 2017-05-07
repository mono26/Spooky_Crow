using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField]
    private PlantBluePrint[] plantBluePrint;

    public void PurchasePlant1()
    {
        GameManager.Instance.SetPlantToBuild(plantBluePrint[0]);
    }
    public void PurchasePlant2()
    {
        GameManager.Instance.SetPlantToBuild(plantBluePrint[1]);
    }
    public void PurchasePlant3()
    {
        GameManager.Instance.SetPlantToBuild(plantBluePrint[2]);
    }
}
