﻿using UnityEngine;

public class PlayerAnimatorController : CharacterAnimationController, IPlayerAnimatorController
{
    public PlayerAnimatorController(Animator animator) : base(animator)
    {
        ServiceLocator.Subscribe<IPlayerAnimatorController>(this);
    }
}