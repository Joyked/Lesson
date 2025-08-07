using System;
using System.Collections;
using UnityEngine;

public class Collector : MonoBehaviour
{
    [SerializeField] private Transform _mandible;
    
    private Cookie _cookieTarget;
    private Coroutine _idleCoroutine;
    private AntBase _antBase;
    
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
                if (_idleCoroutine != null)
                {
                    StopCoroutine(_idleCoroutine);
                    _idleCoroutine = null;
                }
                
                IsGetCookie = true;
                Collider collider = cookie.GetComponent<Collider>();
                collider.isTrigger = true;
                GotCookie?.Invoke(Ant);
            }
        }
    }

    public void SetTargetCookie(Cookie cookie, AntBase antBase)
    {
        _antBase = antBase;
        _cookieTarget = cookie;
        _idleCoroutine = StartCoroutine(Idle());
    }

    public void PutCookie()
    {
        IsGetCookie = false;
        
        if (_idleCoroutine != null)
        {
            StopCoroutine(_idleCoroutine);
            _idleCoroutine = null;
        }
    }

    private IEnumerator Idle()
    {
        float idleSecond = 8;
        yield return new WaitForSeconds(idleSecond);

        if (IsGetCookie == false)
            Ant.Strolle();
    }
}
