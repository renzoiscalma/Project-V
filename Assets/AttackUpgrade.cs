using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackUpgrade : Upgrade
{
  AttackComponent attackComponent;
  // Start is called before the first frame update
  override public void Init(GameObject playerReference)
  {
    attackComponent = playerReference.GetComponent<AttackComponent>();
  }

  override public void ApplyUpgrade()
  {
    Debug.Log("Applying attack level up!");
    level++;
    attackComponent.ApplyAtkUpgrade();
  }
}
