using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultsScene : MonoBehaviour {

  public bool debugScores = false;

  [Header("Object Links")]
  public Text locText;
  public Text fatalitiesText;
  public Text timeText;

  void Start() {
#if UNITY_EDITOR
    if(debugScores) {
      SceneLoader.ClearScores();
      SceneLoader.LogScore("test 1", 10, 4, 0);
      SceneLoader.LogScore("test 2", 7, 7, 3);
      SceneLoader.LogScore("test 3", 20, 40, 2);
      SceneLoader.LogScore("test 4", 3, 89, 1);
    }
#endif
    RenderHighScores();
  }

  void RenderHighScores() {
    string locBuffer = "#Lines of Code\n\n";
    string fatBuffer = "#Fatalities\n\n";
    string tickBuffer = "#Time\n\n";

    int totalLoc = 0;
    int totalFat = 0;
    int totalTick = 0;

    //Safety check for editor debugging
    if (SceneLoader.highScores != null) {
      foreach (SceneLoader.HighScore score in SceneLoader.highScores) {
        locBuffer = locBuffer + string.Format("{0}: {1}\n", score.levelName, score.linesOfCode);
        fatBuffer = fatBuffer + string.Format("{0}: {1}\n", score.levelName, score.fatalities);
        tickBuffer = tickBuffer + string.Format("{0}: {1}\n", score.levelName, score.ticks);

        totalLoc += score.linesOfCode;
        totalFat += score.fatalities;
        totalTick += score.ticks;
      }
    }

    locBuffer = locBuffer + "\ntotal: " + totalLoc;
    fatBuffer = fatBuffer + "\ntotal: " + totalFat;
    tickBuffer = tickBuffer + "\ntotal: " + totalTick;

    locText.text = locBuffer;
    fatalitiesText.text = fatBuffer;
    timeText.text = tickBuffer;
  }

  public void NextButtonClicked() {
    SceneLoader.LoadSplash();
  }
}
