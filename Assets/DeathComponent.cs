using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeathComponent : MonoBehaviour
{
  Animator animator;

  void Awake()
  {
    animator = GetComponent<Animator>();
  }
  public void Kill()
  {
    animator.SetTrigger("death");
  }

  public void DestroyObject()
  {
    Destroy(transform.gameObject);
    // add reward component here for rolling.
  }
}
