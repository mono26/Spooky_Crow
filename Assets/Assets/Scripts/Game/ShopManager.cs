using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField]
    private PlantBluePrint[] plantPrefabs;

    public void PurchasePlant1()
    {
        GameManager.Instance.SetPlantToBuild(plantPrefabs[0]);
    }
    public void PurchasePlant2()
    {
        GameManager.Instance.SetPlantToBuild(plantPrefabs[1]);
    }
    public void PurchasePlant3()
    {
        GameManager.Instance.SetPlantToBuild(plantPrefabs[2]);
    }
}
