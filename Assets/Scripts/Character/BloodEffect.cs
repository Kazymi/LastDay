using UnityEngine;

public class BloodEffect : MonoBehaviour
{
    private void Start()
    {
        transform.rotation = ServiceLocator.GetService<IPlayerController>().BodyRotate;
    }
}