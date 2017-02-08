
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
    private Rigidbody2D enemyPrefab;
    [SerializeField]
    private int size;

    private List<Rigidbody2D> enemies;

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
        enemies = new List<Rigidbody2D>();
    }
    private void AddBullet()
    {
        Rigidbody2D instance = Instantiate(enemyPrefab);
        instance.gameObject.SetActive(false);
        enemies.Add(instance);
    }
    public Rigidbody2D GetBullet()
    {
        if (enemies.Count == 0)
            AddBullet();    //Si no hay ninguna bala la añade.

        Rigidbody2D enemy = enemies[enemies.Count - 1];      //Se hace una referencia a la bala en la ultima posicion de la list, funciona como un queue
        enemies.RemoveAt(enemies.Count - 1);    //Se remueve la bala de la lista
        enemy.gameObject.SetActive(true);    //Se activa la bala
        return enemy;
    }

    public void ReleaseBullet(Rigidbody2D releaseBullet)
    {
        releaseBullet.gameObject.SetActive(false);
        enemies.Add(releaseBullet);
    }
}
