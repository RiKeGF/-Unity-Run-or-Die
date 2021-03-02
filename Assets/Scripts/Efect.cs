using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Efect : MonoBehaviour
{
   public AudioSource sEfect;
   private float timerEfect = 10;

   // Update is called once per frame
   void Update()
   {
      timerEfect -= Time.deltaTime;
      if (timerEfect < 0)
      {
         StartEfect();
         timerEfect = Random.Range(30, 40);
      }
   }

   private void StartEfect()
   {
      if (!sEfect.isPlaying)
      {
         sEfect.Play();
      }
   }

}
