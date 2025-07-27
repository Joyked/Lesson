using System;
using UnityEngine;

public class Collector : MonoBehaviour
{
    [SerializeField] private Transform _mandible;
    
    private Cookie _cookieTarget;
    
    public bool IsGetCookie { get; private set; }
    public AntMover Ant { get; private set; }
    
    public event Action<AntMover> GotCookie;

    private void Awake() =>
        Ant = GetComponent<AntMover>();

    private void Update()
    {
        if (IsGetCookie)
            _cookieTarget.transform.position = _mandible.position;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out Cookie cookie) && IsGetCookie == false)
        {
            if (cookie == _cookieTarget)
            {
                IsGetCookie = true;
                Collider collider = cookie.GetComponent<Collider>();
                collider.isTrigger = true;
                GotCookie?.Invoke(Ant);
            }
        }
    }

    public void SetTargetCookie(Cookie cookie) =>
        _cookieTarget = cookie;

    public void PutCookie() =>
        IsGetCookie = false;
}
