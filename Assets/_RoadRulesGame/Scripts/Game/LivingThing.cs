using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RoadRules {

  public class LivingThing : MonoBehaviour {

    public bool alive = true;

    public UnityEvent OnDie;
    public UnityEvent OnRespawn;

    private Animator _animator;

    void Awake() {
      _animator = this.GetComponent<Animator>();
    }

    public void Die() {
      if (alive) {
        if(_animator != null) {
          _animator.SetBool("Dead", true);
        }
        OnDie.Invoke();
        alive = false;
      }
    }

    public void Respawn() {
      if (!alive) {
        if (_animator != null) {
          _animator.SetBool("Dead", false);
        }
        OnRespawn.Invoke();
        alive = true;
      }
    }
  }

}