using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RoadRules {

  public class InstructionsInputUI : MonoBehaviour {

    private const float MIN_TIME_SCALE = 1f;
    private const float MAX_TIME_SCALE = 5f;
    private const float TIME_SCALE_INTERVAL = 1f;

    public CommandableAgent targettedAgent;

    public float caratJumpHeight = -15f;

    public bool playing = false;

    public Color playingColor;
    public Color haltedColor;

    [Header("Object Links")]
    public InputField inputScript;
    public Button playButton;
    public Button stopButton;
    public PuzzleMaster puzzleMaster;
    public RectTransform caratRoot;
    public Text caratLabel;
    public Text titleLabel;

    public Button lessTimeButton;
    public Button moreTimeButton;
    public Text timeScaleLabel;

    public GameObject gameOverUi;
    public GameObject helpSection;

    void Awake() {
      this.inputScript.text = targettedAgent.instructionInput;
      this.titleLabel.text = SceneLoader.CurrentSceneName();
      RenderTimeUI();
    }

    void Update() {
      if(playing) {
        //Update carat position
        this.caratRoot.anchoredPosition = new Vector2(caratJumpHeight, (this.targettedAgent.instructionIndex-1) * caratJumpHeight);
        if(this.targettedAgent.repeatCount > 0) {
          this.caratLabel.text = this.targettedAgent.repeatCount.ToString();
        } else {
          this.caratLabel.text = "";
        }
        if(this.targettedAgent.halted) {
          this.caratRoot.GetComponent<Image>().color = haltedColor;
        } else {
          this.caratRoot.GetComponent<Image>().color = playingColor;
        }

      }
    }

    public void PuzzleCompleted() {
      this.playButton.interactable = false;
      this.stopButton.interactable = false;

      this.gameOverUi.SetActive(true);
    }

    public void StopClick() {      
      this.playButton.interactable = true;
      this.stopButton.interactable = false;
      this.caratRoot.gameObject.SetActive(false);

      playing = false;
      this.puzzleMaster.Reset();
    }

    public void PlayClick() {
      this.playButton.interactable = false;
      this.stopButton.interactable = true;
      this.caratRoot.gameObject.SetActive(true);

      //TODO apply instruction changes on input rather than on play
      ApplyScript();

      playing = true;
      this.puzzleMaster.Play();
    }

    public void HelpButtonClicked() {
      this.helpSection.SetActive(!this.helpSection.activeSelf);
    }

    private void ApplyScript() {
      this.targettedAgent.instructionInput = this.inputScript.text;
    }

    public void NextPuzzleClicked() {
      SceneLoader.LoadNextScene();
    }

    public void ExitGameClicked() {
      SceneLoader.LoadSplash();
    }

    public void MoreTimeClicked() {
      Time.timeScale = Mathf.Clamp(Time.timeScale + TIME_SCALE_INTERVAL, MIN_TIME_SCALE, MAX_TIME_SCALE);
      RenderTimeUI();  
    }
    public void LessTimeClicked() {
      Time.timeScale = Mathf.Clamp(Time.timeScale - TIME_SCALE_INTERVAL, MIN_TIME_SCALE, MAX_TIME_SCALE);
      RenderTimeUI();
    }
    private void RenderTimeUI() {
      moreTimeButton.interactable = (Time.timeScale < MAX_TIME_SCALE);
      lessTimeButton.interactable = (Time.timeScale > MIN_TIME_SCALE);
      timeScaleLabel.text = string.Format("{0,0}x", Time.timeScale);
    }
  }

}
