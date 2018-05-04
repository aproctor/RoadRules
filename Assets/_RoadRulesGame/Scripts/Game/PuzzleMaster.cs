using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoadRules {

  public class PuzzleMaster : MonoBehaviour {

    public enum PlayState {
      Waiting,
      Playing,
      Complete
    }
    public PlayState state = PlayState.Waiting;

    public float tickRate = 1f;
    public CommandableAgent[] agents;


    public void Play() {      
      foreach(CommandableAgent agent in agents) {
        agent.Begin();
      }
      state = PlayState.Playing;
    }


    public void Reset() {
      foreach (CommandableAgent agent in agents) {
        agent.Reset();
      }
      state = PlayState.Waiting;
    }

    private float lastTickTime = 0f;

    void Update() {
      if(state == PlayState.Playing) {
        if(Time.time > lastTickTime + tickRate) {
          foreach (CommandableAgent agent in agents) {
            agent.Tick();
          }
          lastTickTime = Time.time;
        }
      }
    }


  }

}