using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TimeComponent : MonoBehaviour
{
  [SerializeField] TextMeshProUGUI timeText;
  float timeElapsed;

  void Update()
  {
    timeElapsed += Time.deltaTime;
    float minutes = Mathf.FloorToInt(timeElapsed / 60);
    float seconds = Mathf.FloorToInt(timeElapsed % 60);
    timeText.SetText(string.Format("{0:00}:{1:00}", minutes, seconds));
  }
}
