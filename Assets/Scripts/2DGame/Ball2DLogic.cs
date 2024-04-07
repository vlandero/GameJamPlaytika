using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball2DLogic : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    private Rigidbody2D _rb;
    private Vector2 _direction;
    // Start is called before the first frame update
    void Start()
    {
        var parent = transform.parent.GetComponent<Renderer>();
        _speed = parent.bounds.max.x - parent.bounds.max.y * 2f;
        _rb = GetComponent<Rigidbody2D>();
        StartCoroutine(AddForce());
    }

    private IEnumerator AddForce()
    {
        yield return new WaitForSeconds(1f);
        _rb.velocity = new Vector2(0, -_speed);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("brick"))
        {
            Destroy(other.gameObject);
        }
        if(other.gameObject.CompareTag("Player"))
        {
            Vector2 heading = other.transform.position - transform.position;
            _direction = heading / heading.magnitude;

            _rb.velocity = _direction * _speed;
        }
        if (other.gameObject.CompareTag("bottomWall"))
        {
            var scene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(scene);
        }

    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _rb.velocity = _direction * _speed;
        }
    }
}
