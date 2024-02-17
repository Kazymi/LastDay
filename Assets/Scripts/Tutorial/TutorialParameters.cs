using UnityEngine;

public class TutorialParameters : MonoBehaviour
{
    [field: SerializeField] public Animator CharacterAnimator { get; private set; }
    [field: SerializeField] public Joystick Joystic { get; private set; }
    [field: SerializeField] public GameObject JimCamera { get; private set; }
    [field: SerializeField] public GameObject WeaponMain { get; private set; }
    [field: SerializeField] public GameObject Levelsystem { get; private set; }

    [field: SerializeField] public GameObject WASDController { get; private set; }
    [field: SerializeField] public GameObject FingerController { get; private set; }
    [field: SerializeField] public Quest FindweaponQuest { get; private set; }
    [field: SerializeField] public Quest KillZobies { get; private set; }
    [field: SerializeField] public QuestStateMachine QuestStateMachine { get; private set; }
}