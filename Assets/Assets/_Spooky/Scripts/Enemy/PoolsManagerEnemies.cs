using System.Collections.Generic;
using UnityEngine;

public class PoolsManagerEnemies : MonoBehaviour {
    //Singleton part
    private static PoolsManagerEnemies instance;
    public static PoolsManagerEnemies Instance
    {
        get { return instance; }
    }

    //PoolCode
    public List<GameObject> enemyPrefabs;  //Lista con los prefabs de los pools
    public List<List<GameObject>> pools = new List<List<GameObject>>();    //Lista que contiene los pools, lista de listas.
    public List<int> listOfSizes;     //Lista con el tamaño de cada una de las otras listas, debe de estar ordenada igual que los prefabs

    [SerializeField]
    Transform poolsPosition;

    private void Awake()
    {
        //This is for making this the only instance in the game. Singleton part.
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(gameObject);

        //Here we read the amount of enemy prefabs are assigned on the ínspector. Then por each one of them
        //we tell the listOfSizes the size of it and set the value of the amount of enemies we want to sapwn at start.
        for (int enemyPrefab = 0; enemyPrefab < enemyPrefabs.Count; enemyPrefab++)
        {
            pools.Add(new List<GameObject>());
            listOfSizes.Add(5);
        }
    }
    // Use this for initialization
    void Start ()
    {
        //Because the listOfSizes has the same lenght as the EnemyPrefabsList for each index of the listOfSizes we add to the current list
        //the number of enemies equal to the value store in the index.
		for (int index = 0; index < listOfSizes.Count; index++)
        {
            var size = listOfSizes[index];
            //Here we save the size of the list; the value inside the the index in the listOfSizes
            //we the same value of enemies inside the list.
            for (int i = 0; i < size; i++)
            {
                AddEnemy(index);
            }
        }
	}
    private void AddEnemy(int index)      //Se le pasa el index que debe de ser igual para le pool y el objeto
    {
        GameObject obj = Instantiate(enemyPrefabs[index], poolsPosition.position, poolsPosition.rotation);
        obj.GetComponent<AIEnemyController>().enemyInfo.enemyIndex = index;       //Modifica el index del componente del objeto instanciado.
        obj.gameObject.SetActive(false);
        pools[index].Add(obj);
    }
    public GameObject GetEnemy(int index, Transform target)      //Se le pasa el index que debe de ser igual para le pool y el objeto
    {
        if (pools[index].Count == 0)
            AddEnemy(index);    //Si no hay ningun GameObject lo añade.
        var list = pools[index];    //Referencia al pool especifico
        GameObject enemy = list[list.Count - 1];      //Se hace una referencia GameObject en la ultima posicion de la list, funciona como un queue
        list.RemoveAt(list.Count - 1);    //Se remueve el GameObject de la lista
        SetEnemyPosition(enemy, target);
        enemy.gameObject.SetActive(true);    //Se activa el GameObject
        return enemy;
    }
    public void ReleaseEnemy(GameObject obj)     ////Se le pasa el index que debe de ser igual para le pool y el objeto
    {
        //Cada GameObject deberia de tener informacion del pool al que esta asignado.
        obj.gameObject.SetActive(false);
        pools[obj.GetComponent<AIEnemyController>().enemyInfo.enemyIndex].Add(obj);
    }
    public void SetEnemyPosition(GameObject obj, Transform target)
    {
        obj.transform.position = target.position;
        obj.transform.rotation = target.rotation;
    }
}
