using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartManager : MonoBehaviour
{
  [SerializeField] Canvas gameOverUi;
  public bool gameOver = false;
  public void RestartGame()
  {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    gameObject.SetActive(false);
  }

  // used by event on player death animation
  public void GameOver()
  {
    gameOverUi.gameObject.SetActive(true);
  }
}
