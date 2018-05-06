using System.Collections;
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
    public bool halted = false;
    public UnityEvent OnReset;

    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private Vector3 targetPosition;
    private Animator animator;

    public int LinesOfCode {
      get {
        return instructions.Count;
      }
    }
    public bool Alive {
      get {
        return alive;
      }
    }


    private void Awake() {
      this.originalPosition = this.transform.position;
      this.originalRotation = this.transform.rotation;
      this.animator = this.GetComponent<Animator>();
    }

    public void Begin() {
      //Parse instructions
      instructions = CommandProcessor.ParseCommands(instructionInput);
    }

    public void Tick() {
      if (alive && !halted) {
        if (repeatCount > 0) {
          Run(lastInstruction);
          repeatCount -= 1;
        } else if (instructionIndex < instructions.Count) {
          CommandProcessor.Command command = instructions[instructionIndex];
          if(command.instruction == "goto") {
            instructionIndex = command.arg0;
            repeatCount = 0;
          } else {
            Run(command.instruction);

            lastInstruction = instructions[instructionIndex].instruction;
            repeatCount = instructions[instructionIndex].arg0 - 1;
            instructionIndex += 1;  
          }
        }
      }
      DebugStateLabel();
    }

    public void Celebrate() {
      Halt();
      if(this.animator != null) {
        this.animator.SetBool("Celebrate",true);
      }
    }

    public void Halt() {
      moving = false;
      halted = true;
    }

    private void Run(string instruction) {
      if(instruction == "forward") {
        //TODO nicer movement
        this.targetPosition = this.transform.position + this.transform.forward * moveSpeed;
        moving = true;
      } else {
        //All other instructions stop moving
        moving = false;

        if (instruction == "right") {
          this.transform.Rotate(new Vector3(0f, 90f, 0f));
        }
        if (instruction == "left") {
          this.transform.Rotate(new Vector3(0f, -90f, 0f));
        }
        if (instruction == "halt") {
          Halt();
        }
      }
    }

    void Update() {
      if(alive && moving) {
        this.transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
      }
    }


    public void Die() {
      alive = false;
    }

    public void Reset() {
      this.transform.position = originalPosition;
      this.transform.rotation = originalRotation;
      if (this.animator != null) {
        this.animator.SetBool("Celebrate", false);
      }
      instructionIndex = 0;
      repeatCount = 0;
      alive = true;
      moving = false;
      halted = false;
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

    void OnDrawGizmosSelected() {
      if(moving) {
        Gizmos.DrawSphere(this.targetPosition, 0.3f);
      }
    }

#endregion


  }
}