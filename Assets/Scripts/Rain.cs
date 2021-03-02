using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rain : MonoBehaviour
{
   public GameObject thunder;
   public Light light;
   public AudioSource sThunder;
   public ParticleSystem psRain;
   public GameObject player;
   private float timerThunder = 10;


   // Start is called before the first frame update
   void Start()
   {
      thunder.SetActive(false);

   }

   // Update is called once per frame
   void Update()
   {
      timerThunder -= Time.deltaTime;
      if (timerThunder < 0)
      {
         StartThunder();
         timerThunder = Random.Range(10,20);
      }

      psRain.transform.position = new Vector3(player.transform.position.x, psRain.transform.position.y, player.transform.position.z);
   }

   private void StartThunder()
   {
      StartCoroutine(Thunder());
      if (!sThunder.isPlaying)
      {
         sThunder.Play();
      }
   }

   IEnumerator Thunder()
   {
      int count = 0;
      
      while (count < 3)
      {        
         yield return new WaitForSeconds(Random.Range(0.09f, 0.2f));
         thunder.SetActive(true);
         light.intensity = 0.5f;
         yield return new WaitForSeconds(Random.Range(0.09f, 0.2f));
         thunder.SetActive(false);
         light.intensity = 0.2f;
         count++;
      }

      StopCoroutine(Thunder());

   }
}
