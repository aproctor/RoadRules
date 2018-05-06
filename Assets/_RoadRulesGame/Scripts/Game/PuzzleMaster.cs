using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RoadRules {

  public class PuzzleMaster : MonoBehaviour {

    public LivingThing[] objectives;


    public enum PlayState {
      Waiting,
      Playing,
      Complete
    }
    public PlayState state = PlayState.Waiting;

    public float tickRate = 1f;
    public CommandableAgent[] agents;
    private int totalTicks = 0;

    public UnityEvent OnPuzzleComplete;

    private float lastTickTime = 0f;

    void Update() {
      if (state == PlayState.Playing) {
        if (Time.time > lastTickTime + tickRate) {
          foreach (CommandableAgent agent in agents) {
            agent.Tick();
          }
          lastTickTime = Time.time;
          totalTicks += 1;
        }

        if (IsPuzzleComplete()) {
          Debug.Log("YOU WIN");
          int totalDead = 0;
          foreach (CommandableAgent agent in agents) {
            agent.Celebrate();
            if(!agent.Alive) {
              totalDead += 1;
            }
          }
          OnPuzzleComplete.Invoke();

          //TODO clean up score reporting
          SceneLoader.LogScore(SceneLoader.CurrentSceneName(), totalTicks, agents[0].LinesOfCode, totalDead);

          state = PlayState.Complete;
        }
      }
    }


    public void Play() {      
      foreach(CommandableAgent agent in agents) {
        agent.Begin();
      }
      state = PlayState.Playing;
    }

    public void Reset() {
      totalTicks = 0;

      foreach (CommandableAgent agent in agents) {
        agent.Reset();
      }
      foreach (LivingThing objective in objectives) {
        objective.Respawn();
      }
      state = PlayState.Waiting;
    }


    private bool IsPuzzleComplete() {
      foreach (LivingThing objective in objectives) {
        if(objective.alive) {
          return false;
        }
      }
      return true;
    }




  }

}