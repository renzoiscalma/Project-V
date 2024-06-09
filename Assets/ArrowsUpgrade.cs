using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ArrowsUpgrade : Upgrade
{
  public override void ApplyUpgrade()
  {
    GetComponent<ArrowsComponent>().LevelUp();
  }
}
