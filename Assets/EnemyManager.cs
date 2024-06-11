using Cinemachine;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
  [SerializeField] GameObject enemy;
  [SerializeField] GameObject experiencePrefab;
  [SerializeField] float experienceValue;
  [SerializeField] float spawnTimer = 5;
  [SerializeField] Transform chaseTarget;
  [SerializeField] CinemachineVirtualCamera cmCam;
  [SerializeField] float cameraOffset = 10f;
  // increase hp every N units of time
  [SerializeField] float timeToIncreaseHp = 10f;
  float hpIncreaseTimer;
  // increase atk every N units of time
  [SerializeField] float timeToIncreaseAtk = 20f;
  float atkIncreaseTimer;
  // spawn a boss ever N units of time
  [SerializeField] float timeToSpawnBoss = 30f;
  float bossSpawnTimer;
  [SerializeField] float timeToDecreaseSpawnTimer = 40f;
  float decreaseSpawnTimer;
  bool spawnTimerMax = false;
  float timer = 0;
  [SerializeField] float timeToIncreaseExp = 50f;
  float increaseExpTimer = 0;

  // Start is called before the first frame update
  void Start()
  {
    decreaseSpawnTimer = timeToDecreaseSpawnTimer;
    bossSpawnTimer = timeToSpawnBoss;
    atkIncreaseTimer = timeToIncreaseAtk;
    hpIncreaseTimer = timeToIncreaseHp;
    increaseExpTimer = timeToIncreaseExp;
  }

  // Update is called once per frame
  void Update()
  {
    timer -= Time.deltaTime;
    if (timer <= 0f)
    {
      // spawn two enemies
      SpawnEnemy();
      SpawnEnemy();
      timer = spawnTimer;
    }
    hpIncreaseTimer -= Time.deltaTime;
    if (hpIncreaseTimer <= 0f)
    {
      Debug.Log("Enemy Health Increasing! " + gameObject.name);
      enemy.GetComponent<HealthComponent>().maxHp += 2;
      hpIncreaseTimer = timeToIncreaseHp;
    }
    atkIncreaseTimer -= Time.deltaTime;
    if (atkIncreaseTimer <= 0)
    {
      Debug.Log("Enemy Attack Increasing! " + gameObject.name);
      enemy.GetComponent<EnemyAttackComponent>().attackDamage += 2;
      atkIncreaseTimer = timeToIncreaseAtk;
    }
    bossSpawnTimer -= Time.deltaTime;
    if (bossSpawnTimer <= 0)
    {
      SpawnBoss();
      bossSpawnTimer = timeToSpawnBoss;
    }
    decreaseSpawnTimer -= Time.deltaTime;
    if (decreaseSpawnTimer <= 0 && !spawnTimerMax)
    {
      Debug.Log("Spawn Timer Decreasing! " + gameObject.name);
      spawnTimer -= 0.08f;
      decreaseSpawnTimer = timeToDecreaseSpawnTimer;
      if (spawnTimer <= 0.03f)
      {
        Debug.Log("Spawn Timer limit Reached.");
        spawnTimerMax = true;
        spawnTimer = 0.05f; // hard limit for spawn timer
      }
    }
    increaseExpTimer -= Time.deltaTime;
    if (increaseExpTimer <= 0)
    {
      Debug.Log("Experience Value increasing!");
      experienceValue += 1;
      increaseExpTimer = timeToIncreaseExp;
    }
  }

  private void SpawnEnemy()
  {
    Vector3 position = generateRandomPosition();
    GameObject newEnemy = Instantiate(enemy);
    newEnemy.transform.position = position;
    Enemy enemyComponent = newEnemy.GetComponent<Enemy>();
    enemyComponent.Init(chaseTarget.gameObject, experiencePrefab);
    enemyComponent.experienceValue = experienceValue;
  }

  private Vector3 generateRandomPosition()
  {
    var cameraPosition = cmCam.transform.position;
    var spawnLocation = cameraPosition;
    // determine if it spawns on x or y axis
    var XorY = Random.Range(0, 2);
    if (XorY == 0)
    {
      var upOrDown = Random.Range(0, 2);
      spawnLocation.y += upOrDown == 0 ? cameraOffset : -cameraOffset;
      spawnLocation.x += Random.Range(cameraOffset, -cameraOffset);
    }
    else
    {
      var leftOrRight = Random.Range(0, 2);
      spawnLocation.x += leftOrRight == 0 ? cameraOffset : -cameraOffset;
      spawnLocation.y += Random.Range(cameraOffset, -cameraOffset);
    }
    spawnLocation.z = 0;
    return spawnLocation;
  }

  private void SpawnBoss()
  {
    Debug.Log("Spawn some boss now!");
  }
}
