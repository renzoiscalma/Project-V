using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class ArrowsComponent : WeaponThrowableBase
{
  // Start is called before the first frame update
  [SerializeField] GameObject arrowPrefab;
  void Start()
  {
    timeToLive = 2;
    damage = 6;
    nextAttackTime = 2;
  }
  public override void OnAttack()
  {
    GameObject arrowLeft = Instantiate(arrowPrefab);
    GameObject arrowRight = Instantiate(arrowPrefab);
    arrowLeft.GetComponent<Arrow>().ttl = arrowRight.GetComponent<Arrow>().ttl = timeToLive;

    arrowLeft.GetComponent<Arrow>().damage = damage;
    arrowRight.GetComponent<Arrow>().damage = damage;

    arrowLeft.transform.position = arrowRight.transform.position = transform.position;

    arrowRight.transform.Rotate(new Vector3(0, 0, 1), 90f);
    arrowLeft.transform.Rotate(new(0, 0, 1), -90f);

    arrowLeft.GetComponent<Rigidbody2D>().velocity = new(-10, 0);
    arrowRight.GetComponent<Rigidbody2D>().velocity = new(10, 0);
  }

  public void LevelUp()
  {
    damage += 5f;
    timeToLive += 0.3f;
    nextAttackTime -= 0.2f;
    if (nextAttackTime <= 0.1f)
    {
      nextAttackTime = 0.15f;
    }
    Debug.Log("Applying arrows upgrade!");
  }
}
