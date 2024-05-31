using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackComponent : MonoBehaviour
{
  [SerializeField] public float attackDamage = 8;
  [SerializeField] public float attackSpeedMultiplier = 0.8f; // multiplier for attack animation
  [SerializeField] public float attackRadius = 0; // % increase in attack radius
  [SerializeField] PauseManager pauseManager;
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
    if (pauseManager.paused) return;
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
    var newSwordColliderOffsetY = attackRadius * baseAttackRangeSize.y / 2;
    swordCollider.offset = new(baseAttackRangeSize.x + (newSwordColliderOffsetX / 2),
      baseAttackRangeOffset.y + (newSwordColliderOffsetY / 2));
    swordCollider.size = new(baseAttackRangeSize.x + newSwordColliderOffsetX,
      baseAttackRangeSize.y + newSwordColliderOffsetY);
    Debug.Log("Increasing attack aoe size " + swordCollider.size);
  }

  public void ApplyAtkUpgrade()
  {
    attackDamage += 2;
    Debug.Log("Adding damage... New AD: " + attackDamage);
  }

  public void ApplyAtkSpdUpgrade()
  {
    attackSpeedMultiplier += 0.5f;
    Debug.Log("Adding attackspeed... New AttackSpeed: " + attackSpeedMultiplier);
  }

  public void ApplyAttackSizeUpgrade()
  {
    IncreaseAttackRadius(0.05f);
  }
}
