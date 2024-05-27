using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackHit : MonoBehaviour
{
  void OnTriggerEnter2D(Collider2D collision)
  {
    // if different tags, meaning player can hit enemies, and vice versa
    if (collision.gameObject.CompareTag("Player"))
    {
      Debug.Log("Hurt this character!: " + collision.gameObject.tag);
      transform.GetComponentInParent<EnemyAttackComponent>().DamageObject(collision.gameObject);
    }
  }
}
