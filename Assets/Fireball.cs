using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Fireball : MonoBehaviour
{
  [SerializeField] GameObject explosion;
  CircleCollider2D circleCollider2D;
  Rigidbody2D r2d;
  float damage;
  float scalePercent;
  float timeToLive;
  float timeAlive;

  public void Init(float damage, float scalePercent, bool facingRight, float timeToLive)
  {
    this.damage = damage;
    this.scalePercent = scalePercent;
    this.timeToLive = timeToLive;
    if (facingRight)
    {
      r2d.velocity = new(15, 0);
      transform.Rotate(new(0, 0, 1), 180);
    }
    else
    {
      r2d.velocity = new(-15, 0);
    }
  }
  void Awake()
  {
    circleCollider2D = GetComponent<CircleCollider2D>();
    r2d = GetComponent<Rigidbody2D>();
  }

  // Update is called once per frame
  void Update()
  {
    timeAlive += Time.deltaTime;
    if (timeAlive >= timeToLive)
    {
      Destroy(gameObject);
    }
  }

  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.CompareTag("Monsters"))
    {
      // spawn an explosion
      GameObject explosionGameObject = Instantiate(explosion);
      // divide this by 2 because we only need the half.
      float colliderOffsetX = circleCollider2D.offset.x / 2;
      explosionGameObject.transform.position = new(transform.position.x + colliderOffsetX, transform.position.y, 0);
      explosionGameObject.GetComponent<Explosion>().Init(damage, scalePercent);
      // kill this object
      Destroy(gameObject);
    }
  }
}
