using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantPoint : MonoBehaviour
{
    public GameObject plant;
    public bool towerON;

    public void OnMouseDown()
    {
        if(!towerON && plant == null)
        {
            //Ejecutar codigo para comprar la torre
        }
        if(towerON && plant != null)
        {
            //Ejecutar codigo para poder subir de nivel o vender la torre
        }
    }
    private void BuyTower()
    {

    }
    private void SellTower()
    {

    }
    private void ActivateBuyMenu()
    {

    }
}
