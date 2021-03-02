using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
   public Fade scFade;

   private void Start()
   {
      Cursor.visible = true;
      Cursor.lockState = CursorLockMode.None;
   }

   public void LoadScene()
   {
      StartCoroutine(Load());
   }

   public void Exit()
   {
      Application.Quit();
   }

   IEnumerator Load()
   {
      scFade.FadeOut();
      while (true)
      {
         yield return new WaitForSeconds(3f);
         SceneManager.LoadScene("Scene");
         break;
      }
   }

   private void OnDisable()
   {
      StopAllCoroutines();
   }

}
