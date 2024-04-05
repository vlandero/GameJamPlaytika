using UnityEngine;

public class BrickSpawner2D : MonoBehaviour
{
    [SerializeField]
    private GameObject _brickPrefab;
    [SerializeField]
    private GameObject _brickHeartPrefab;
    private int[,] _grid = new int[10, 9] {
        {0, 0, 0, 0, 0, 0, 0, 0, 0 },
        {0, 0, 1, 0, 0, 0, 1, 0, 0 },
        {0, 1, 1, 1, 0, 1, 1, 1, 0 },
        {0, 1, 1, 1, 1, 1, 1, 1, 0 },
        {0, 1, 1, 1, 2, 1, 1, 1, 0 },
        {0, 0, 1, 1, 1, 1, 1, 0, 0 },
        {0, 0, 0, 1, 1, 1, 0, 0, 0 },
        {0, 0, 0, 0, 1, 0, 0, 0, 0 },
        {0, 0, 0, 0, 0, 0, 0, 0, 0 },
        {0, 0, 0, 0, 0, 0, 0, 0, 0 }
    };
    private Vector3 _brickPosition;
    // Start is called before the first frame update
    void Start()
    {
        _brickPosition = new Vector3(-4f, 6f, -0.1f) + transform.parent.position;

        for (int i = 0; i < 10; i++)
        {

            for (int j = 0; j < 9; j++)
            {
                if (_grid[i, j] == 1)
                {
                    var brick = Instantiate(_brickPrefab, _brickPosition, Quaternion.identity, transform);
                }
                if (_grid[i, j] == 2)
                {
                    var brick = Instantiate(_brickHeartPrefab, _brickPosition, Quaternion.identity, transform);
                }
                _brickPosition.x ++;
            }
            _brickPosition.x = -4f + transform.parent.position.x;
            _brickPosition.y --;
        }
    }

}
