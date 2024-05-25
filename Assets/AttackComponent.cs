using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackComponent : MonoBehaviour
{
  [SerializeField] float attackDamage = 8;
  PlayerMove playerMove;
  public bool attacking = false;

  void Awake()
  {
    playerMove = GetComponent<PlayerMove>();
  }

  // Update is called once per frame
  void Update()
  {
    if (!playerMove.rolling)
    {
      HandleAttacking();
    }
  }

  void HandleAttacking()
  {
    if (Input.GetMouseButton(0))
    {
      playerMove.facingRight = Input.mousePosition.x > Screen.width / 2f;
      attacking = true;
    }
    if (!Input.GetMouseButton(0))
    {
      attacking = false;
    }
  }

  public void DamageObject(GameObject targetGameObject)
  {
    targetGameObject.GetComponent<HealthComponent>().TakeDamage(attackDamage);
  }
}
