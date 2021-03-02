using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ouija : MonoBehaviour
{
   public bool unLockeed;
   private bool startDelay;
   private float delay = 1f;
   public Animator animGaveta;

   private void Update()
   {
      if (animGaveta.GetBool("Open"))
      {
         startDelay = true;
      }
      else
      {
         startDelay = false;
         delay = 1f;
      }

      if (startDelay)
      {
         delay -= Time.deltaTime;

         if (delay < 0)
         {
            unLockeed = true;
         }
         else
         {
            unLockeed = false;
         }
      }
   }

}
