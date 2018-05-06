using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public static class SceneLoader {

  public const int NUM_SCENES = 4;
  public static int curSceneIndex = 0;

  public static void LoadNextScene() {
    curSceneIndex = (curSceneIndex + 1) % NUM_SCENES;
    SceneManager.LoadScene(curSceneIndex);

  }

  public static void LoadSplash() {
    curSceneIndex = 0;
    SceneManager.LoadScene(curSceneIndex);
  }

  public static string CurrentSceneName() {
    //Forgive me for what i'm about to do
    string path = SceneUtility.GetScenePathByBuildIndex(curSceneIndex);
    Match match = new Regex(@"\d_(.*)\.unity").Match(path);
    if(match.Success && match.Captures.Count > 0){
      return match.Groups[1].Value;
    }

    return "Road Rules";
  }
}
