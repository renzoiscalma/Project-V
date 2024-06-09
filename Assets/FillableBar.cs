using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FillableBar : MonoBehaviour
{
  float value;
  float max;
  Image image;
  [SerializeField] TextMeshProUGUI text;

  public void Init(float value, float max)
  {
    image = GetComponent<Image>();
    this.value = value;
    this.max = max;
    image.fillAmount = value / max;
    UpdateText();
  }

  public void SetMaxValue(float max)
  {
    this.max = max;
    image.fillAmount = value / this.max;
    UpdateText();
  }

  public void SetValue(float value)
  {
    this.value = value;
    image.fillAmount = value / this.max;
    UpdateText();
  }

  void UpdateText()
  {
    if (text)
    {
      text.SetText(this.value.ToString("F0") + "/" + this.max.ToString("F0"));
    }
  }
}
