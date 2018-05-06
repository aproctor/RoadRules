using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public static class SceneLoader {

  public const int NUM_SCENES = 6;
  public static int curSceneIndex = 0;

  public struct HighScore {
    public string levelName;
    public int ticks, linesOfCode, fatalities;

    public HighScore(string level, int t, int loc, int f) {
      levelName = level;
      ticks = t;
      linesOfCode = loc;
      fatalities = f;
    }
  }

  public static List<HighScore> highScores;

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

  public static void ClearScores() {
    highScores = new List<HighScore>();
  }

  public static void LogScore(string level, int numTicks, int linesOfCode, int fatalities) {
    //TODO maybe use a hash for score overwriting, but keeping things simple for rendering later
    highScores.Add(new HighScore(level, numTicks,linesOfCode,fatalities));
  }
}
