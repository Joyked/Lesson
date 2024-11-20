using UnityEngine;

public class Detector : MonoBehaviour
{
    [SerializeField] private Signal _signal;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Rogue rogue))
            _signal.TurnOn();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Rogue rogue))
            _signal.TurnOff();
    }
}
