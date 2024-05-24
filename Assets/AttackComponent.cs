using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackComponent : MonoBehaviour
{
  [SerializeField] float attackDamage = 8;
  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }

  public void Attack(GameObject targetGameObject)
  {
    Debug.Log("Attacking target: " + targetGameObject.name.ToString());
    targetGameObject.GetComponent<HealthComponent>().TakeDamage(attackDamage);
  }
}
