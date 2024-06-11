using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballManager : WeaponThrowableBase
{
  [SerializeField] GameObject fireballPrefab;
  [SerializeField] public float chancePercent;
  [SerializeField] public float aoePercent;
  public PlayerMove playerMove;
  void Awake()
  {
    playerMove = transform.parent.parent.GetComponent<PlayerMove>();
  }
  public override void ManualAttackTrigger()
  {
    if (Random.Range(0f, 100) <= chancePercent)
    {
      GameObject fbObj = Instantiate(fireballPrefab);
      fbObj.transform.position = transform.position;
      fbObj.GetComponent<Fireball>().Init(damage, aoePercent, playerMove.facingRight, timeToLive);
    }
  }

  public void LevelUp()
  {
    damage += 3f;
    aoePercent += 0.7f;
    chancePercent += 5f;
    Debug.Log("Applying fireball upgrade!");
  }
}
