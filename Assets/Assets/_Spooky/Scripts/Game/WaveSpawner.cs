using System.Collections;
using UnityEngine;
using UnityEngine.UI;

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
    public Text my_WaveText;
    public int nextWave = 0;    //Para saber cual es la siguente Wave y comparar si es el maximo.
    public float timeBetweenWaves = 5.0f;      //Tiempo automatico para spawnear la siguiente oleada.
    public float waveCountDown;     //Tiempo para spawnear la oleada.

    private float searchTime = 1.0f;

    public SpawnState state;    //Para saber el estado del spawn     

    public int my_SpawnPoint = 0;

	// Use this for initialization
	void Start ()
    {
        waveCountDown = timeBetweenWaves;
        state = SpawnState.COUNTING;
        //Esta seccion del codigo solo sirve para asignar los index del pool al WaveInfo para el prefab contenido en el wave.enemy
        //Esta machetiado para que funcione solo si los enemy infos pertenecientes a cada prefab son los mismos y estan en el mismo orden tanto en el pool como en la wave.enemy
        /*for (int wave = 0; wave < waves.Length; wave++)
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
        }*/
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
            searchTime = 1.5f;
            var enemies = GameObject.FindGameObjectsWithTag("Enemy");
            int index = 0;
            while (index < enemies.Length)    //Si no encuentra ningun enemigo return false
            {
                if (enemies[index].activeInHierarchy)
                {
                    index++; 
                }
                return true;
            }
            if (index == enemies.Length)
                return false;     
        }
        return true;
    }
    IEnumerator SpawnWave(Wave _wave)   //Debes de pasarle un wave
    {
        state = SpawnState.SPAWNING;    //Al inicio para poner el stado en spawning.
        my_WaveText.text = "Wave:" + nextWave + 1;
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
                for (int count = 0; count < _wave.count[enemy]; count++)
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
        //Generar un spawn aleatorio, - 1 porque el lenght se cuenta desde 1 no desde 0
        var obj = PoolsManager.Instance.GetObject(_enemy.index);
        my_SpawnPoint = Random.Range(0, GameManager.Instance.spawnPoints.Length);
        obj.transform.position = GameManager.Instance.spawnPoints[my_SpawnPoint].position;
        obj.transform.rotation = Quaternion.identity;
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
        Debug.Log("Se completo una wave");
    }
}
