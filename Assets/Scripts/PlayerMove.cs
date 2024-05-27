using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMove : MonoBehaviour
{
  [SerializeField] float speed = 3f;
  [SerializeField] float rollForce = 7f;
  [SerializeField] public float experience = 0;
  Rigidbody2D rgbd2d;
  Vector3 movementVector;
  BoxCollider2D boxColliderComponent;
  AttackComponent attackComponent;
  HealthComponent healthComponent;
  public bool rolling = false;
  public bool shouldRoll = false;
  public bool moving = false;
  public bool facingRight = true;
  private Vector2 rollVelocity = Vector2.zero;
  private void Awake()
  {
    rgbd2d = GetComponent<Rigidbody2D>();
    boxColliderComponent = GetComponent<BoxCollider2D>();
    attackComponent = GetComponent<AttackComponent>();
    healthComponent = GetComponent<HealthComponent>();
    movementVector = new Vector3();
  }
  // Update is called once per frame
  void Update()
  {
    if (healthComponent.dead) return;
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
      attackComponent.attacking = false;
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
      // don't updating facing if attacking,
      // we want the character to face to where it's attacking.
      if (movementVector.x != 0 && !attackComponent.attacking)
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
