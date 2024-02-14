using System;
using UnityEngine;

[Serializable]
public class UIAttacher : MonoBehaviour
{
    [SerializeField] private Transform attachPosition;

    private void Update()
    {
        transform.position = attachPosition.position;
    }
}