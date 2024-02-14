using System.Collections;
using UnityEngine;

public class RayAttachments : MonoBehaviour
{
    [SerializeField] private float addSomeMoreRotation;

    private Transform target;
    private Camera camera;

    private void Update()
    {
        camera ??= Camera.main;
        target ??= GetComponentInParent<AttachmentList>().Center;
        transform.position = camera.WorldToScreenPoint(target.position);
    }
}