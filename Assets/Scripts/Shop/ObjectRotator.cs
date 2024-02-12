using UnityEngine;

public class ObjectRotator : MonoBehaviour
{
    private float _sensitivity;
    private Vector3 _mouseReference;
    private Vector3 _mouseOffset;
    private Vector3 _rotation;
    private bool _isRotating;

    private void Start()
    {
        _sensitivity = 0.4f;
        _rotation = Vector3.zero;
    }

    private void OnEnable()
    {
        transform.localRotation = Quaternion.identity;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (_isRotating == false)
            {
                IdStartRotate();
                _isRotating = true;
            }

            _mouseOffset = (Input.mousePosition - _mouseReference);
            _rotation.y = -(_mouseOffset.x + _mouseOffset.y) * _sensitivity;
            transform.Rotate(_rotation);
            _mouseReference = Input.mousePosition;
        }
        else
        {
            _isRotating = false;
        }
    }

    private void IdStartRotate()
    {
        _mouseReference = Input.mousePosition;
    }
}