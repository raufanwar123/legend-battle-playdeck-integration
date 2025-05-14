using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
[RequireComponent(typeof(Button))]

public class ScrollerButtonController : MonoBehaviour
{
	 public ScrollRect Target;
	 public Button TheOtherButton;
	 public float Step = 0.1f;

	 public void Increment()
	 {
		  if (Target == null || TheOtherButton == null) throw new Exception("Setup ScrollbarIncrementer first!");
		  Target.verticalNormalizedPosition = Mathf.Clamp(Target.verticalNormalizedPosition + Step, 0, 1);
		  GetComponent<Button>().interactable = Target.verticalNormalizedPosition != 1;
		  TheOtherButton.interactable = true;
	 }

	 public void Decrement()
	 {
		  if (Target == null || TheOtherButton == null) throw new Exception("Setup ScrollbarIncrementer first!");
		  Target.verticalNormalizedPosition = Mathf.Clamp(Target.verticalNormalizedPosition - Step, 0, 1);
		  GetComponent<Button>().interactable = Target.verticalNormalizedPosition != 0; 
		  TheOtherButton.interactable = true;
	 }
}
