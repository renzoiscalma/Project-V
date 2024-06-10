using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainLightning : WeaponThrowableBase
{
  [SerializeField] public GameObject lightning;
  [SerializeField] int bounces = 1;
  private Lightning rootLightning; // follows player
  private List<GameObject> possibleTargets;
  void Awake()
  {
    possibleTargets = new List<GameObject>();
  }

  public override void Update()
  {
    base.Update();
    if (rootLightning != null)
    {
      rootLightning.UpdateSprite();
    }
  }

  public override void OnAttack()
  {
    if (possibleTargets.Count == 0) return;
    Debug.Log("Attacking with chain lightning");
    // GameObject monster = collider.gameObject;
    // spawn a thunder
    GameObject monster = possibleTargets[Random.Range(0, possibleTargets.Count)];
    GameObject firstLightning = Instantiate(lightning);
    rootLightning = firstLightning.GetComponent<Lightning>();
    if (monster != null)
    {
      rootLightning.Init(transform.parent, monster, new List<GameObject>(), GetComponent<ChainLightning>(), bounces - 1);
    }
  }

  void OnTriggerExit2D(Collider2D collider)
  {
    if (possibleTargets.Contains(collider.gameObject))
    {
      possibleTargets.Remove(collider.gameObject);
    }
  }

  void OnTriggerEnter2D(Collider2D collider)
  {
    if (collider.gameObject.CompareTag("Monsters"))
    {
      if (!possibleTargets.Contains(collider.gameObject))
      {
        possibleTargets.Add(collider.gameObject);
      }
    }
  }

  public void LevelUp()
  {
    damage += 3f;
    bounces += 2;
    if (nextAttackTime > 0.1f)
    {
      nextAttackTime -= 0.2f;
    }
    Debug.Log("Applying lightning upgrade!");
  }
}
