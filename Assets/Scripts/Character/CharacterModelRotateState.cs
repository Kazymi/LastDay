using DG.Tweening;
using StateMachine;
using UnityEngine;

public class CharacterModelRotateState : State
{
    private readonly Transform _rotateState;
    private IPlayerTargetSearcher _playerTargetSearcher;

    public CharacterModelRotateState(Transform rotateState)
    {
        _rotateState = rotateState;
    }

    public override void OnStateEnter()
    {
        _rotateState.DOKill();
        _playerTargetSearcher = ServiceLocator.GetService<IPlayerTargetSearcher>();
    }

    public override void OnStateExit()
    {
        _rotateState.DOLocalRotateQuaternion(Quaternion.identity, 0.6f);
    }

    public override void Tick()
    {
        LookAtTarget();
    }

    private void LookAtTarget()
    {
        var targetRotateAxys = _playerTargetSearcher.FoundedTarget.TargetPosition;
        targetRotateAxys.y = _rotateState.position.y;
        Vector3 targetDir = targetRotateAxys - _rotateState.position;
        var rotateSpeed = 12;
        float step = rotateSpeed * Time.deltaTime;

        Vector3 rotateDir = Vector3.RotateTowards(_rotateState.forward, targetDir, step, 0.0F);
        _rotateState.rotation = Quaternion.LookRotation(rotateDir);
    }
}