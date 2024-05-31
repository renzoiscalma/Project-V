using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFlashComponent : MonoBehaviour
{
  [SerializeField] private Color flashColor = Color.white;
  [SerializeField] private float flashTime = 0.25f;
  private SpriteRenderer spriteRenderer;
  private Material material;
  private Coroutine dmgFlashCoroutine;
  // Start is called before the first frame update
  void Awake()
  {
    spriteRenderer = GetComponent<SpriteRenderer>();
    material = spriteRenderer.material;
  }

  public void DamageFlash()
  {
    dmgFlashCoroutine = StartCoroutine(DamageFlasher());
  }

  private IEnumerator DamageFlasher()
  {
    material.SetColor("_FlashColor", flashColor);
    float currentFlashAmount = 0f;
    float elapsedTime = 0f;
    while (elapsedTime < flashTime)
    {
      elapsedTime += Time.deltaTime;
      // lerp flash amount
      currentFlashAmount = Mathf.Lerp(1f, 0f, elapsedTime / flashTime);
      material.SetFloat("_FlashAmount", currentFlashAmount);
      yield return null;
    }
  }
}
