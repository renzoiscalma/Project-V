using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponThrowableBase : MonoBehaviour
{
  public float nextAttackTime = 1;
  public float damage = 0;
  public float timeToLive = 0;

  public float currAttackTimer = 0;

  public virtual void Update()
  {
    currAttackTimer -= Time.deltaTime;
    if (currAttackTimer <= 0)
    {
      OnAttack();
      currAttackTimer = nextAttackTime;
    }
  }

  public virtual void OnAttack()
  {

  }

  public virtual void ManualAttackTrigger()
  {

  }
}
