using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class HyperText : MonoBehaviour {

  [Range(0.0f, 1f)]
  public float tickRate = 0.1f;
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
    if(Time.time > lastShiftTime + tickRate) {
      Shift();
      lastShiftTime = Time.time;
    }
	}

  void Shift() {
    textField.color = pallete[Random.Range(0,pallete.Length)];
  }
}
