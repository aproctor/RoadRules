using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

namespace RoadRules {
  public static class CommandProcessor {

    public static Regex[] COMMAND_LIST = {
      new Regex(@"^forward( -?\d+)?$"),
      new Regex(@"^wait( -?\d+)?$"),
      new Regex("^right$"),
      new Regex("^left$")
    };

    public static bool ValidateInstruction(string instruction) {
      Debug.LogFormat("Validating: {0}",instruction);
      foreach (Regex command in COMMAND_LIST) {
        Match match = command.Match(instruction);
        if (match != null && match.Success) {
          return true;
        }
      }
      return false;
    }


  }
}