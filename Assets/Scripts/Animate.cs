using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animate : MonoBehaviour
{
  Animator animator;
  private PlayerMove playerMove;
  private AttackComponent playerAttackComponent;
  private Transform spriteTransform;
  private static Quaternion facingRightQ = new(0, 0, 0, 0);
  private static Quaternion facingLeftQ = new(0, 180, 0, 0);
  private void Awake()
  {
    animator = GetComponentInChildren<Animator>();
    playerMove = transform.GetComponent<PlayerMove>();
    spriteTransform = transform.GetComponent<Transform>();
    playerAttackComponent = transform.GetComponent<AttackComponent>();
  }
  public void Start()
  {
    animator.SetFloat("attackSpeedMultiplier", playerAttackComponent.attackSpeedMultiplier);
  }
  private void Update()
  {
    HandleFacing();
    animator.SetBool("moving", playerMove.moving);
    if (playerMove.shouldRoll)
    {
      animator.SetTrigger("rolling");
      playerMove.rolling = true;
      playerMove.shouldRoll = false;
    }
    animator.SetBool("attacking", playerAttackComponent.attacking);
  }

  private void HandleFacing()
  {
    spriteTransform.rotation = playerMove.facingRight ? facingRightQ : facingLeftQ;
  }

  private void SetAttackMulitplier(float attackSpeedMultiplier)
  {
    animator.SetFloat("attackSpeedMultiplier", attackSpeedMultiplier);
  }
}
