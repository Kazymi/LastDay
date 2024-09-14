using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallTrigger : MonoBehaviour
{
   private void OnTriggerEnter(Collider other)
   {
      var enemy = other.GetComponent<EnemyStateMachine>();
      if (enemy)
      {
         enemy.ToFall();
      }
   }
}
