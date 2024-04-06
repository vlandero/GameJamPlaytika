using UnityEngine;

public class SecretRoomSwap : MonoBehaviour
{
    public GameObject _secretRoom;
    public Transform _glassCubeNormal;
    public Transform _glassCubeSecret;
    public CamerMovement _camera;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ball"))
        {
            if (_secretRoom.activeSelf)
            {
                _secretRoom.SetActive(false);
                _camera.target = _glassCubeNormal;
            }
            else
            {
                _secretRoom.SetActive(true);
                _camera.target = _glassCubeSecret;
            }
            _camera.ClickThing();
        }
    }
}
