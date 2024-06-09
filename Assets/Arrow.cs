using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Arrow : MonoBehaviour
{
  public float damage = 0;
  public float ttl = 0; // time to live
  float timeAlive = 0;
  void Awake()
  {

  }

  void Update()
  {
    timeAlive += Time.deltaTime;
    if (timeAlive >= ttl)
    {
      Destroy(gameObject);
    }
  }

  void OnTriggerEnter2D(Collider2D collider2D)
  {
    if (collider2D.gameObject.CompareTag("Monsters"))
    {
      collider2D.GetComponent<HealthComponent>().TakeDamage(damage);
    }
  }
}
