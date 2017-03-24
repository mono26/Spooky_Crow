using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolsManager : MonoBehaviour {
    //Singleton part
    private static PoolsManager instance;
    public static PoolsManager Instance
    {
        get { return instance; }
    }

    //PoolCode
    public List<GameObject> objectPrefabs;  //Lista con los prefabs de los pools
    public List<List<GameObject>> pools = new List<List<GameObject>>();    //Lista que contiene los pools, lista de listas.
    public List<int> listSizes;     //Lista con el tamaño de cada una de las otras listas

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;    //Parte del singleton en donde se asigna la unica instancia de la clase
        }
        else
            Destroy(gameObject);

        for (int i = 0; i < objectPrefabs.Count; i++)
        {
            pools.Add(new List<GameObject>());
            listSizes.Add(5);
        }
    }
    // Use this for initialization
    void Start ()
    {
		for (int index = 0; index < listSizes.Count; index++)  //Para iterar la lista de tamaños
        {
            var size = listSizes[index];
            for(int i = 0; i < size; i++)     //Iterar y instanciar los objetos necesarios deacuerdo al numero almacenado en la lista de tamaños.
            {
                AddObject(index);
            }
        }
	}
    private void AddObject(int index)      //Se le pasa el index que debe de ser igual para le pool y el objeto
    {
        GameObject obj = Instantiate(objectPrefabs[index]);
        obj.GetComponent<AIStateController>().enemyInfo.index = index;       //Modifica el index del componente del objeto instanciado.
        obj.gameObject.SetActive(false);
        pools[index].Add(obj);
    }
    public GameObject GetObject(int index)      //Se le pasa el index que debe de ser igual para le pool y el objeto
    {
        if (pools[index].Count == 0)
            AddObject(index);    //Si no hay ningun GameObject lo añade.
        var list = pools[index];    //Referencia al pool especifico
        GameObject enemy = list[list.Count - 1];      //Se hace una referencia GameObject en la ultima posicion de la list, funciona como un queue
        list.RemoveAt(list.Count - 1);    //Se remueve el GameObject de la lista
        enemy.gameObject.SetActive(true);    //Se activa el GameObject
        return enemy;
    }
    public void ReleaseObject(GameObject obj)     ////Se le pasa el index que debe de ser igual para le pool y el objeto
    {
        //Cada GameObject deberia de tener informacion del pool al que esta asignado.
        obj.gameObject.SetActive(false);
        pools[obj.GetComponent<AIStateController>().enemyInfo.index].Add(obj);
    }
}
