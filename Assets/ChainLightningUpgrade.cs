using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainLightningUpgrade : Upgrade
{
  public override void ApplyUpgrade()
  {
    GetComponent<ChainLightning>().LevelUp();
  }
}
