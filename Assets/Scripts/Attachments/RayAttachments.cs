using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class RayAttachments : MonoBehaviour
{
    [SerializeField] private float addSomeMoreRotation;
    [SerializeField] private Transform lookTarget;
    [SerializeField] private Camera camera;
    [SerializeField] private MeshRenderer target;

    private void Update()
    {
        Quaternion rotation = Quaternion.LookRotation(
            lookTarget.transform.position - transform.position,
            transform.TransformDirection(Vector3.up)
        );
        transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
        var fixRotate = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(new Vector3(fixRotate.x,fixRotate.y,fixRotate.z + addSomeMoreRotation));
        transform.position = camera.WorldToScreenPoint(target.bounds.center);
    }
}