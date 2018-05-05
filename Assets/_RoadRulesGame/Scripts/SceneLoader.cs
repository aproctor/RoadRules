using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader {

  public const int NUM_SCENES = 3;
  public static int curSceneIndex = 0;

  public static void LoadNextScene() {
    curSceneIndex = (curSceneIndex + 1) % NUM_SCENES;
    SceneManager.LoadScene(curSceneIndex);
  }

  public static void LoadSplash() {
    curSceneIndex = 0;
    SceneManager.LoadScene(curSceneIndex);
  }
}
