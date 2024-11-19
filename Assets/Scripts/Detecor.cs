using System;
using UnityEngine;

public class Detecor : MonoBehaviour
{
    public event Action<bool> RogueInHouse;
    
    private bool _rogueInHouse = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Rogue rogue))
        {
            _rogueInHouse = true;
            RogueInHouse?.Invoke(_rogueInHouse);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Rogue rogue))
        {
            _rogueInHouse = false;
            RogueInHouse?.Invoke(_rogueInHouse);
        }
    }
}
