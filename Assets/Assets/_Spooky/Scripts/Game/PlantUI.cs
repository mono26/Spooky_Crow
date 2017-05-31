using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantUI : MonoBehaviour
{
    public PlantPoint my_TargetPlantPoint;
    public GameObject my_UI;

    public void SetPlantPoint(PlantPoint plantPoint)
    {
        my_TargetPlantPoint = plantPoint;
        transform.position = plantPoint.transform.position;
        my_UI.SetActive(true);
    }
    public void HidePlantUI()
    {
        my_UI.SetActive(false);
    }
    public void Sell()
    {
        my_TargetPlantPoint.SellPlant();
        GameManager.Instance.DeselectPlantPoint();
    }
}
