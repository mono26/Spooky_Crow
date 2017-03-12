
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    //Singleton part
    private static EnemyPool instance;
    public static EnemyPool Instance
    {
        get { return instance; }
    }
    [SerializeField]
    private Rigidbody enemyPrefab;
    [SerializeField]
    private int size;

    private List<Rigidbody> enemies;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;    //Parte del singleton en donde se asigna la unica instancia de la clase
        }
        else
            Destroy(gameObject);
    }
    private void Start()
    {
        enemies = new List<Rigidbody>();
    }
    private void AddEnemy()
    {
        Rigidbody instance = Instantiate(enemyPrefab);
        instance.gameObject.SetActive(false);
        enemies.Add(instance);
    }
    public Rigidbody GetEnemy()
    {
        if (enemies.Count == 0)
            AddEnemy();    //Si no hay ninguna bala la añade.

        Rigidbody enemy = enemies[enemies.Count - 1];      //Se hace una referencia a la bala en la ultima posicion de la list, funciona como un queue
        enemies.RemoveAt(enemies.Count - 1);    //Se remueve la bala de la lista
        enemy.gameObject.SetActive(true);    //Se activa la bala
        return enemy;
    }

    public void ReleaseEnemy(Rigidbody releaseEnemy)
    {
        releaseEnemy.gameObject.SetActive(false);
        enemies.Add(releaseEnemy);
    }
}
