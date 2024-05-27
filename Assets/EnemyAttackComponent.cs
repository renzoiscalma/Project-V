using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackComponent : MonoBehaviour
{

  [SerializeField] float attackDamage = 2;
  [SerializeField] public float attackSpeedMultiplier = 0.8f; // multiplier for attack animation
  [SerializeField] public float attackRadius = 0f; // % increase in attack radius
  private float baseAttackRadius = 0f;

  void Awake()
  {
    baseAttackRadius = gameObject.GetComponentInChildren<CircleCollider2D>().radius;
  }
  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }

  private void IncreaseAttackRadius(float attackRadius)
  {
    attackRadius += attackRadius;
  }

  public void DamageObject(GameObject targetGameObject)
  {
    if (targetGameObject.TryGetComponent<HealthComponent>(out var targetHealthComponent))
    {
      targetHealthComponent.TakeDamage(attackDamage);
    }
  }
}
