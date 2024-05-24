using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
  [SerializeField] float health = 10;
  public bool dead = false;
  public void TakeDamage(float damage)
  {
    health -= damage;
    if (health <= 0)
    {
      transform.GetComponent<DeathComponent>().Kill();
      transform.GetComponent<BoxCollider2D>().enabled = false;
      dead = true;
    }
  }
}
