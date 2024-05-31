using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExpComponent : MonoBehaviour
{
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
  }

  public void AddExperience(float value)
  {
    Debug.Log("Obtained exp: " + value);
    currentExperience += value;
    while (currentExperience >= expToLevelUp)
    {
      Debug.Log("Level up! Current level: " + currentLevel);
      healthComponent.ApplyLevelUp();
      attackComponent.ApplyAtkUpgrade();
      currentExperience -= expToLevelUp;
      currentLevel++;
      expToLevelUp = getExpForLevelUp(currentLevel);
    }
    Debug.Log("Current EXP: " + currentExperience + "/" + expToLevelUp);
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
