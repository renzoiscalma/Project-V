using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
  public bool dead = false;
  [SerializeField] float maxHp = 10;
  float health = 10;
  private DeathComponent deathComponent;
  private BoxCollider2D hitboxCollider;
  private DamageFlashComponent damageFlasher;

  void Awake()
  {
    deathComponent = GetComponent<DeathComponent>();
    hitboxCollider = GetComponent<BoxCollider2D>();
    damageFlasher = GetComponentInChildren<DamageFlashComponent>();
    health = maxHp;
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

  public void ApplyLevelUp()
  {
    health += 2;
    maxHp += 2;
  }
}
