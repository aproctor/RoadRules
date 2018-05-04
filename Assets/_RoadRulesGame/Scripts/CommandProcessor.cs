using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

namespace RoadRules {
  public static class CommandProcessor {

    public static string HALT = "halt";
    private static char[] SPACE_SPLIT = { ' ' };

    public static Regex[] COMMAND_LIST = {
      new Regex(@"^forward( -?\d+)?$"),
      new Regex(@"^wait( -?\d+)?$"),
      new Regex("^right$"),
      new Regex("^left$")
    };

    public class Command {
      public string instruction = null;
      public int arg0 = 0;

      public Command(string commandStr) {
        string[] parts = commandStr.Split(SPACE_SPLIT);
        instruction = parts[0];
        if(parts.Length > 1) {
          arg0 = System.Convert.ToInt32(parts[1]);
        }
      }
    }

    public static bool ValidateInstruction(string instruction) {
      Debug.LogFormat("Validating: {0}",instruction);
      return ParseCommand(instruction) != null;
    }

    public static Command ParseCommand(string instruction) {
      foreach (Regex command in COMMAND_LIST) {
        Match match = command.Match(instruction);
        if (match != null && match.Success) {
          return new Command(instruction);
        }
      }

      return null;
    }


  }
}