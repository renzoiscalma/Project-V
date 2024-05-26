using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpCollectorComponent : MonoBehaviour
{
  void OnTriggerEnter2D(Collider2D collider)
  {
    if (collider.gameObject.CompareTag("Experience"))
    {
      collider.gameObject.GetComponent<ExperienceComponent>().GoToTarget(transform);
    }
  }
  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }
}
