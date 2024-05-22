using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animate : MonoBehaviour
{
  Animator animator;
  public bool moving;
  public bool attacking;
  public bool rolling;
  private void Awake()
  {
    animator = GetComponentInChildren<Animator>();
  }

  private void Update()
  {
    animator.SetBool("moving", moving);
    animator.SetBool("attacking", attacking);
    animator.SetBool("rolling", rolling);
  }

}
