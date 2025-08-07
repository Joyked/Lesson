using UnityEngine;

public class OnClickHandler : MonoBehaviour
{
    private AntBase _antBase;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
        
            if (Physics.Raycast(ray, out hit))
            {
                GameObject clickedObject = hit.collider.gameObject;

                if (clickedObject.TryGetComponent(out AntBase antBase))
                {
                    _antBase = antBase;
                    _antBase.Flag.ReturnPosition();
                }
                else if (_antBase != null)
                {
                    _antBase.Flag.NewPosition(hit.point);
                }
            }
        }
    }
}
