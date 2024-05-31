using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSizeUpgrade : Upgrade
{
  AttackComponent attackComponent;
  public override void Init(GameObject playerReference)
  {
    attackComponent = playerReference.GetComponent<AttackComponent>();
  }

  public override void ApplyUpgrade()
  {
    Debug.Log("Applying Upgrade to attack size");
    attackComponent.ApplyAttackSizeUpgrade();
  }
}
