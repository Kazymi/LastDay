using UnityEngine;

public interface IPlayerController
{
    Vector3 PlayerPosition { get; }
    Vector3 ForwardPosition { get; }
    Quaternion BodyRotate { get; }
   
}