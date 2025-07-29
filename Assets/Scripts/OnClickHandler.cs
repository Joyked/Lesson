using UnityEngine;

public class OnClickHandler : MonoBehaviour
{
    private CreateBase _createBase;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
        
            if (Physics.Raycast(ray, out hit))
            {
                GameObject clickedObject = hit.collider.gameObject;
                Debug.Log(clickedObject.name);

                if (clickedObject.TryGetComponent(out CreateBase createBase))
                {
                    _createBase = createBase;
                    _createBase.ReturnPosition();
                }
                else if (_createBase != null)
                {
                    _createBase.NewPosition(hit.point);
                }
            }
        }
    }
}
