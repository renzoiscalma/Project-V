using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
  public bool paused = false;
  public void Pause()
  {
    paused = true;
    Time.timeScale = 0f;
  }
  public void Unpause()
  {
    paused = false;
    Time.timeScale = 1f;
  }

  public void Update()
  {
    if (Input.GetKeyUp(KeyCode.P))
    {
      if (paused) Unpause();
      else Pause();
    }
  }
}
