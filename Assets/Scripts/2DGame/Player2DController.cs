using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    private Rigidbody2D _rb;

    private float _moveDirection;
    private float _minX, _maxX;
    private float _outlineWidth = 0.5f;

    private void Start()
    {
        _speed = 30f;
        _rb = GetComponent<Rigidbody2D>();
        SetMinMax();
    }

    private void Update()
    {
        
        _moveDirection = Input.GetAxis("Horizontal");

    }
    private void FixedUpdate()
    {
        float nextPos = Mathf.Clamp(transform.position.x + _moveDirection * _speed * Time.fixedDeltaTime, _minX, _maxX);

        _rb.MovePosition(new Vector2(nextPos, transform.position.y));
    }
    private void SetMinMax()
    {
        Vector3 planeSize = transform.parent.GetComponent<Renderer>().bounds.size;
        _minX = transform.position.x - planeSize.x / 2 + transform.localScale.x / 2 + _outlineWidth;
        _maxX = transform.position.x + planeSize.x / 2 - transform.localScale.x / 2 - _outlineWidth;
    }
}