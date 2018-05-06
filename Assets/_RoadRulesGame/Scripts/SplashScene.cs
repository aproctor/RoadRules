using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScene : MonoBehaviour {
	
  public void PlaygGame() {
    SceneLoader.ClearScores();
    SceneLoader.LoadNextScene();
  }
}
