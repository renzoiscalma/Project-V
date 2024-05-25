using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
  [SerializeField] float walkSpeed;
  private Transform TransformTarget;
  private GameObject GameObjTarget;
  private Rigidbody2D rb2d;
  private Animator animator;
  private HealthComponent healthComponent;
  public void Init(GameObject gameObjectTarget)
  {
    GameObjTarget = gameObjectTarget;
    TransformTarget = gameObjectTarget.transform;
  }

  void Awake()
  {
    rb2d = GetComponent<Rigidbody2D>();
    animator = GetComponent<Animator>();
    healthComponent = GetComponent<HealthComponent>();
  }

  void Update()
  {
    if (GameObjTarget == null) return;
    if (!healthComponent.dead)
    {
      Vector3 direction = (TransformTarget.position - (Vector3)rb2d.position).normalized;
      if (direction.x < 0)
      {
        transform.rotation = new Quaternion(0, 180, 0, 0);
      }
      else if (direction.x > 0)
      {
        transform.rotation = new Quaternion(0, 0, 0, 0);
      }
      rb2d.velocity = direction * walkSpeed;
      animator.SetBool("moving", rb2d.velocity.x != 0 && rb2d.velocity.y != 0);
    }
  }

  private void OnCollisionStay2D(Collision2D collisionInfo)
  {
    if (collisionInfo.gameObject == GameObjTarget)
    {
      Attack();
    }
  }

  void Attack()
  {
    animator.SetTrigger("attack");
  }
}
