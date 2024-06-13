using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ExperienceComponent : MonoBehaviour
{
  [SerializeField] public float value = 1;
  [SerializeField] float speed = 6f; // velocity multiplier
  private Transform target = null;
  private Rigidbody2D rb2d;
  private float collectTimer = 0.3f;
  private BoxCollider2D boxCollider2D;
  void Awake()
  {
    rb2d = GetComponent<Rigidbody2D>();
    boxCollider2D = GetComponent<BoxCollider2D>();
  }

  // Update is called once per frame
  void Update()
  {
    if (target != null)
    {
      Vector3 direction = (target.position - transform.position).normalized;
      rb2d.velocity = speed * direction;
    }
    collectTimer -= Time.deltaTime;
    if (collectTimer < 0)
    {
      boxCollider2D.enabled = true;
    }
  }

  public void GoToTarget(Transform target)
  {
    this.target = target;
  }

  public float Collect()
  {
    Destroy(transform.gameObject);
    return value;
  }

  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.CompareTag("Player"))
    {
      other.gameObject.GetComponent<PlayerExpComponent>().AddExperience(Collect());
    }
  }
}
