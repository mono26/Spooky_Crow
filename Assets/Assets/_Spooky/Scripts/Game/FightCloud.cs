using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightCloud : MonoBehaviour
{
    private static FightCloud instance;
    public static FightCloud Instance
    {
        get { return instance; }
    }
    public GameObject player;
    public GameObject enemy;

    public Transform poolsPosition;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;    //Parte del singleton en donde se asigna la unica instancia de la clase
        }
        else
            Destroy(gameObject);

        transform.position = poolsPosition.position;
    }
    private void OnMouseDown()
    {
        EndFight();
    }
    //Para ejecutar la pelea
    public void PrepareFight()
    {
        transform.position = player.transform.position;
        player.SetActive(false);
        enemy.SetActive(false);
    }
    //Para pasar los datos a la pelea
    public void SetFight(GameObject _player, GameObject _enemy)
    {
        player = _player;
        enemy = _enemy;
        PrepareFight();
    }
    //Para finalizar la pelea cuando hagan click en el
    private void EndFight()
    {
        transform.position = poolsPosition.position;
        player.SetActive(true);
        enemy.GetComponent<AIEnemyController>().Die();
    }
}
