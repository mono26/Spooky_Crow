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
        public enum WaveType
        {
            SINGLE, MULTIPLE    //Para crear waves con multiples enemigos.
        }
        public WaveType type;
        public string name;
        public EnemyInfo[] enemy;       //Alamcenar los distintos enemigos.
        public float spawnRate;
        public int[] count;     //Cuanto se spawnea de cada enmigo
    }
    public Wave[] waves;        //Para alamacenar las diferentes waves.
    public int nextWave = 0;    //Para saber cual es la siguente Wave y comparar si es el maximo.
    public float timeBetweenWaves = 5.0f;      //Tiempo automatico para spawnear la siguiente oleada.
    public float waveCountDown;     //Tiempo para spawnear la oleada.

    private float searchTime = 1.0f;

    public SpawnState state;    //Para saber el estado del spawn     

    public Transform[] spawnPoints;   //Para almacenar los spawn points

    void Awake()
    {
        waveCountDown = timeBetweenWaves;
        state = SpawnState.COUNTING;
        if (spawnPoints.Length == 0)
            Debug.LogError("No spawn points");
        if (waves.Length == 0)
            Debug.LogError("No wave to spawn");
    }
	// Use this for initialization
	void Start ()
    {
        //Esta seccion del codigo solo sirve para asignar los index del pool al WaveInfo para el prefab contenido en el wave.enemy
        //Esta machetiado para que funcione solo si los enemy infos pertenecientes a cada prefab son los mismos y estan en el mismo orden tanto en el pool como en la wave.enemy
        for (int wave = 0; wave < waves.Length; wave++)
        {
            if (waves[wave].type == Wave.WaveType.SINGLE)
                waves[wave].enemy[0].index = wave;
            else if(waves[wave].type == Wave.WaveType.MULTIPLE)
            {
                for (int enemy = 0; enemy < waves[wave].enemy.Length; enemy++)
                {
                    waves[wave].enemy[enemy].index = enemy;
                }
            }
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (state == SpawnState.WAITING)
        {
            Debug.Log(EnemyIsAlive());
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
        if (searchTime <= 0.0f)
        {
            searchTime = 1.0f;
            var enemies = GameObject.FindGameObjectsWithTag("Enemy");
            for (int i = 0; i < enemies.Length; i++)    //Si no encuentra ningun enemigo return false
            {
                if(enemies[i].activeInHierarchy)
                {

                }
                return false;
            }     
            else
            {
                Debug.Log("Encontre enemigos");
                return true;
            }
        }
        return true;
    }
    IEnumerator SpawnWave(Wave _wave)   //Debes de pasarle un wave
    {
        state = SpawnState.SPAWNING;    //Al inicio para poner el stado en spawning.
        if (_wave.type == Wave.WaveType.SINGLE)
        {
            for (int i = 0; i < _wave.count[0]; i++)
            {
                SpawnEnemy(_wave.enemy[0]);    //Should pass the correct enemy per wave
                //SpawnEnemy(EnemyPool.Instance.GetEnemy());
                yield return new WaitForSeconds(1f / _wave.spawnRate);
            }
        }
        else if(_wave.type == Wave.WaveType.MULTIPLE)
        {
            for(int enemy = 0; enemy < _wave.enemy.Length; enemy++)
            {
                for (int count = 0; count < _wave.count[count]; count++)
                {
                    SpawnEnemy(_wave.enemy[enemy]);
                    yield return new WaitForSeconds(1f / _wave.spawnRate);
                }
            }
        }
        state = SpawnState.WAITING;
        yield break;
    }

    void SpawnEnemy(EnemyInfo _enemy)
    {
        var spawnIndex = Random.Range(0, spawnPoints.Length - 1);   //Generar un spawn aleatorio, - 1 porque el lenght se cuenta desde 1 no desde 0
        var obj = PoolsManager.Instance.GetObject(_enemy.index);
        obj.transform.position = spawnPoints[spawnIndex].position;
        obj.transform.rotation = spawnPoints[spawnIndex].rotation;
    }

    void WaveCompleted()
    {
        state = SpawnState.COUNTING;
        waveCountDown = timeBetweenWaves;

        if(nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;
        }

        nextWave++;
    }
}
