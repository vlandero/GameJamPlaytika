using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VoidBrick : MonoBehaviour
{
    public Transform _bricksHolder;
    private Transform _player;
    public Camera _toCamera;
    public Intro _intro;
    // Start is called before the first frame update
    private void Awake()
    {
        _toCamera = GameObject.FindGameObjectWithTag("platform").GetComponent<Camera>();
        _intro = FindAnyObjectByType<Intro>();
    }
    void Start()
    {
        transform.eulerAngles = new Vector3(0, 0, 90);
        _bricksHolder = transform.parent;
        _player = FindObjectOfType<PlayerController>().transform;
        _toCamera.gameObject.SetActive(false);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("ball"))
        {
            foreach (Transform trans in _bricksHolder)
            {
                if (!trans.GetComponent<VoidBrick>())
                {
                    var brick = trans.GetComponent<Brick2D>();
                    brick.MoveTo(transform, Vector2.Distance(transform.position, other.transform.position) * 5f, true);
                }
            }
            _toCamera.transform.position = new Vector3(transform.position.x, transform.position.y, _toCamera.transform.position.z);
            other.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            _player.localPosition = new Vector3(0f, 0.1f, -4.25f);
            StartCoroutine(Exclaim(other));

            other.transform.localEulerAngles = new Vector3(90, 0 , 0);
            other.transform.localPosition = new Vector3(0f, 0.1f, -3.6f);
            
        }
    }
    private IEnumerator Exclaim(Collider2D other)
    {
        yield return new WaitForSeconds(0.5f);
        other.transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        other.transform.GetChild(0).gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        _toCamera.transform.LookAt(new Vector3(0, 3, 0));
        StartCoroutine(CameraSwap.SwitchCameras(Camera.main, _toCamera, 1f));

        yield return new WaitForSeconds(1f);
        _intro.StartChatBox(4);

        yield return new WaitUntil(() => _intro.n == 0);

        yield return new WaitForSeconds(1f);


        SceneManager.LoadScene(1);

        // o sa revin si o sa pun chestia asta cand se porneste camera
        /* foreach (Transform brick in _bricks3D)
             brick.transform.localPosition = new Vector3(transform.position.x, transform.position.y, _toCamera.transform.position.z - 100);

         yield return new WaitForSeconds(0.5f);
         GameObject altObiect = new GameObject("AltObiect");

         foreach (Transform trans in _bricks3D)
         {
             trans.transform.localPosition = new Vector3(_toCamera.transform.position.x, _toCamera.transform.position.y, _toCamera.transform.position.z - 1);
             var brick = trans.GetComponent<Brick2D>();
             altObiect.transform.position = brick._initialPos;
             Debug.Log("ceva");
             brick.MoveTo(altObiect.transform, Vector3.Distance(brick.transform.position, brick._initialPos) * 50f, false);
             yield return new WaitUntil(() => brick.transform.position == altObiect.transform.position);
         }*/
    }
}
