using Cinemachine;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
  [SerializeField] GameObject enemy;
  [SerializeField] GameObject experience;
  [SerializeField] float spawnTimer = 5;
  [SerializeField] Transform chaseTarget;
  [SerializeField] CinemachineVirtualCamera cmCam;
  [SerializeField] float cameraOffset = 10;
  float timer;
  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    timer -= Time.deltaTime;
    if (timer < 0f)
    {
      SpawnEnemy();
      timer = spawnTimer;
    }
  }

  private void SpawnEnemy()
  {
    Vector3 position = generateRandomPosition();
    GameObject newEnemy = Instantiate(enemy);
    newEnemy.transform.position = position;
    newEnemy.GetComponent<Enemy>().Init(chaseTarget.gameObject, experience);
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
}
