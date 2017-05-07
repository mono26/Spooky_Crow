using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantPoint : MonoBehaviour
{
    public GameObject plant;
    public Renderer m_Renderer;
    public bool towerON;

    [SerializeField]
    private Color hoverColor;
    [SerializeField]
    private Color startColor;

    void Start()
    {

    }
    private void OnMouseDown()
    {
        if (!towerON && plant == null)
        {
            Debug.Log("No hay planta");
            GameObject plant = GameManager.Instance.GetPlantToBuild();
            plant = Instantiate(plant, transform.position, Quaternion.identity);
            //Ejecutar codigo para comprar la torre
        }
        if(towerON && plant != null)
        {
            Debug.Log("Ya hay una planta");
            //Ejecutar codigo para poder subir de nivel o vender la torre
        }
    }
    private void OnMouseEnter()
    {
        m_Renderer.material.color = hoverColor;
    }
    private void OnMouseExit()
    {
        m_Renderer.material.color = startColor;
    }
}
