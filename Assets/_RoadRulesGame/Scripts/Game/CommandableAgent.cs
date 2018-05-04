using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RoadRules {

  public class CommandableAgent : MonoBehaviour {

    private static char[] NEW_LINE_SPLIT = { '\n' };

    public float moveSpeed = 1f;

    [Header("Instructions")]
    [TextArea]
    public string instructionInput;
    public int instructionIndex = 0;  //Look but don't touch
    private string lastInstruction = "";
    private int repeatCount = 0; //How many times we'll repeat the last instruction
    private List<CommandProcessor.Command> instructions;

    [Header("Statefullness")]
    private bool alive = true;
    public UnityEvent OnReset;
    public UnityEvent OnDie;

    private Vector3 originalPosition;
    private Quaternion originalRotation;

    private void Awake() {
      this.originalPosition = this.transform.position;
      this.originalRotation = this.transform.rotation;

      //TODO call this from an external method
      Begin();
    }

    public void Begin() {
      //Parse instructions
      string[] lines = instructionInput.Split(NEW_LINE_SPLIT);
      instructions = CommandProcessor.ParseCommands(instructionInput);
    }

    public void Tick() {
      if (alive) {
        if (repeatCount > 0) {
          Run(lastInstruction);
        } else if (instructionIndex < instructions.Count) {
          Run(instructions[instructionIndex].instruction);

          lastInstruction = instructions[instructionIndex].instruction;
          repeatCount = instructions[instructionIndex].arg0;
          instructionIndex += 1;
        } else {
          Debug.Log("Done instructions waiting", this);
        }
      }
    }

    private void Run(string instruction) {
      
    }


    public void Die() {
      alive = false;
      OnDie.Invoke();
    }

    public void Reset() {
      this.transform.position = originalPosition;
      this.transform.rotation = originalRotation;
      instructionIndex = 0;
      repeatCount = 0;
      alive = true;
      OnReset.Invoke();
    }

  }
}