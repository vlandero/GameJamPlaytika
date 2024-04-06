using UnityEngine;

public class VoidBrick : MonoBehaviour
{
    public Transform _bricksHolder;

    // Start is called before the first frame update
    void Start()
    {
        transform.eulerAngles = new Vector3(0, 0, 90);
        _bricksHolder = transform.parent;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("ball"))
        {
            foreach (Transform trans in _bricksHolder)
            {
                if (!trans.Equals(transform))
                {
                    var brick = trans.GetComponent<Brick2D>();
                    brick.MoveTo(transform, Vector2.Distance(transform.position, other.transform.position) * 5f);
                }
            }
            other.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            other.transform.localPosition = new Vector3(0f, 0.1f, -3.6f);
        }
    }
}
