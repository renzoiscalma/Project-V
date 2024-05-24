using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
  [SerializeField] GameObject enemy;
  [SerializeField] Vector2 spawnArea;
  [SerializeField] float spawnTimer;
  [SerializeField] Transform chaseTarget;
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
    Vector3 position = new(
      UnityEngine.Random.Range(-spawnArea.x, spawnArea.x),
      UnityEngine.Random.Range(-spawnArea.y, spawnArea.y),
      0f
    );
    GameObject newEnemy = Instantiate(enemy);
    newEnemy.transform.position = position;
    newEnemy.GetComponent<Enemy>().Init(chaseTarget.gameObject);
  }
}
