using UnityEngine;

public class RotateDisabler : MonoBehaviour
{
    [SerializeField] private Vector3 lockRotate;

    private void Update()
    {
        transform.rotation = Quaternion.Euler(lockRotate);
    }
}