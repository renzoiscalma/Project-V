using System;
using System.Collections;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
  public bool dead = false;
  [SerializeField] public float maxHp = 10;
  [SerializeField] FillableBar healthUI;
  [SerializeField] bool hasUI = false;
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
    if (hasUI)
    {
      healthUI.Init(health, maxHp);
    }
  }
  public void TakeDamage(float damage)
  {
    health -= damage;
    if (hasUI)
    {
      healthUI.SetValue(health);
    }
    damageFlasher.DamageFlash();
    if (health <= 0 && !dead)
    {
      deathComponent.Kill();
      hitboxCollider.enabled = false;
      health = 0;
      dead = true;
    }
  }

  public void ApplyLevelUp()
  {
    health += 2;
    maxHp += 2;
    if (hasUI)
    {
      healthUI.SetValue(health);
      healthUI.SetMaxValue(maxHp);
    }
  }

  public float GetHealth()
  {
    return health;
  }
}
