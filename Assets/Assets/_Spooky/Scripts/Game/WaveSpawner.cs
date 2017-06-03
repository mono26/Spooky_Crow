using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    //Singleton part
    private static WaveSpawner instance;
    public static WaveSpawner Instance
    {
        get { return instance; }
    }
    public enum SpawnState
    {
        SPAWNING, COUNTING, WAITING
    }
    [System.Serializable]   //Para poder ver una clase en el editor
    public class Wave
    {
        public string name;
        public EnemyInfo[] enemy;       //Alamcenar los distintos enemigos.
        public float spawnRate;
        public int[] count;     //Cuanto se spawnea de cada enmigo
    }
    public Wave[] waves;        //Para alamacenar las diferentes waves.
    public Text my_WaveText;
    public int my_NumberOfEnemies = 0;
    public int nextWave = 0;    //Para saber cual es la siguente Wave y comparar si es el maximo.
    public float timeBetweenWaves = 5.0f;      //Tiempo automatico para spawnear la siguiente oleada.
    public float waveCountDown;     //Tiempo para spawnear la oleada.

    public SpawnState state;    //Para saber el estado del spawn     

    public int my_SpawnPoint = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;    //Parte del singleton en donde se asigna la unica instancia de la clase
        }
        else
            Destroy(gameObject);
    }
    // Use this for initialization
    void Start ()
    {
        waveCountDown = timeBetweenWaves;
        state = SpawnState.COUNTING;
	}
	// Update is called once per frame
	void Update ()
    {
        if (nextWave == waves.Length)      //Para mirar si ya logro terminar todas las waves
        {
            GameManager.Instance.WinLevel();
            this.enabled = false;
        }

        if (state == SpawnState.WAITING)
        {
            if(my_NumberOfEnemies <= 0)
            {
                //Begin a new Round
                WaveCompleted();
            }
        }
        if (waveCountDown <= 0.0f && my_NumberOfEnemies == 0)
        {
            if(state != SpawnState.SPAWNING)    //Si cuando el conteo de spawn = 0 no esta en estado de spawn
            {
                StartCoroutine(SpawnWave(waves[nextWave]));     //Comienza la couroutine para spawnear.
            }
        }
        else
        {
            waveCountDown -= Time.deltaTime;
            waveCountDown = Mathf.Clamp(waveCountDown, 0f, Mathf.Infinity);
        }
	}
    IEnumerator SpawnWave(Wave _wave)   //Debes de pasarle un wave
    {
        state = SpawnState.SPAWNING;    //Al inicio para poner el stado en spawning.
        my_WaveText.text = "Wave:" + (nextWave + 1);
        my_NumberOfEnemies = 0;
        for(int enemy = 0; enemy < _wave.enemy.Length; enemy++)
        {
            for (int count = 0; count < _wave.count[enemy]; count++)
            {
                SpawnEnemy(_wave.enemy[enemy]);
                yield return new WaitForSeconds(1f / _wave.spawnRate);
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
        my_NumberOfEnemies++;
    }
    void WaveCompleted()
    {
        state = SpawnState.COUNTING;
        waveCountDown = timeBetweenWaves;
        nextWave++;
        Debug.Log("Se completo una wave");
    }
}
