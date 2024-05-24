using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMove : MonoBehaviour
{
  [SerializeField] float speed = 3f;
  [SerializeField] float attackSpeed = 1f;
  [SerializeField] float rollForce = 10f;
  Rigidbody2D rgbd2d;
  Vector3 movementVector;
  Transform spriteTransform;
  BoxCollider2D boxColliderComponent;
  Animate animate;
  private bool attacking = false;
  private bool rolling = false;
  private bool shouldRoll = false;
  private bool moving = false;
  private Vector2 rollVelocity = Vector2.zero;
  private bool facingRight = true;
  private void Awake()
  {
    rgbd2d = GetComponent<Rigidbody2D>();
    animate = GetComponent<Animate>();
    boxColliderComponent = GetComponent<BoxCollider2D>();
    spriteTransform = transform.GetChild(0);
    movementVector = new Vector3();
  }
  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    HandleActions();
    if (!rolling)
    {
      HandleMovements();
      HandleAttacking();
    }
    HandleAnimations();
  }

  void HandleAttacking()
  {
    if (Input.GetMouseButton(0))
    {
      attacking = true;
      facingRight = Input.mousePosition.x > Screen.width / 2f;
    }
    if (Input.GetMouseButtonUp(0))
    {
      attacking = false;
    }
  }

  void HandleActions()
  {
    if (Input.GetKeyDown(KeyCode.Space))
    {
      shouldRoll = true;
      movementVector.x = Input.GetAxisRaw("Horizontal");
      movementVector.y = Input.GetAxisRaw("Vertical");
      // allow dodging when not pressing any direction input
      if (movementVector.x == 0 && movementVector.y == 0)
      {
        rollVelocity = facingRight ? new(1, 0) : new(-1, 0);
      }
      else
      {
        rollVelocity = new(movementVector.x, movementVector.y);
      }
    }
    if (rolling)
    {
      Debug.Log(facingRight);
      facingRight = rollVelocity.x > 0;
      rgbd2d.velocity = rollVelocity * rollForce;
    }
  }

  void HandleMovements()
  {
    movementVector.x = Input.GetAxisRaw("Horizontal");
    movementVector.y = Input.GetAxisRaw("Vertical");
    if (movementVector.x > 0 || movementVector.y > 0 ||
      movementVector.y < 0 || movementVector.x < 0)
    {
      facingRight = movementVector.x > 0;
      moving = true;
    }
    else
    {
      moving = false;
    }
    rgbd2d.velocity = movementVector * speed;
  }

  private void HandleAnimations()
  {
    spriteTransform.rotation = facingRight ? new(0, 0, 0, 0) : new(0, 180, 0, 0);
    animate.moving = moving;
    if (shouldRoll)
    {
      animate.Roll();
      shouldRoll = false;
    }
    animate.attacking = attacking;
  }

  public void SetHitbox(bool enabled)
  {
    boxColliderComponent.enabled = enabled;
  }

  public void SetRolling(bool rolling)
  {
    this.rolling = rolling;
  }
}
