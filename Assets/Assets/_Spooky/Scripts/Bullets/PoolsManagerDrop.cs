﻿using System.Collections.Generic;
using UnityEngine;

public class PoolsManagerDrop : MonoBehaviour
{
    //Singleton part
    private static PoolsManagerDrop instance;
    public static PoolsManagerDrop Instance
    {
        get { return instance; }
    }

    //PoolCode
    public List<GameObject> dropPrefabs;  //Lista con los prefabs de los pools
    public List<List<GameObject>> dropPools = new List<List<GameObject>>();    //Lista que contiene los pools, lista de listas.
    public List<int> listSizes;     //Lista con el tamaño de cada una de las otras listas, debe de estar ordenada igual que los prefabs

    [SerializeField]
    Transform poolsPosition;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;    //Parte del singleton en donde se asigna la unica instancia de la clase
        }
        else
            Destroy(gameObject);

        for (int enemyPrefab = 0; enemyPrefab < dropPrefabs.Count; enemyPrefab++)
        {
            dropPools.Add(new List<GameObject>());
            listSizes.Add(5);       //Para que el tamaño insial para cada una de las listas sea 5
        }
    }
    // Use this for initialization
    void Start()
    {
        for (int index = 0; index < listSizes.Count; index++)  //Para iterar la lista de tamaños
        {
            var size = listSizes[index];
            for (int i = 0; i < size; i++)     //Iterar y instanciar los objetos necesarios deacuerdo al numero almacenado en la lista de tamaños.
            {
                AddObject(index);
            }
        }
    }
    private void AddObject(int index)      //Se le pasa el index que debe de ser igual para le pool y el objeto
    {
        GameObject obj = Instantiate(dropPrefabs[index], poolsPosition.position, poolsPosition.rotation);
        obj.GetComponent<AIDrop>().objectIndex = index;       //Modifica el index del componente del objeto instanciado.
        obj.gameObject.SetActive(false);
        dropPools[index].Add(obj);
    }
    public GameObject GetObject(int index, Transform target)      //Se le pasa el index que debe de ser igual para le pool y el objeto
    {
        if (dropPools[index].Count == 0)
            AddObject(index);    //Si no hay ningun GameObject lo añade.
        var list = dropPools[index];    //Referencia al pool especifico
        GameObject drop = list[list.Count - 1];      //Se hace una referencia GameObject en la ultima posicion de la list, funciona como un queue
        list.RemoveAt(list.Count - 1);    //Se remueve el GameObject de la lista
        SetPosition(drop, target);
        drop.gameObject.SetActive(true);    //Se activa el GameObject
        return drop;
    }
    public void ReleaseDrop(GameObject obj)     //Se le pasa el index que debe de ser igual para le pool y el objeto
    {
        //Cada GameObject deberia de tener informacion del pool al que esta asignado.
        obj.gameObject.SetActive(false);
        obj.GetComponent<AIDrop>().objectReward = 0;
        dropPools[obj.GetComponent<AIDrop>().objectIndex].Add(obj);
    }
    public void SetPosition(GameObject obj, Transform target)
    {
        obj.transform.position = target.position;
        obj.transform.rotation = target.rotation;
    }
}
