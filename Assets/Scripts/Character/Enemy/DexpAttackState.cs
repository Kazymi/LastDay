using System.Linq;
using StateMachine;
using UnityEngine;

public class DexpAttackState : State
{
    private readonly Transform center;
    private readonly float radius;
    private readonly int damage;
    private readonly LayerMask mask;
    private readonly ICharacterAnimationController characterAnimationController;
    private readonly DexpBossEnemy dexpBossEnemy;

    private float currentTime;
    private const float attackTimer = 0.3f;

    public DexpAttackState(Transform center, float radius, int damage, LayerMask mask,
        ICharacterAnimationController characterAnimationController, DexpBossEnemy dexpBossEnemy)
    {
        this.center = center;
        this.radius = radius;
        this.damage = damage;
        this.mask = mask;
        this.characterAnimationController = characterAnimationController;
        this.dexpBossEnemy = dexpBossEnemy;
    }

    public override void Tick()
    {
        if (currentTime <= 0)
        {
            currentTime = attackTimer;
            Attack();
        }
        else
        {
            currentTime -= Time.deltaTime;
        }
    }

    private void Attack()
    {
        var foundPlayer = Physics.OverlapSphere(center.position, radius, mask)
            .Where(t => t.GetComponent<IPlayerDamageTarget>() != null).ToList();
        if (foundPlayer.Count == 0) return;
        foreach (var player in foundPlayer)
        {
            player.GetComponent<IPlayerDamageTarget>().TakeDamage(damage);
        }
    }

    public override void OnStateEnter()
    {
        characterAnimationController.SetBool(CharacterAnimationType.BossAttack, true);
        currentTime = 1;
    }

    public override void OnStateExit()
    {
        dexpBossEnemy.DexpAttackCooldown();
        characterAnimationController.SetBool(CharacterAnimationType.BossAttack, false);
    }
}