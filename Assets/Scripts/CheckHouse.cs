using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckHouse : MonoBehaviour
{
   public bool inHouse;
   [SerializeField] private bool soundInHouse = true;
   [SerializeField] private bool soundInForest = false;

   public AudioSource sRainHouse;
   public AudioSource sRainForest;

   public ParticleSystem psRain;

   private void Update()
   {
      if (inHouse)
      {
         if (!sRainHouse.isPlaying)
         {
            sRainHouse.Play();
         }     
         if (psRain.isPlaying)
         {
            psRain.Stop();
         }
         if (!soundInHouse && soundInForest)
         {
            StartCoroutine(ChangeSound());
         }
      }
      else
      {
         if (!sRainForest.isPlaying)
         {
            sRainForest.Play();
         }
         if (!psRain.isPlaying)
         {
            psRain.Play();
         }
         if (soundInHouse && !soundInForest)
         {
            StartCoroutine(ChangeSound());
         }
      }
   }

   IEnumerator ChangeSound()
   {
      while (true)
      {
         if (inHouse)
         {
            do
            {
               if (sRainHouse.volume < 0.5f)
               {
                  sRainHouse.volume += 0.05f;
               }
               if (sRainForest.volume > 0)
               {
                  sRainForest.volume -= 0.05f;
               }
               yield return new WaitForSeconds(0.2f);
            } while (sRainHouse.volume < 0.5f && sRainForest.volume > 0);
            soundInForest = false;
            soundInHouse = true;
         }
         else
         {
            do
            {
               if (sRainHouse.volume > 0)
               {
                  sRainHouse.volume -= 0.05f;
               }
               if (sRainForest.volume < 0.5f)
               {
                  sRainForest.volume += 0.05f;
               }
               yield return new WaitForSeconds(0.2f);
            } while (sRainForest.volume < 0.5f && sRainHouse.volume > 0);
            soundInForest = true;
            soundInHouse = false;
         }

         break;
      }
      StopCoroutine(ChangeSound());
   }

   private void OnTriggerStay(Collider other)
   {
      if (other.CompareTag("House"))
      {
         inHouse = true;
      }
   }

   private void OnTriggerExit(Collider other)
   {
      if (other.CompareTag("House"))
      {
         inHouse = false;
      }
   }
}
