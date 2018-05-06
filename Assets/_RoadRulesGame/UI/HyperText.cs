using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class HyperText : MonoBehaviour {

  [Range(0.0f, 1f)]
  public float tickRate = 0.33f;
  public Color[] pallete;

  private Text textField;
  private float lastShiftTime = 0f;
  private bool initialized = false;

  void Awake() {    
    this.textField = GetComponent<Text>();
    if(pallete.Length == 0) {
      Debug.LogWarning("HyperText missing pallete",this);
    } else {
      initialized = true;
    }
  }
	
	// Update is called once per frame
	void Update () {
    if(initialized) {
      if (Time.time > lastShiftTime + tickRate) {
        Shift();
        lastShiftTime = Time.time;
      }  
    }
	}

  int lastIndex = -1;
  Color NextColor() {
    int index = Random.Range(0, pallete.Length);
    if(index != lastIndex) {
      lastIndex = index;
      return pallete[index];  
    }
    //recursion for lazy rerolling.  Statistically this shouldn't be too many iterations before a valid new index is found
    return NextColor();
  }

  void Shift() {
    textField.color = NextColor();
  }
}
