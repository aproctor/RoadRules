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
      Assert.True(CommandProcessor.ValidateInstruction("wait"));
    }
  }
}