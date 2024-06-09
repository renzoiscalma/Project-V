using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDeathAnimationFinished : MonoBehaviour
{
  [SerializeField] RestartManager restartManager;

  public void handleDeathAnimationFinished()
  {
    restartManager.GameOver();
  }
}
