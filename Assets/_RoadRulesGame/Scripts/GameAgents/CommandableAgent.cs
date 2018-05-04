using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RoadRules {

  public class CommandableAgent : MonoBehaviour {

    [TextArea]
    public string instructionInput;
    private int instructionIndex = 0;
    private ArrayList instructions;

    [Header("Triggers")]
    public UnityEvent OnReset;
    public UnityEvent OnDie;

    private Vector3 originalPosition;
    private Quaternion originalRotation;

    private void Awake() {
      this.originalPosition = this.transform.position;
      this.originalRotation = this.transform.rotation;
      Begin();
    }

    public void Begin() {
      //Parse instructions
      string[] lines = instructionInput.Split(new char[] { '\n' });
      instructions = new ArrayList();
      foreach (string line in lines) {        
        string trimmed = line.Trim().ToLower();
        if(trimmed.Length == 0 || trimmed.StartsWith("#")) {
          continue;
        }
        if (CommandProcessor.ValidateInstruction(trimmed)) {
          instructions.Add(trimmed);
        } else {
          Debug.LogWarningFormat("Ignoring invalid instruction <{0}>",trimmed);
        }
      }
      Debug.Log("Lines " + instructions.Count);
    }


    public void Die() {
      OnDie.Invoke();
    }

    public void Reset() {
      this.transform.position = originalPosition;
      this.transform.rotation = originalRotation;
      instructionIndex = 0;
      OnReset.Invoke();
    }

  }
}