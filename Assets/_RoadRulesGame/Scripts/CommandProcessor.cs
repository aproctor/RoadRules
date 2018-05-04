using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

namespace RoadRules {
  public static class CommandProcessor {

    public static Regex[] COMMAND_LIST = {
    new Regex("forward"),
    new Regex("wait"),
  };

    public static bool ValidateInstruction(string instruction) {
      foreach (Regex command in COMMAND_LIST) {
        Match match = command.Match(instruction);
        if (match != null) {
          return true;
        }
      }
      return false;
    }

  }
}