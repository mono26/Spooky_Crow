using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    //Singleton part
    private static BulletPool instance;
    public static BulletPool Instance
    {
        get { return instance; }
    }
    [SerializeField]
    private Rigidbody2D bulletPrefab;
    [SerializeField]
    private int size;

    private List<Rigidbody2D> bullets;

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
        bullets = new List<Rigidbody2D>();
    }
    private void AddBullet()
    {
        Rigidbody2D instance = Instantiate(bulletPrefab);
        instance.gameObject.SetActive(false);
        bullets.Add(instance);
    }
    public Rigidbody2D GetBullet()
    {
        if (bullets.Count == 0)
            AddBullet();    //Si no hay ninguna bala la añade.

        Rigidbody2D food = bullets[bullets.Count - 1];      //Se hace una referencia a la bala en la ultima posicion de la list, funciona como un queue
        bullets.RemoveAt(bullets.Count - 1);    //Se remueve la bala de la lista
        food.gameObject.SetActive(true);    //Se activa la bala
        return food;
    }

    public void ReleaseBullet(Rigidbody2D releaseBullet)
    {
        releaseBullet.gameObject.SetActive(false);
        bullets.Add(releaseBullet);
    }
}