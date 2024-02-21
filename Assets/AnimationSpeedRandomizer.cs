using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSpeedRandomizer : MonoBehaviour
{
   private void Awake()
   {
      GetComponent<Animator>().speed = Random.Range(0.6f, 1f);
   }
}
