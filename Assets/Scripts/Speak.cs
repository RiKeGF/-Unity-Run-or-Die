using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speak : MonoBehaviour
{
   //public int number = 0;
   public AudioSource[] speaks;
   public bool[] speaksPlay;
   public Animator dicas;

   private void Start()
   {
      StartCoroutine(order(0, 2f));
      StartCoroutine(order(1, 4f));
   }


   private void attAudio()
   {
      for (int i = 0; i < speaks.Length; i++)
      {
         if (speaks[i].isPlaying)
         {
            speaksPlay[i] = true;
         }
      }
   }

   public IEnumerator order(int number, float timer)
   {
      while (true)
      {
         yield return new WaitForSeconds(timer);
         if (!speaks[number].isPlaying && !speaksPlay[number])
         {
            speaks[number].Play();
         }
         if (number == 1)
         {
            yield return new WaitForSeconds(1f);
            dicas.SetTrigger("Dicas1");
         }
         else if (number == 3)
         {
            yield return new WaitForSeconds(1f);
            dicas.SetTrigger("Dicas2");
         }
         else if (number == 8)
         {
            yield return new WaitForSeconds(1f);
            dicas.SetTrigger("Dicas3");
         }
         attAudio();
         break;
      }
   }
   private void OnDisable()
   {
      StopAllCoroutines();
   }
}

