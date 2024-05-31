using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UpgradeType
{
  Weapon,   // upgrades the weapon of the player
  Player, // upgrades the stats of the player
}
public class Upgrade : MonoBehaviour
{
  [SerializeField]
  public string upgradeName;
  [SerializeField]
  public string upgradeDesc;
  [SerializeField]
  public Sprite image;
  public int level;
  public GameObject playerReference;
  public UpgradeType upgradeType;

  public virtual void Init(GameObject playerReference)
  {
    this.playerReference = playerReference;
  }
  public virtual void ApplyUpgrade()
  {

  }
}
