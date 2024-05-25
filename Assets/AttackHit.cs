using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// attached to sprite of unit when attacking
public class AttackHit : MonoBehaviour
{
  void OnCollisionEnter2D(Collision2D collision)
  {
    // if different tags, meaning player can hit enemies, and vice versa
    if (!collision.gameObject.CompareTag(transform.gameObject.tag))
    {
      transform.GetComponentInParent<AttackComponent>().DamageObject(collision.gameObject);
    }
  }
}
