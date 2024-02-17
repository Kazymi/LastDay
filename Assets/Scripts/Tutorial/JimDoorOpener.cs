using UnityEngine;

public class JimDoorOpener : MonoBehaviour
{
    [SerializeField] private Transform door_1;
    [SerializeField] private Transform door_2;

    public void DisableDoor()
    {
        door_1.gameObject.SetActive(false);
    }

    public void EnableDoor()
    {
        door_2.gameObject.SetActive(true);
    }
}