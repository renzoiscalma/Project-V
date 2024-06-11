using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Explosion : MonoBehaviour
{
  private Vector3 baseScale;
  float damage;
  public void Init(float damage, float aoeScalePercent)
  {
    this.damage = damage;
    baseScale = transform.localScale;
    transform.localScale = baseScale * (1 + aoeScalePercent);
  }
  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.CompareTag("Monsters"))
    {
      other.gameObject.GetComponent<HealthComponent>().TakeDamage(damage);
    }
  }

  void OnAnimationEnd()
  {
    Destroy(gameObject);
  }
}
