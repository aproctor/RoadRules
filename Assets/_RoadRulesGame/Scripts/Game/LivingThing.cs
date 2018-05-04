using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RoadRules {

  public class LivingThing : MonoBehaviour {

    public bool alive = true;

    public UnityEvent OnDie;
    public UnityEvent OnRespawn;

    public void Die() {
      if (alive) {
        OnDie.Invoke();
        alive = false;
      }
    }

    public void Respawn() {
      if (!alive) {
        OnRespawn.Invoke();
        alive = true;
      }
    }
  }

}