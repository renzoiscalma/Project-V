using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animate : MonoBehaviour
{
  Animator animator;
  public bool moving;
  public bool attacking;
  public bool rolling;
  private PlayerMove playerMove;
  private void Awake()
  {
    animator = GetComponentInChildren<Animator>();
    playerMove = transform.GetComponent<PlayerMove>();
  }

  private void Update()
  {
    animator.SetBool("moving", moving);
    animator.SetBool("attacking", attacking);
  }

  public void Roll()
  {
    animator.SetTrigger("rolling");
    playerMove.SetRolling(true);
  }

}
