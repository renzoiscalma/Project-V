using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesUIController : MonoBehaviour
{
  UpgradeChoiceButton[] upgradeButtons;
  [SerializeField] PauseManager pauseManager;

  public void Init()
  {
    upgradeButtons = GetComponentsInChildren<UpgradeChoiceButton>(true);
  }

  public void SetUpgradeButtons(List<Upgrade> upgrades, Action<Upgrade> applyUpgrade)
  {
    for (int i = 0; i < upgrades.Count; i++)
    {
      Upgrade upgrade = upgrades[i];
      upgradeButtons[i].SetData(upgrade, () =>
      {
        applyUpgrade(upgrade);
        Close();
        pauseManager.Unpause();
      });
    }
  }

  public void Open()
  {
    gameObject.SetActive(true);
    pauseManager.Pause();
  }

  public void Close()
  {
    gameObject.SetActive(false);
  }
}
