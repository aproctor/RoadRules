using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

namespace RoadRules {
  [TestFixture]
  public class CommandProcessorTests {

    [Test]
    public void TestCommandsList() {
      Assert.True(CommandProcessor.ValidateInstruction("forward"));
      Assert.True(CommandProcessor.ValidateInstruction("forward 1"));
      Assert.True(CommandProcessor.ValidateInstruction("forward -1"));
      Assert.True(CommandProcessor.ValidateInstruction("forward 100"));
      Assert.False(CommandProcessor.ValidateInstruction("forward 100 40"));
      Assert.False(CommandProcessor.ValidateInstruction("forward qq"));

      Assert.True(CommandProcessor.ValidateInstruction("wait"));
      Assert.True(CommandProcessor.ValidateInstruction("wait 1"));
      Assert.True(CommandProcessor.ValidateInstruction("wait -1"));
      Assert.True(CommandProcessor.ValidateInstruction("wait 100"));
      Assert.False(CommandProcessor.ValidateInstruction("wait 100 40"));
      Assert.False(CommandProcessor.ValidateInstruction("wait qq"));

      Assert.True(CommandProcessor.ValidateInstruction("left"));
      Assert.True(CommandProcessor.ValidateInstruction("right"));

      Assert.False(CommandProcessor.ValidateInstruction("fubar"));
    }
  }
}