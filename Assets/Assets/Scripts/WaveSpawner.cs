using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public enum SpawnState
    {
        SPAWNING, COUNTING, WAITING
    }
    [System.Serializable]   //Para poder ver una clase en el editor
    public class Wave
    {
        public string name;
        public Rigidbody enemy;
        public float spawnRate;
        public int count;
    }
    public Wave[] waves;        //Para alamacenar las diferentes waves.
    public int nextWave = 0;    //Para saber cual es la siguente Wave y comparar si es el maximo.
    public float timeBetweenWaves = 5.0f;      //Tiempo automatico para spawnear la siguiente oleada.
    public float waveCountDown;     //Tiempo para spawnear la oleada.

    private float searchTime;

    public SpawnState state;    //Para saber el estado del spawn     

    public Transform[] spawnPoints;   //Para almacenar los spawn points

	// Use this for initialization
	void Start ()
    {
        waveCountDown = timeBetweenWaves;
        state = SpawnState.COUNTING;
        if (spawnPoints.Length == 0)
            Debug.LogError("No spawn points");
        if (waves.Length == 0)
            Debug.LogError("No wave to spawn");
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (state == SpawnState.WAITING)
        {
            if(!EnemyIsAlive())
            {
                //Begin a new Round
                WaveCompleted();
            }
            else
            {
                return;
            }
        }
		if (waveCountDown <= 0.0f)
        {
            if(state != SpawnState.SPAWNING)    //Si cuando el conteo de spawn = 0 no esta en estado de spawn
            {
                Debug.Log("Should Start Spawning");
                StartCoroutine(SpawnWave(waves[nextWave]));     //Comienza la couroutine para spawnear.
            }
        }
        else
        {
            waveCountDown -= Time.deltaTime;
        }
	}

    bool EnemyIsAlive()
    {
        searchTime -= Time.deltaTime;   //Reduce el tiempo para buscar los enemigos
        if (searchTime == 0.0f)
        {
            searchTime = 1.0f;
            Debug.Log("Looking for enemies");
            if (GameObject.FindGameObjectsWithTag("Enemy") == null)     //Si no encuentra ningun enemigo return false
                return false;
        }
        return true;
    }
    IEnumerator SpawnWave(Wave _wave)   //Debes de pasarle un wave
    {
        Debug.Log("Spawning Wave");
        state = SpawnState.SPAWNING;    //Al inicio para poner el stado en spawning.
        for (int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemy);    //Should pass the correct enemy per wave
            //SpawnEnemy(EnemyPool.Instance.GetEnemy());
            yield return new WaitForSeconds(1f / _wave.spawnRate);
        }
        state = SpawnState.WAITING;
        yield break;
    }

    void SpawnEnemy(Rigidbody _enemy)
    {
        Debug.Log("Spawning Enemy:" + _enemy.name);
        var spawnIndex = Random.Range(0, spawnPoints.Length - 1);   //Generar un spawn aleatorio, - 1 porque el lenght se cuenta desde 1 no desde 0
        var instance = GameObject.Instantiate(_enemy, spawnPoints[spawnIndex].position, spawnPoints[spawnIndex].rotation);
    }

    void WaveCompleted()
    {
        state = SpawnState.COUNTING;
        waveCountDown = timeBetweenWaves;

        if(nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;
            Debug.Log("Waves Completed");
        }

        nextWave++;
    }
}
