using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _bottomWall;
    [SerializeField]
    private GameObject _topWall;

    [SerializeField]
    private GameObject _toSpawnPrefab;
    [SerializeField]
    private float _spawnTime;

    private Vector2 _boundsX, _boundsY, _boundsZ;
    private Bounds _bottomBounds, _topBounds;

    void Start()
    {
        _bottomBounds = _bottomWall.GetComponent<Renderer>().bounds;
        _topBounds = _topWall.GetComponent<Renderer>().bounds;


        _boundsX = new Vector2(_bottomBounds.min.x + _toSpawnPrefab.transform.localScale.x / 2 + 0.25f, _bottomBounds.max.x - _toSpawnPrefab.transform.localScale.x / 2 - 0.25f);
        _boundsZ = new Vector2(_bottomBounds.min.z + _toSpawnPrefab.transform.localScale.z / 2 + 0.25f, _bottomBounds.max.z - _toSpawnPrefab.transform.localScale.z / 2 - 0.25f);
        _boundsY = new Vector2(_bottomBounds.max.y + _toSpawnPrefab.transform.localScale.y / 2 + 5f, _topBounds.min.y - _toSpawnPrefab.transform.localScale.y / 2);
        StartCoroutine(SpawnPrefab());
    }

    private IEnumerator SpawnPrefab()
    {
        while (true)
        {
            yield return new WaitForSeconds(_spawnTime);

            Vector3 randomPos = GetRandomPosition();

            while(Physics.CheckBox(randomPos, _toSpawnPrefab.transform.localScale / 2, _toSpawnPrefab.transform.localRotation))
                randomPos = GetRandomPosition();

            Instantiate(_toSpawnPrefab, randomPos, Quaternion.identity, transform);

        }
    }

    private Vector3 GetRandomPosition()
    {
        return new Vector3(
            Random.Range(_boundsX.x, _boundsX.y),
            Random.Range(_boundsY.x, _boundsY.y),
            Random.Range(_boundsZ.x, _boundsZ.y)
        );
    }
}
