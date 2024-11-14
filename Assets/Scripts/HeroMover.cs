using UnityEngine;
using UnityEngine.UIElements;

public class HeroMover : Mover
{
    [Space]
    [SerializeField] private MouseButton _tabButtons;

    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
        Direction = transform.position;
    }

    private void Update()
    {
        if(Input.GetMouseButton(_tabButtons.GetHashCode()))
        {
            RaycastHit hit;

            if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out hit))
                Direction = hit.point;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out EnemyMover enemy))
        {
            transform.LookAt(enemy.transform);
            Animation.SetBool("Attack", true);
        }
    }
}
