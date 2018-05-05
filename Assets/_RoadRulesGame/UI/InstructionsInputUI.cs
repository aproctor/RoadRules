using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RoadRules {

  public class InstructionsInputUI : MonoBehaviour {

    public CommandableAgent targettedAgent;

    public float caratJumpHeight = -15f;

    public bool playing = false;

    [Header("Object Links")]
    public InputField inputScript;
    public Button playButton;
    public Button stopButton;
    public PuzzleMaster puzzleMaster;
    public RectTransform caratRoot;
    public Text caratLabel;

    void Awake() {
      this.inputScript.text = targettedAgent.instructionInput;
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

      }
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

    private void ApplyScript() {
      this.targettedAgent.instructionInput = this.inputScript.text;
    }
  }

}
