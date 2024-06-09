using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeathComponent : MonoBehaviour
{
  Animator animator;
  void Awake()
  {
    if (gameObject.CompareTag("Monsters"))
    {
      animator = GetComponent<Animator>();
    }
    else
    {
      animator = GetComponentInChildren<Animator>();
    }
  }
  public void Kill()
  {
    if (gameObject.CompareTag("Monsters"))
    {
      gameObject.GetComponent<Enemy>().SpawnExperienceShard();
    }
    animator.SetTrigger("death");
  }

  public void DestroyObject()
  {
    Destroy(transform.gameObject);
  }
}
