using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
  [SerializeField] float walkSpeed;
  [SerializeField] public float experienceValue;
  public GameObject ExperiencePrefab;
  [SerializeField] private Transform TransformTarget;
  [SerializeField] GameObject GameObjTarget;
  private Rigidbody2D rb2d;
  private Animator animator;
  private HealthComponent healthComponent;
  public void Init(GameObject gameObjectTarget, GameObject experiencePrefab)
  {
    gameObject.SetActive(true);
    GameObjTarget = gameObjectTarget;
    TransformTarget = gameObjectTarget.transform;
    ExperiencePrefab = experiencePrefab;
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
    float slowDown = 1f;
    if (!healthComponent.dead)
    {
      Vector3 direction = (TransformTarget.position - (Vector3)rb2d.position).normalized;
      if (animator.GetCurrentAnimatorStateInfo(0).IsName("attacking"))
      {
        slowDown *= 0.3f; // slow down when attacking
      }
      else // do not allow to switch sides when attacking
      {
        if (direction.x < 0)
        {
          transform.rotation = new Quaternion(0, 180, 0, 0);
        }
        else if (direction.x > 0)
        {
          transform.rotation = new Quaternion(0, 0, 0, 0);
        }
      }
      rb2d.velocity = slowDown * walkSpeed * direction;

      animator.SetBool("moving", rb2d.velocity.x != 0 && rb2d.velocity.y != 0);
    }
    else
    {
      rb2d.velocity *= 0;
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

  public void SpawnExperienceShard()
  {
    GameObject expShard = Instantiate(ExperiencePrefab);
    expShard.GetComponent<ExperienceComponent>().value = experienceValue;
    expShard.transform.position = transform.position;
    expShard.SetActive(true);
  }
}
