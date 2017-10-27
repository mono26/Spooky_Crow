using UnityEngine;
using UnityEngine.EventSystems;

public class PlantPoint : MonoBehaviour
{
    public PlantBluePrint plantBluePrint;
    public AIPlantController currentPlant;
    private SoundPlayer plantPointPlayer; 

    void Start()
    {
        plantPointPlayer = this.GetComponent<SoundPlayer>();
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
        currentPlant = plant.GetComponent<AIPlantController>();
        plantBluePrint = blueprint;
        GameManager.Instance.TakeMoney(blueprint.price);
        plantPointPlayer.PlayClip();
    }
    public void SellPlant()
    {
        GameManager.Instance.GiveMoney(currentPlant.GetComponent<AIPlantController>().plantInfo.plantReward);
        plantBluePrint = null;
        PoolsManagerPlants.Instance.ReleasePlant(currentPlant.gameObject);
        currentPlant = null;
        UIManager.Instance.DeselectPlantPoint();
    }
    public void UpgradePlant()
    {
        GameManager.Instance.TakeMoney(plantBluePrint.upgradePrice);
        PoolsManagerPlants.Instance.ReleasePlant(currentPlant.gameObject);
        currentPlant = PoolsManagerPlants.Instance.GetPlant(plantBluePrint.upgradePlant.plantInfo.plantIndex).GetComponent<AIPlantController>();
        currentPlant.transform.position = transform.position;
        UIManager.Instance.DeselectPlantPoint();
    }
    public void DestroyPlant()
    {
        plantBluePrint = null;
        currentPlant = null;
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("EnemyMeleeCollider") && other.gameObject.activeInHierarchy)
        {
            Debug.Log("Recibiendo daño");
            currentPlant.TakeDamage(other.GetComponentInParent<AIEnemyController>().enemyInfo.objectDamage);
            if (currentPlant.GetComponent<AIPlantController>().plantHealth <= 0)
            {
                DestroyPlant();
            }
        }
    }
}
