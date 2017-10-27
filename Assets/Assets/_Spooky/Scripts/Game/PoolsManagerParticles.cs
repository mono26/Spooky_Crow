using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolsManagerParticles : MonoBehaviour
{
    //Singleton part
    private static PoolsManagerParticles instance;
    public static PoolsManagerParticles Instance
    {
        get { return instance; }
    }

    //PoolCode
    public List<GameObject> particlesPrefabs;  //Lista con los prefabs de los pools
    public List<List<GameObject>> pools = new List<List<GameObject>>();    //Lista que contiene los pools, lista de listas.
    public List<int> listSizes;     //Lista con el tamaño de cada una de las otras listas, debe de estar ordenada igual que los prefabs

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;    //Parte del singleton en donde se asigna la unica instancia de la clase
        }
        else
            Destroy(gameObject);

        for (int particlePrefab = 0; particlePrefab < particlesPrefabs.Count; particlePrefab++)
        {
            pools.Add(new List<GameObject>());
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
                AddParticle(index);
            }
        }
    }
    private void AddParticle(int index)      //Se le pasa el index que debe de ser igual para le pool y el objeto
    {
        GameObject obj = Instantiate(particlesPrefabs[index]);
        obj.GetComponent<BulletController>().bulletInfo.objectIndex = index;       //Modifica el index del componente del objeto instanciado.
        obj.gameObject.SetActive(false);
        pools[index].Add(obj);
    }
    public GameObject GetParticle(int index)      //Se le pasa el index que debe de ser igual para le pool y el objeto
    {
        if (pools[index].Count == 0)
            AddParticle(index);    //Si no hay ningun GameObject lo añade.
        var list = pools[index];    //Referencia al pool especifico
        GameObject particle = list[list.Count - 1];      //Se hace una referencia GameObject en la ultima posicion de la list, funciona como un queue
        list.RemoveAt(list.Count - 1);    //Se remueve el GameObject de la lista
        particle.gameObject.SetActive(true);    //Se activa el GameObject
        return particle;
    }
    public void ReleaseParticle(GameObject obj)     ////Se le pasa el index que debe de ser igual para le pool y el objeto
    {
        //Cada GameObject deberia de tener informacion del pool al que esta asignado.
        obj.gameObject.SetActive(false);
        var rb = obj.GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0,0,0);
        pools[obj.GetComponent<BulletController>().bulletInfo.objectIndex].Add(obj);
    }
}
