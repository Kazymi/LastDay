using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialParameters : MonoBehaviour
{
    [field: SerializeField] public TutorialExit TutorialExit;
    [field: SerializeField] public Animator CharacterAnimator { get; private set; }
    [field: SerializeField] public Joystick Joystic { get; private set; }
    [field: SerializeField] public GameObject JimCamera { get; private set; }
    [field: SerializeField] public GameObject WeaponMain { get; private set; }
    [field: SerializeField] public GameObject Levelsystem { get; private set; }

    [field: SerializeField] public GameObject WASDController { get; private set; }
    [field: SerializeField] public GameObject FingerController { get; private set; }
    [field: SerializeField] public Quest FindweaponQuest { get; private set; }
    [field: SerializeField] public Quest KillZobies { get; private set; }
    [field: SerializeField] public Quest ExitQuest { get; private set; }
    [field: SerializeField] public QuestStateMachine QuestStateMachine { get; private set; }
    
    
    [field: SerializeField] public GameObject ShopMenuFinger { get; private set; }
    [field: SerializeField] public GameObject ShopCloseFinger { get; private set; }
    [field: SerializeField] public GameObject UpgradeCloseFinger { get; private set; }
    [field: SerializeField] public GameObject UpgradeFinger { get; private set; }
    [field: SerializeField] public GameObject UpgradeSelectFinger { get; private set; }
    [field: SerializeField] public GameObject ShopBuyFinger { get; private set; }
    [field: SerializeField] public GameObject StartGameFinger { get; private set; }
    [field: SerializeField] public GameObject SelectWeaponFinger { get; private set; }
    
    [field: SerializeField] public GameObject StarGameButton { get; private set; }
    [field: SerializeField] public GameObject CloseShop { get; private set; }
    [field: SerializeField] public GameObject CloseUpgrade { get; private set; }
}