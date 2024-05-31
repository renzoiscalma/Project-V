using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeChoiceButton : MonoBehaviour
{
  UpgradeImage upgradeImage;
  UpgradeLabelComponent upgradeLabel;
  UpgradeDescriptionComponent upgradeDesc;
  Button button;
  bool initialized = false;
  // Start is called before the first frame update
  void Start()
  {
    button = GetComponent<Button>();
    upgradeImage = GetComponentInChildren<UpgradeImage>();
    upgradeLabel = GetComponentInChildren<UpgradeLabelComponent>();
    upgradeDesc = GetComponentInChildren<UpgradeDescriptionComponent>();
    initialized = true;
  }

  public void SetData(Upgrade upgrade, Action applyUpgradeCb)
  {
    if (!initialized)
    {
      Start();
    }
    upgradeImage.SetSprite(upgrade.image);
    upgradeLabel.SetText(upgrade.upgradeName);
    upgradeDesc.SetText(upgrade.upgradeDesc);
    button.onClick.RemoveAllListeners();
    button.onClick.AddListener(() =>
    {
      applyUpgradeCb();
    });
  }
}
