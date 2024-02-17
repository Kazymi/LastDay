using UnityEngine;



    [CreateAssetMenu(menuName = "Config/Tutorial/Node")]
    public class TutorialNode : ScriptableObject
    {
        [SerializeReference, SubclassSelector] TutorialStep[] steps;

        public string Key => name;
        public TutorialStep[] Steps => steps;
    }

