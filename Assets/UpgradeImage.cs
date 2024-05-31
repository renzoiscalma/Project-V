using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeImage : MonoBehaviour
{
  Image image;

  void Awake()
  {
    image = GetComponent<Image>();
  }

  public void SetSprite(Sprite sprite)
  {
    image.sprite = sprite;
  }
}
