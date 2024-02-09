using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformDebugger : MonoBehaviour
{
    [SerializeField] private Joystick moveDir;
    [SerializeField] private string Test;
    [SerializeField] private Vector3 forwardOne;
    [SerializeField] private Vector3 forwardTwo;
    [SerializeField] private Vector3 forwardDir;
    [SerializeField] private Vector3 Result;

    [SerializeField] private Transform One;
    [SerializeField] private Transform Two;

    private void Update()
    {
        forwardOne = One.forward;
        forwardTwo = Two.forward;
        forwardDir = moveDir.Direction;
        Result = forwardOne - forwardTwo;
        
        var isRight = moveDir.Direction.x > 0.1;
        var isLeft = moveDir.Direction.x < -0.1;
        var isForward = moveDir.Direction.y > 0.1f;
        var isBack = moveDir.Direction.y < -0.1f;
        if (isForward)
        {
            Test = "For";
        }

        if (isBack)
        {
            Test = "b";
        }

        if (isRight)
        {
            Test = "r";
        }

        if (isLeft)
        {
            Test = "l";
        }
    }
}