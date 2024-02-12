using UnityEngine;

public class MiniGun : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private WeaponMain weaponMain;

    private float currentSpeed;

    private void Update()
    {
        if (weaponMain.CanBeShot)
        {
            currentSpeed += Time.deltaTime;
            if (currentSpeed > 1) currentSpeed = 1;
        }
        else
        {
            currentSpeed -= Time.deltaTime;
            if (currentSpeed < 0) currentSpeed = 0;
        }

        animator.speed = currentSpeed;
    }
}