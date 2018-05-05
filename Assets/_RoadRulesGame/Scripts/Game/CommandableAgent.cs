﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RoadRules {

  public class CommandableAgent : MonoBehaviour {

    public float moveSpeed = 1f;


    [Header("Instructions")]
    [TextArea]
    public string instructionInput;
    public int instructionIndex = 0;  //Look but don't touch
    private string lastInstruction = "";
    public int repeatCount = 0; //How many times we'll repeat the last instruction
    private List<CommandProcessor.Command> instructions;

    [Header("Statefullness")]
    private bool alive = true;
    private bool moving = false;
    public UnityEvent OnReset;

    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private Rigidbody _rigidbody;
    private Vector3 targetPosition;

    private void Awake() {
      this.originalPosition = this.transform.position;
      this.originalRotation = this.transform.rotation;
    }

    public void Begin() {
      //Parse instructions
      instructions = CommandProcessor.ParseCommands(instructionInput);
    }

    public void Tick() {
      if (alive) {
        if (repeatCount > 0) {
          Run(lastInstruction);
          repeatCount -= 1;
        } else if (instructionIndex < instructions.Count) {
          Run(instructions[instructionIndex].instruction);

          lastInstruction = instructions[instructionIndex].instruction;
          repeatCount = instructions[instructionIndex].arg0 - 1;
          instructionIndex += 1;
        }
      }
      DebugStateLabel();
    }

    private void Run(string instruction) {
      if(instruction == "forward") {
        //TODO nicer movement
        this.targetPosition = this.transform.position + this.transform.forward;
        moving = true;
      } else {
        //All other instructions stop moving
        moving = false;


      }
    }

    void Update() {
      if(alive && moving) {
        this.transform.position = targetPosition;
        moving = false;
      }
    }


    public void Die() {
      alive = false;
    }

    public void Reset() {
      this.transform.position = originalPosition;
      this.transform.rotation = originalRotation;
      instructionIndex = 0;
      repeatCount = 0;
      alive = true;
      moving = false;
      OnReset.Invoke();
      DebugStateLabel();
    }



#region editor_tools
    public bool debugState = false;
    private string initialName = null;

    private void DebugStateLabel() {
#if UNITY_EDITOR
      if (debugState) {
        if (initialName == null) {
          initialName = this.gameObject.name;
        }
        if (alive) {
          this.gameObject.name = string.Format("{0} {1} [{2}]", initialName, lastInstruction, repeatCount);
        } else {
          this.gameObject.name = string.Format("{0} [dead]", initialName);
        }
      }

#endif
    }

#endregion


  }
}