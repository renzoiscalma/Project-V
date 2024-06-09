using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManagerComponent : MonoBehaviour
{
  public Upgrade[] possibleUpgrades;
  public List<Upgrade> currentUpgrades;
  GameObject player;
  [SerializeField] UpgradesUIController upgradesUi;
  [SerializeField] WeaponManager weaponManager;
  // Start is called before the first frame update
  void Start()
  {
    currentUpgrades = new List<Upgrade>();
    upgradesUi.Init();
  }

  // Update is called once per frame
  void Update()
  {

  }

  public void GenerateUpgradesAndShowUI(GameObject player)
  {
    this.player = player;
    upgradesUi.Open();
    upgradesUi.SetUpgradeButtons(GenerateUpgradesToChoose(), applyUpgrade);
  }

  List<Upgrade> GenerateUpgradesToChoose(int count = 3)
  {
    List<int> randIdx = generateRandomIndxs(count, 0, possibleUpgrades.Length);
    List<Upgrade> upgradesToChoose = new();
    for (int i = 0; i < count; i++)
    {
      upgradesToChoose.Add(possibleUpgrades[randIdx[i]]);
    }
    return upgradesToChoose;
  }

  public void applyUpgrade(Upgrade upgrade)
  {
    Upgrade existingUpgrade = FindUpgrade(upgrade);
    if (existingUpgrade != null)
    {
      existingUpgrade.ApplyUpgrade();
    }
    else
    {
      Upgrade newUpgrade;
      if (upgrade.upgradeType == UpgradeType.Weapon)
      {
        newUpgrade = Instantiate(upgrade, weaponManager.transform);
      }
      else
      {
        newUpgrade = Instantiate(upgrade, transform);
      }
      newUpgrade.Init(player);
      newUpgrade.ApplyUpgrade();
      currentUpgrades.Add(newUpgrade);
    }
  }
  void GetRandomUpgrade()
  {
    int rNum = Random.Range(0, possibleUpgrades.Length);
    Upgrade existingUpgrade = FindUpgrade(possibleUpgrades[rNum]);
    if (existingUpgrade != null)
    {
      existingUpgrade.ApplyUpgrade();
    }
    else
    {
      Upgrade newUpgrade = Instantiate(possibleUpgrades[rNum], transform);
      newUpgrade.Init(player);
      newUpgrade.ApplyUpgrade();
      currentUpgrades.Add(newUpgrade);
    }
  }

  Upgrade FindUpgrade(Upgrade upgrade)
  {
    for (int i = 0; i < currentUpgrades.Count; i++)
    {
      if (currentUpgrades[i].GetType().Equals(upgrade.GetType()))
      {
        return currentUpgrades[i];
      }
    }
    return null;
  }

  private static List<int> generateRandomIndxs(int count, int min, int max)
  {
    List<int> possibleNumbers = new List<int>();
    List<int> chosenNumbers = new List<int>();

    for (int index = min; index < max; index++)
      possibleNumbers.Add(index);

    while (chosenNumbers.Count < count)
    {
      int position = Random.Range(0, possibleNumbers.Count);
      chosenNumbers.Add(possibleNumbers[position]);
      possibleNumbers.RemoveAt(position);
    }
    return chosenNumbers;
  }
}
