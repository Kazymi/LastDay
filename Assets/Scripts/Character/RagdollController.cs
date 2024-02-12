using System.Collections;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class RagdollController : MonoBehaviour
{
    [SerializeField] private Collider parentCollider;
    [SerializeField] private HealthController healthController;

    private Rigidbody[] ragDollObjects;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        InitializeRagDoll();
    }

    private void OnEnable()
    {
        healthController.HealthEmpty += CharacterDead;
    }

    private void OnDisable()
    {
        healthController.HealthEmpty -= CharacterDead;
    }

    private void InitializeRagDoll()
    {
        ragDollObjects = GetComponentsInChildren<Rigidbody>();
        SetRigidBody(true);
    }

    private void CharacterDead()
    {
        parentCollider.enabled = false;
        animator.enabled = false;
        SetRigidBody(false);
        AddForce();
        StartCoroutine(Dead());
    }

    private IEnumerator Dead()
    {
        transform.parent = null;
        yield return new WaitForSeconds(25);
        SetRigidBody(true);
        transform.DOMove(parentCollider.transform.position + Vector3.down * 3, 6f);
        Destroy(parentCollider.gameObject, 6f);
        Destroy(gameObject, 6f);
    }

    private void AddForce()
    {
        var force = 7;
        var axys = ServiceLocator.GetService<IPlayerController>().ForwardPosition;
        foreach (var ragDollObject in ragDollObjects)
        {
            ragDollObject.AddForce(axys * force, ForceMode.Impulse);
        }
    }

    private void SetRigidBody(bool value)
    {
        foreach (var ragDollObject in ragDollObjects)
        {
            if (ragDollObject != null)
            {
                ragDollObject.isKinematic = value;
            }
        }
    }
}