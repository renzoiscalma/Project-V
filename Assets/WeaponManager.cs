using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  // void Update()
  // {

  // }

  public void TriggerAllManualWeaponAttacks()
  {
    var weapons = GetComponentsInChildren<WeaponThrowableBase>();
    foreach (var weapon in weapons)
    {
      if (weapon)
      {
        weapon.ManualAttackTrigger();
      }
    }
  }
}
