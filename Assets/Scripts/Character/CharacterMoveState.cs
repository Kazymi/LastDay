using StateMachine;
using UnityEngine;

public class CharacterMoveState : State
{
    private readonly Rigidbody m_rigidbody;
    private readonly CharacterConfigurations m_characterConfigurations;
    private readonly Joystick m_joystick;
    private readonly IPlayerAnimatorController m_characterAnimationController;
    private readonly Transform characterBody;
    private IPlayerTargetSearcher m_targetSearcher;

    private float needRotate = 0;
    private float currentRotate = 0;

    public CharacterMoveState(Rigidbody rigidbody, CharacterConfigurations characterConfigurations, Joystick joystick,
        IPlayerAnimatorController
            characterAnimationController, Transform characterBody)
    {
        m_targetSearcher = ServiceLocator.GetService<IPlayerTargetSearcher>();
        m_rigidbody = rigidbody;
        m_characterConfigurations = characterConfigurations;
        m_joystick = joystick;
        m_characterAnimationController = characterAnimationController;
        this.characterBody = characterBody;
    }

    public override void Tick()
    {
        if (m_joystick.Direction != Vector2.zero)
        {
            var delta = Time.fixedDeltaTime;
            Rotate(m_joystick.Direction, delta);
            Move(m_joystick.Direction, delta);
            if (m_targetSearcher.IsTargetFounded)
            {
                LegRotate();
            }
            else
            {
                needRotate = 0;
            }
        }

        if (currentRotate > needRotate)
        {
            currentRotate -= Time.deltaTime;
            if (currentRotate < needRotate) currentRotate = needRotate;
        }

        if (currentRotate < needRotate)
        {
            currentRotate += Time.deltaTime;
            if (currentRotate > needRotate) currentRotate = needRotate;
        }

        m_characterAnimationController.SetFloat(CharacterAnimationType.LegsMove, currentRotate);
    }

    public override void OnStateEnter()
    {
        currentRotate = 0;
        m_characterAnimationController.SetBool(CharacterAnimationType.Walk, true);
    }

    public override void OnStateExit()
    {
        m_characterAnimationController.SetBool(CharacterAnimationType.Walk, false);
    }

    private void LegRotate()
    {
        var rotate = 0f;
        var isRight = m_joystick.Direction.x > 0.2;
        var isLeft = m_joystick.Direction.x < -0.2;
        var isForward = m_joystick.Direction.y > 0.2f;
        var isBack = m_joystick.Direction.y < -0.2f;

        if (isForward)
        {
            rotate = characterBody.transform.forward.z > 0 ? 0 : 0.4f;
        }

        if (isBack)
        {
            rotate =  characterBody.transform.forward.z > 0 ? 0.4f : 0; 
        }

        if (isRight)
        {
            rotate = characterBody.transform.forward.z > 0 ? 1 : 0.7f; 
        }

        if (isLeft)
        {
            rotate =  characterBody.transform.forward.z > 0 ?  0.7f : 1f; 
        }
        

        needRotate = rotate;
    }

    private void Rotate(Vector2 joystickDirection, float deltaTime)
    {
        var rb = m_rigidbody;
        var charForward = rb.transform.forward;
        var direction = new Vector3(joystickDirection.x, 0, joystickDirection.y).normalized;
        var speed = m_characterConfigurations.SpeedRotate * deltaTime;

        var newDirection = Vector3.RotateTowards(charForward, direction, speed, 0.0f);
        rb.transform.rotation =
            Quaternion.Lerp(m_rigidbody.transform.rotation, Quaternion.LookRotation(newDirection), 1);
    }

    private void Move(Vector2 joystickDirection, float deltaTime)
    {
        var rb = m_rigidbody;
        rb.velocity = Vector3.zero;
        var charForward = rb.transform.forward;
        var speed = m_characterConfigurations.Speed;
        speed *= joystickDirection.magnitude;
        var velocity = charForward * (speed * deltaTime);
        rb.velocity = velocity;
    }
}