using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
   private Animator fade;

   private void Awake()
   {
      fade = GameObject.FindGameObjectWithTag("Fade").GetComponent<Animator>();
   }

   public void FadeIn()
   {
      fade.SetTrigger("FadeIn");
   }

   public void FadeOut()
   {
      fade.SetTrigger("FadeOut");
   }
}
