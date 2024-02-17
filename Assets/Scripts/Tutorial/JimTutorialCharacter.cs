using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using EventBusSystem;
using UnityEngine;

public class JimTutorialCharacter : MonoBehaviour
{
    [SerializeField] private Animator character;
    [SerializeField] private GameObject cameraStart;
    [SerializeField] private GameObject joystick;

    [SerializeField] private Animator jimAnimator;
    [SerializeField] private Transform jimStartPosition;
    [SerializeField] private Transform jimWhehiclePosition;
    [SerializeField] private Transform jimEndPosition;
    [SerializeField] private Animator wehicleAnimator;
    [SerializeField] private Animator zombieAnimator;
    [SerializeField] private Transform jimDoorAnimationPosition;

    private Dictionary<JimAnimationType, int> animationHashes = new Dictionary<JimAnimationType, int>();

    private void Start()
    {
        InitializeAnimation();
        StartCoroutine(JimTutorial());
    }

    private void InitializeAnimation()
    {
        foreach (JimAnimationType jimAnimation in Enum.GetValues(typeof(JimAnimationType)))
        {
            animationHashes.Add(jimAnimation, Animator.StringToHash(jimAnimation.ToString()));
        }
    }

    private IEnumerator JimTutorial()
    {
        transform.position = jimStartPosition.position;
        var lookAt = jimWhehiclePosition.transform.position;
        lookAt.y = transform.position.y;
        transform.DOLookAt(lookAt, 0.5f);
        var isOnPosition = false;
        transform.DOMove(jimWhehiclePosition.position, 6f).SetEase(Ease.Linear).OnComplete(() => isOnPosition = true);
        jimAnimator.SetBool(animationHashes[JimAnimationType.Walk], true);
        while (isOnPosition == false)
        {
            yield return null;
        }

        jimAnimator.SetBool(animationHashes[JimAnimationType.Walk], false);
        jimAnimator.SetBool(animationHashes[JimAnimationType.Idle], true);
        lookAt = wehicleAnimator.transform.position;
        lookAt.y = transform.position.y;
        transform.DOLookAt(lookAt, 0.5f);
        yield return new WaitForSeconds(0.7f);
        jimAnimator.SetBool(animationHashes[JimAnimationType.Idle], false);
        jimAnimator.SetBool(animationHashes[JimAnimationType.Check], true);
        yield return new WaitForSeconds(7f);
        jimAnimator.SetBool(animationHashes[JimAnimationType.Check], false);
        wehicleAnimator.SetTrigger(animationHashes[JimAnimationType.Whehicle]);
        jimAnimator.SetBool(animationHashes[JimAnimationType.JimFail], true);
        yield return new WaitForSeconds(2f);
        jimAnimator.SetBool(animationHashes[JimAnimationType.JimFail], false);
        zombieAnimator.SetTrigger(animationHashes[JimAnimationType.Zombie]);
        yield return new WaitForSeconds(1f);
        transform.DORotateQuaternion(Quaternion.identity, 1.2f);
        jimAnimator.SetBool(animationHashes[JimAnimationType.Shot], true);
        yield return new WaitForSeconds(1.7f);
        ServiceLocator.GetService<ISoundSystem>().PlaySound(SoundType.Ar);
        zombieAnimator.SetTrigger(animationHashes[JimAnimationType.ZombieHit]);
        jimAnimator.SetBool(animationHashes[JimAnimationType.Shot], false);
        yield return new WaitForSeconds(3.7f);

        lookAt = jimDoorAnimationPosition.transform.position;
        lookAt.y = transform.position.y;
        transform.DOLookAt(lookAt, 0.5f);
        isOnPosition = false;
        transform.DOMove(jimDoorAnimationPosition.position, 3f).SetEase(Ease.Linear)
            .OnComplete(() => isOnPosition = true);
        jimAnimator.SetBool(animationHashes[JimAnimationType.Run], true);
        while (isOnPosition == false)
        {
            yield return null;
        }

        jimAnimator.SetBool(animationHashes[JimAnimationType.Run], false);
        transform.DORotateQuaternion(Quaternion.identity, 0.3f);
        yield return new WaitForSeconds(0.3f);
        jimAnimator.SetTrigger(animationHashes[JimAnimationType.Open]);
        yield return new WaitForSeconds(7f);
        lookAt = jimEndPosition.transform.position;
        lookAt.y = transform.position.y;
        transform.DOLookAt(lookAt, 0.5f);
        transform.DOMove(jimEndPosition.position, 3f).SetEase(Ease.Linear)
            .OnComplete(() => isOnPosition = true);
        jimAnimator.SetBool(animationHashes[JimAnimationType.Run], true);
        yield return new WaitForSeconds(2f);

        EventBus.RaiseEvent<IJimSignal>(t => t.Finish());
        Destroy(gameObject);
    }
}