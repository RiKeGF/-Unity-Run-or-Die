using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonSpawn : MonoBehaviour
{
   private float timerSpawn = 5f;
   public bool startTimer;
   public bool start;

   // Start is called before the first frame update
   void Start()
   {

   }

   // Update is called once per frame
   void Update()
   {
      if (startTimer)
      {
         timerSpawn -= Time.deltaTime;
         if (timerSpawn < 0)
         {
            start = true;
         }
      }
   }
}
