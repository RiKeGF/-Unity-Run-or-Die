using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
   public GameObject flashLight;
   public Light light;
   private float timerBlink = 10f;

   public AudioSource sFlashLightOn;
   public AudioSource sFlashLightOff;

   private void Awake()
   {
      flashLight.SetActive(false);
   }
   // Start is called before the first frame update
   void Start()
   {

   }

   // Update is called once per frame
   void Update()
   {
      if (Input.GetKeyDown(KeyCode.F))
      {
         light.enabled = !light.enabled;
         if (light.enabled)
         {
            sFlashLightOn.Play();
         }
         else if (!light.enabled)
         {
            sFlashLightOff.Play();
         }
      }

      if (light.enabled)
      {
         timerBlink -= Time.deltaTime;

         if (timerBlink < 0)
         {
            StartCoroutine(Blink());
         }
      }


   }

   IEnumerator Blink()
   {
      int count = 0;
      do
      {
         light.enabled = !light.enabled;
         yield return new WaitForSeconds(Random.Range(0.2f, 0.4f));
         count++;
      } while (count < 2);
      light.enabled = true;
      timerBlink = Random.Range(10, 15);
   }

   private void OnDisable()
   {
      StopAllCoroutines();
   }
}
