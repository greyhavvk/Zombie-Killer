using UnityEngine;

public class RotatePlayer : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] float sensivity = 1000f;
#pragma warning restore 0649
    private float _rotateY = 0;
    private float _rotateX = 0;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        RotatePlayerBody();
    }

    private void RotatePlayerBody()
    {
        _rotateY += sensivity * Input.GetAxis("Mouse Y") * -1 * Time.deltaTime;
        _rotateX += sensivity * Input.GetAxis("Mouse X") * Time.deltaTime;
        _rotateY = Mathf.Clamp(_rotateY, -80, 80);
        transform.eulerAngles = new Vector3(_rotateY, _rotateX, 0);

        //yeniden yapılacak. şu anda bir karakter bulunmadıgı için bütün objeyi döndürmekte. bu da hareket mekaniklerini etkilemekte.
    }
}
