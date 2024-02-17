using UnityEngine;

[RequireComponent(typeof(Outline))]
public class TutorialWeapon : MonoBehaviour
{
    public Outline Outline => outline;
    public Vector3 WeaponPosition => transform.position;

    private Outline outline;

    private void Awake()
    {
        outline = GetComponent<Outline>();
    }
}