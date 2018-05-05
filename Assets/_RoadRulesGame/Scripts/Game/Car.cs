using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoadRules {
  public class Car : MonoBehaviour {
    public void OnTriggerEnter(Collider other) {
      LivingThing thing = other.GetComponent<LivingThing>();
      if(thing != null) {
        thing.transform.LookAt(this.transform);
        thing.Die();
      }
    }

  }
}
