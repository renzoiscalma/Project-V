using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
  [SerializeField] public int bounces;
  [SerializeField] public GameObject target;
  private ChainLightning cl;
  private Transform sourcePosition;
  private List<GameObject> previouslyHit;
  private SpriteRenderer spriteRenderer;
  private List<GameObject> targets;
  private CircleCollider2D coll;
  private int bouncesRemaining;
  private bool fired = false; // checks if the lightning has a target for a single tick.
  public void Init(Transform sourcePosition, GameObject target, List<GameObject> previouslyHit, ChainLightning cl, int bouncesRemaining)
  {
    this.sourcePosition = sourcePosition;
    this.target = target;
    this.previouslyHit = previouslyHit;
    this.cl = cl;
    this.bouncesRemaining = bouncesRemaining;
    targets = new List<GameObject>();
    // add current target to target list
    target.GetComponent<HealthComponent>().TakeDamage(cl.damage);
    previouslyHit.Add(target);
    UpdateSprite();
  }

  public void UpdateSprite()
  {
    if (gameObject == null || sourcePosition.gameObject == null) return;
    // get distance and resize lightning to target
    transform.SetPositionAndRotation(sourcePosition.transform.position, Quaternion.identity);
    float distance = Vector2.Distance(transform.position, target.transform.position);
    spriteRenderer.size = new(spriteRenderer.size.x, distance);
    // rotate vector towards target
    Vector3 vectorToTarget = sourcePosition.position - target.transform.position;
    float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg + 90; // idk why i have to add 90 here
    transform.Rotate(new(0, 0, 1), angle);
    // move collision to edge of lightning
    coll.offset = new Vector2(coll.offset.x, distance);
  }
  void Awake()
  {
    spriteRenderer = GetComponent<SpriteRenderer>();
    coll = GetComponent<CircleCollider2D>();
  }

  // Update is called once per frame
  void Update()
  {
    if (target == null)
    {
      Destroy(gameObject);
    }
  }

  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.CompareTag("Monsters"))
    {
      if (!targets.Contains(other.gameObject))
      {
        targets.Add(other.gameObject);
      }
    }
  }

  void OnTriggerExit2D(Collider2D other)
  {
    if (other.gameObject.CompareTag("Monsters"))
    {
      if (targets.Contains(other.gameObject))
      {
        targets.Remove(other.gameObject);
      }
    }
  }

  void OnTriggerStay2D(Collider2D other)
  {
    if (!fired && bouncesRemaining > 0)
    {
      GameObject monster = null;
      for (int i = 0; i < targets.Count; i++)
      {
        if (previouslyHit.Contains(targets[i])) continue;
        monster = targets[i];
        break;
      }
      if (monster != null)
      {
        GameObject lightGameObject = Instantiate(cl.lightning);
        Lightning lightning = lightGameObject.GetComponent<Lightning>();
        lightning.Init(target.transform, monster, previouslyHit, cl, bouncesRemaining - 1);
      }
    }
    fired = true;
  }

  void OnAnimationEnd()
  {
    if (fired)
    {
      Destroy(gameObject);
    }
  }
}
