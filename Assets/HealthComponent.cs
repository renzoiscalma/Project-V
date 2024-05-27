using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
  [SerializeField] float health = 10;
  public bool dead = false;
  private DeathComponent deathComponent;
  private BoxCollider2D hitboxCollider;
  private DamageFlashComponent damageFlasher;

  void Awake()
  {
    deathComponent = GetComponent<DeathComponent>();
    hitboxCollider = GetComponent<BoxCollider2D>();
    damageFlasher = GetComponentInChildren<DamageFlashComponent>();
  }
  public void TakeDamage(float damage)
  {
    health -= damage;
    damageFlasher.DamageFlash();
    if (health <= 0)
    {
      deathComponent.Kill();
      hitboxCollider.enabled = false;
      dead = true;
    }
  }
}
