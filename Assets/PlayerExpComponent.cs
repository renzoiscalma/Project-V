using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExpComponent : MonoBehaviour
{
  [SerializeField] UpgradeManagerComponent upgradeManager;
  [SerializeField] FillableBar expUI;
  public float currentExperience = 0;
  public float currentLevel = 1;
  public float expToLevelUp = getExpForLevelUp(1);
  private AttackComponent attackComponent;
  private HealthComponent healthComponent;
  // Start is called before the first frame update
  void Start()
  {
    attackComponent = GetComponent<AttackComponent>();
    healthComponent = GetComponent<HealthComponent>();
    expUI.Init(currentExperience, expToLevelUp);
  }

  public void AddExperience(float value)
  {
    currentExperience += value;
    expUI.SetValue(currentExperience);
    while (currentExperience >= expToLevelUp) // exp might overflow, just leaving this here
    {
      healthComponent.ApplyLevelUp();
      attackComponent.ApplyAtkUpgrade();
      currentExperience -= expToLevelUp;
      currentLevel++;
      expToLevelUp = getExpForLevelUp(currentLevel);
      upgradeManager.GenerateUpgradesAndShowUI(gameObject);
      expUI.SetValue(currentExperience);
      expUI.SetMaxValue(expToLevelUp);
    }
  }

  static float getExpForLevelUp(float level)
  {
    if (level < 20)
    {
      return (level * 10) - 5;
    }
    return (level * 13) - 6;
  }
}
