using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class listTest : MonoBehaviour
{
	 public List<int> testList = new List<int>();
	 int counter = 0;
	 public List<TestingList> testingList = new List<TestingList>();
	 private void Start()
	 {
		  DontDestroyOnLoad(gameObject);
	 }
	 public void AddList()
	 {
		  testList.Add(counter++);
	 }
	 public void RemoveList()
	 {
		  testList.RemoveAt(testList.Count - 1);
	 }
	 public void RemoveSpecificIndex(int testIndex)
	 {
		  testList.RemoveAt(testIndex);
	 }
	 [System.Serializable]
	 public class TestingList
	 {
		  public int value;
		  public string name;
	 }
}
