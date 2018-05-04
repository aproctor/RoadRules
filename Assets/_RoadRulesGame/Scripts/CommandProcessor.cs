using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

namespace RoadRules {
  public static class CommandProcessor {

    private static char[] NEW_LINE_SPLIT = { '\n' };
    public static Command HALT = new CommandProcessor.Command("halt");
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

    public static bool ValidateCommand(string instruction) {
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

    public static List<Command> ParseCommands(string instructionInput) {
      List<Command> instructions = new List<CommandProcessor.Command>();

      string[] lines = instructionInput.Split(NEW_LINE_SPLIT);
      foreach (string line in lines) {
        string trimmed = line.Trim().ToLower();
        if (trimmed.Length == 0 || trimmed.StartsWith("#")) {
          continue;
        }
        Command command = ParseCommand(trimmed);
        if (command != null) {
          instructions.Add(command);
        } else {
          Debug.LogWarningFormat("Ignoring invalid instruction <{0}>", trimmed);
          instructions.Add(CommandProcessor.HALT);
        }
      }

      return instructions;
    }


  }
}