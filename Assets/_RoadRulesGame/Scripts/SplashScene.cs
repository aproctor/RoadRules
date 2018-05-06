using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScene : MonoBehaviour {

  private void Update() {
    if (Input.GetKeyDown(KeyCode.Escape)) {
      Application.Quit();
    }
  }

  public void PlaygGame() {
    SceneLoader.ClearScores();
    SceneLoader.LoadNextScene();
  }
}
