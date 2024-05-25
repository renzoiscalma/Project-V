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
  public bool rolling = false;
  public bool shouldRoll = false;
  public bool moving = false;
  public bool facingRight = true;
  private Vector2 rollVelocity = Vector2.zero;
  private void Awake()
  {
    rgbd2d = GetComponent<Rigidbody2D>();
    boxColliderComponent = GetComponent<BoxCollider2D>();
    spriteTransform = transform.GetChild(0);
    movementVector = new Vector3();
  }
  // Update is called once per frame
  void Update()
  {
    HandleActions();
    if (!rolling)
    {
      HandleMovements();
    }
  }
  void HandleActions()
  {
    if (Input.GetKeyDown(KeyCode.Space) && !rolling)
    {
      shouldRoll = true;
      movementVector.x = Input.GetAxisRaw("Horizontal");
      movementVector.y = Input.GetAxisRaw("Vertical");
      // allow dodging when not pressing any direction input
      if (movementVector.x == 0 && movementVector.y == 0)
      {
        rollVelocity = new(facingRight ? 1 : -1, 0);
      }
      else
      {
        rollVelocity = new(movementVector.x, movementVector.y);
      }
    }
    if (rolling)
    {
      facingRight = rollVelocity.x == 0 ? facingRight : rollVelocity.x > 0;
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
      if (movementVector.x != 0)
      {
        facingRight = movementVector.x > 0;
      }
      moving = true;
    }
    else
    {
      moving = false;
    }
    rgbd2d.velocity = movementVector * speed;
  }

  public void SetHitbox(bool enabled)
  {
    boxColliderComponent.enabled = enabled;
  }
}
