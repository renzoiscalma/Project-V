using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// attached to sprite of unit when attacking
public class AttackHit : MonoBehaviour
{
  [SerializeField] AttackComponent attackComponent;
  void OnTriggerEnter2D(Collider2D collision)
  {
    // if different tags, meaning player can hit enemies, and vice versa
    if (!collision.gameObject.CompareTag(transform.gameObject.tag))
    {
      transform.GetComponentInParent<AttackComponent>().DamageObject(collision.gameObject);
    }
  }

  public void OnAttackHitboxActivate()
  {
    attackComponent.HandleAttackHitboxActivate();
  }
}
