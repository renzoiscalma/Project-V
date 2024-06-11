using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballUpgrade : Upgrade
{
  public override void ApplyUpgrade()
  {
    GetComponent<FireballManager>().LevelUp();
  }
}
