using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RoadRules {

  public class InstructionsInputUI : MonoBehaviour {

    public CommandableAgent targettedAgent;

    [Header("Object Links")]
    public InputField inputScript;
    public Button playButton;
    public Button stopButton;
    public PuzzleMaster puzzleMaster;

    void Awake() {
      this.inputScript.text = targettedAgent.instructionInput;
    }

    public void StopClick() {      
      this.playButton.interactable = true;
      this.stopButton.interactable = false;

      this.puzzleMaster.Reset();
    }

    public void PlayClick() {
      this.playButton.interactable = false;
      this.stopButton.interactable = true;

      //TODO apply instruction changes on input rather than on play
      ApplyScript();

      this.puzzleMaster.Play();
    }

    private void ApplyScript() {
      this.targettedAgent.instructionInput = this.inputScript.text;
    }
  }

}
