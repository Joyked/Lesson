using UnityEngine;

[RequireComponent(typeof(RayCaster))]
public class FlagTransporter : MonoBehaviour
{
    private AntBase _antBase;
    private RayCaster _rayCaster;

    private void Awake() =>
        _rayCaster = GetComponent<RayCaster>();

    private void OnEnable() =>
        _rayCaster.ObjectFound += SelectPosition;

    private void OnDisable() =>
        _rayCaster.ObjectFound -= SelectPosition;

    private void SelectPosition(RaycastHit hit)
    {
        if (hit.collider.gameObject.TryGetComponent(out AntBase antBase))
        {
            _antBase = antBase;
            ReturnOnBase(antBase);
        }
        else if (_antBase != null)
        {
            NewPosition(hit.point);
        }
    }

    private void NewPosition(Vector3 position) =>
        _antBase.FlagThisBase.transform.position = position;
    
    public void ReturnOnBase(AntBase antBase) =>
        antBase.FlagThisBase.gameObject.transform.position = antBase.transform.position;
        
}
