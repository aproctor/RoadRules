using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

namespace RoadRules {
  [TestFixture]
  public class CommandProcessorTests {

    [Test]
    public void TestCommandsList() {
      Assert.True(CommandProcessor.ValidateCommand("forward"));
      Assert.True(CommandProcessor.ValidateCommand("forward 1"));
      Assert.True(CommandProcessor.ValidateCommand("forward -1"));
      Assert.True(CommandProcessor.ValidateCommand("forward 100"));
      Assert.False(CommandProcessor.ValidateCommand("forward 100 40"));
      Assert.False(CommandProcessor.ValidateCommand("forward qq"));

      Assert.True(CommandProcessor.ValidateCommand("wait"));
      Assert.True(CommandProcessor.ValidateCommand("wait 1"));
      Assert.True(CommandProcessor.ValidateCommand("wait -1"));
      Assert.True(CommandProcessor.ValidateCommand("wait 100"));
      Assert.False(CommandProcessor.ValidateCommand("wait 100 40"));
      Assert.False(CommandProcessor.ValidateCommand("wait qq"));

      Assert.True(CommandProcessor.ValidateCommand("left"));
      Assert.True(CommandProcessor.ValidateCommand("right"));

      Assert.True(CommandProcessor.ValidateCommand("goto 12"));
      Assert.False(CommandProcessor.ValidateCommand("goto -1"));
      Assert.False(CommandProcessor.ValidateCommand("goto"));

      Assert.False(CommandProcessor.ValidateCommand("fubar"));
    }

    [Test]
    public void TestBlobInput() {
      string input = "";
      Assert.True(CommandProcessor.ParseCommands(input).Count == 0);

      input = "#it doesn't look like anything to me\n";
      Assert.True(CommandProcessor.ParseCommands(input).Count == 0);

      input = "forward 20\nwait 10\n\n\n";
      Assert.True(CommandProcessor.ParseCommands(input).Count == 2);

      input = "invalid";
      Assert.True(CommandProcessor.ParseCommands(input).Count == 0);
    }
  }
}