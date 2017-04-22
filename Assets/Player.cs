using UnityEngine;
using UnityEngine.InputNew;

public class Player : MonoBehaviour
{
    public float Jump = 2f;
    public float Speed = 4f;
    public float Force = 8f;

    private PlayerControlls _playerControlls = null;
    private Rigidbody _rigidbody;
    private Vector3 _pos = new Vector3();

    public void Start()
    {
        var payerInput = GetComponent<PlayerInput>();
        if (payerInput)
        {
            _playerControlls = payerInput.GetActions<PlayerControlls>();
        }
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Update()
    {
        UpdateControlls();
    }

    private void UpdateControlls()
    {
        if (_playerControlls == null) return;

        _pos.x = _playerControlls.moveX.value*Speed*Time.deltaTime;
        _pos.y = 0f;
        _pos.z = _playerControlls.moveY.value*Speed*Time.deltaTime;

        /*
        transform.Translate(_playerControlls.moveX.value*Speed*Time.deltaTime, 0,
            _playerControlls.moveY.value*Speed*Time.deltaTime, Space.Self);
            */

        _rigidbody.AddForce(_pos * Force, ForceMode.Impulse);

        if (_playerControlls.jump.wasJustPressed)
        {
            _rigidbody.AddForce(Vector3.up * Jump, ForceMode.Impulse);
        }
    }
}