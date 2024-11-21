using System;
using UnityEngine;

public class Detector : MonoBehaviour
{ 
    public event Action RogueIsHere;
    public event Action RogueIsntHere;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Rogue rogue))
            RogueIsHere?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Rogue rogue))
            RogueIsntHere?.Invoke();
    }
}
