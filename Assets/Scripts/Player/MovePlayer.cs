using UnityEngine;

public class MovePlayer : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] private int speed = 3;
#pragma warning restore 0649
    private const float _gravity = 9.8f;
    private Vector3 _moveVector;
    private CharacterController _characterController;

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        MoveCharacter();
    }
    private void MoveCharacter()
    {
        _moveVector = new Vector3(Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0, Input.GetAxis("Vertical") * speed * Time.deltaTime);
        _moveVector = transform.TransformDirection(_moveVector);
        if (!_characterController.isGrounded)
        {
            _moveVector.y -= Time.deltaTime * _gravity;
        }
        _characterController.Move(_moveVector);

        //zıplama mekaniklerini ekliyecegim.
    }
}
