using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackComponent : MonoBehaviour
{
  [SerializeField] float attackDamage = 8;
  [SerializeField] public float attackSpeedMultiplier = 0.8f; // multiplier for attack animation
  [SerializeField] public float attackRadius = 0; // % increase in attack radius
  private Vector2 baseAttackRangeOffset = Vector2.zero;
  private Vector2 baseAttackRangeSize = Vector2.zero;

  PlayerMove playerMove;
  public bool attacking = false;

  void Awake()
  {
    playerMove = GetComponent<PlayerMove>();
    var swordCollider = transform.GetChild(0).GetComponentInChildren<BoxCollider2D>();
    baseAttackRangeOffset = swordCollider.offset;
    baseAttackRangeSize = swordCollider.size;
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
    var targetHealthComponent = targetGameObject.GetComponent<HealthComponent>();
    if (targetHealthComponent != null)
    {
      targetGameObject.GetComponent<HealthComponent>().TakeDamage(attackDamage);
    }
  }

  public void IncreaseAttackRadius(float radius)
  {
    attackRadius += radius;
    var swordCollider = transform.GetChild(0).GetComponentInChildren<BoxCollider2D>();
    var newSwordColliderOffsetX = attackRadius * baseAttackRangeSize.x;
    var newSwordColliderOffsetY = attackRadius * baseAttackRangeSize.y;
    swordCollider.offset = new(baseAttackRangeSize.x + (newSwordColliderOffsetX / 2),
      baseAttackRangeOffset.y + (newSwordColliderOffsetY / 2));
    swordCollider.size = new(baseAttackRangeSize.x + newSwordColliderOffsetX,
      baseAttackRangeSize.y + newSwordColliderOffsetY);
  }
}
