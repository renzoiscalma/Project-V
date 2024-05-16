using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMove : MonoBehaviour
{
  Rigidbody2D rgbd2d;
  Vector3 movementVector;
  SpriteRenderer spriteRenderer;
  [SerializeField] float speed = 3f;
  [SerializeField] float attackSpeed = 1f;

  Animate animate;
  private void Awake()
  {
    rgbd2d = GetComponent<Rigidbody2D>();
    animate = GetComponent<Animate>();
    spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    movementVector = new Vector3();
  }
  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    movementVector.x = Input.GetAxisRaw("Horizontal");
    movementVector.y = Input.GetAxisRaw("Vertical");
    if (movementVector.x > 0 || movementVector.y > 0 ||
      movementVector.y < 0 || movementVector.x < 0)
    {
      animate.moving = true;
      if (movementVector.x < 0)
      {
        spriteRenderer.flipX = true;
      }
      else if (movementVector.x > 0)
      {
        spriteRenderer.flipX = false;
      }
    }
    else
    {
      animate.moving = false;
    }
    rgbd2d.velocity = movementVector * speed;
  }
}
