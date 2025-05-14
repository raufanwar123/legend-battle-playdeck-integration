using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawnPoint : MonoBehaviour
{
    public bool HasPlayer = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag.Equals("Player") == true)
            HasPlayer = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag.Equals("Player") == true)
            HasPlayer = false;
    }
}
