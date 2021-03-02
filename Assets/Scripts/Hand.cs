using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hand : MonoBehaviour
{
   public FirstPersonController scFPS;

   public GameObject flashLight;
   public Text txtInfo;
   private bool catchOuija;

   public GameObject ouijaRoom;
   public GameObject ouijaRoomLocal;
   public GameObject ouijaGav;

   public Animator pointer;
   public Animator demonSpawn;
   public Animator dicas;
   public AudioSource demonGrr;
   public GameObject pentagram;
   public Speak scSpeak;

   public GameObject tabuleiro;
   public GameObject loc;
   public DemonSpawn scDemonSpawn;

   public bool block;



   private void Awake()
   {
      ouijaRoom.SetActive(false);
      ouijaRoomLocal.SetActive(false);
      txtInfo.gameObject.SetActive(false);
      pentagram.SetActive(false);
   }

   private void OnTriggerEnter(Collider other)
   {
      if (Input.GetKey(KeyCode.E))
      {
         if (other.name.Equals("P1") || other.name.Equals("P2"))
         {
            if (!other.GetComponent<SoundDoor>().isOpen)
            {
               other.GetComponent<Animator>().SetTrigger("Open");

               other.GetComponent<SoundDoor>().isOpen = true;

               if (!other.GetComponent<SoundDoor>().sOpenDoor.isPlaying)
               {
                  other.GetComponent<SoundDoor>().sOpenDoor.Play();
               }

               StartCoroutine(CloseDoor(other.GetComponent<SoundDoor>()));
            }

         }
         else if (other.name.Equals("PS") )
         {
            if (scDemonSpawn.start)
            {
               if (!other.GetComponent<SoundDoor>().isOpen)
               {
                  other.GetComponent<Animator>().SetTrigger("Open");

                  other.GetComponent<SoundDoor>().isOpen = true;

                  if (!other.GetComponent<SoundDoor>().sOpenDoor.isPlaying)
                  {
                     other.GetComponent<SoundDoor>().sOpenDoor.Play();
                  }

                  StartCoroutine(CloseDoor(other.GetComponent<SoundDoor>()));
               }
            }
            else
            {
               other.GetComponent<AudioSource>().Play();
            }
            
         }
         else if (other.name.Equals("Loocked"))
         {
            other.GetComponent<AudioSource>().Play();
         }
         else if (other.name.Equals("FlashLight"))
         {
            other.GetComponent<AudioSource>().Play();
            Destroy(other.gameObject);
            flashLight.SetActive(true);
         }
         else if (other.name.Equals("Gaveta"))
         {
            other.GetComponent<Animator>().SetBool("Open", !other.GetComponent<Animator>().GetBool("Open"));

            if (!scSpeak.speaks[2].isPlaying)
            {
               StartCoroutine(scSpeak.order(2, 1.5f));
               StartCoroutine(scSpeak.order(2, 3f));
               StartCoroutine(scSpeak.order(3, 4f));
            }
         }
      }

      if (other.name.Equals("P1") || other.name.Equals("P2") || other.name.Equals("PS"))
      {
         txtInfo.text = "Pressione 'E' para abrir";
         txtInfo.gameObject.SetActive(true);
      }
      else if (other.name.Equals("Gaveta"))
      {
         txtInfo.text = "Pressione 'E' para abrir/fechar";
         txtInfo.gameObject.SetActive(true);
      }
      else if (other.name.Equals("FlashLight"))
      {
         txtInfo.text = "Pressione 'E' para pegar";
         txtInfo.gameObject.SetActive(true);
      }
      else if (other.name.Equals("Tabuleiro"))
      {
         if (ouijaGav.GetComponent<Ouija>().unLockeed)
         {
            txtInfo.text = "Pressione 'E' para pegar";
            txtInfo.gameObject.SetActive(true);
         }
      }
      else if (other.name.Equals("TabuleiroLocal"))
      {
         txtInfo.text = "Pressione 'E' para colocar";
         txtInfo.gameObject.SetActive(true);
      }
      
   }

   private void OnTriggerStay(Collider other)
   {
      if (Input.GetKey(KeyCode.E))
      {
         if (other.name.Equals("P1") || other.name.Equals("P2"))
         {
            if (!other.GetComponent<SoundDoor>().isOpen)
            {
               other.GetComponent<Animator>().SetTrigger("Open");

               other.GetComponent<SoundDoor>().isOpen = true;

               if (!other.GetComponent<SoundDoor>().sOpenDoor.isPlaying)
               {
                  other.GetComponent<SoundDoor>().sOpenDoor.Play();
               }

               StartCoroutine(CloseDoor(other.GetComponent<SoundDoor>()));
            }

         }
         else if (other.name.Equals("PS"))
         {
            if (scDemonSpawn.start)
            {
               if (!other.GetComponent<SoundDoor>().isOpen)
               {
                  other.GetComponent<Animator>().SetTrigger("Open");

                  other.GetComponent<SoundDoor>().isOpen = true;

                  if (!other.GetComponent<SoundDoor>().sOpenDoor.isPlaying)
                  {
                     other.GetComponent<SoundDoor>().sOpenDoor.Play();
                  }

                  StartCoroutine(CloseDoor(other.GetComponent<SoundDoor>()));
               }
            }
            else
            {
               other.GetComponent<AudioSource>().Play();
            }

         }
         else if (other.name.Equals("Loocked"))
         {
            if (!other.GetComponent<AudioSource>().isPlaying)
            {
               other.GetComponent<AudioSource>().Play();
            }

         }
         else if (other.name.Equals("FlashLight"))
         {
            other.GetComponent<AudioSource>().Play();
            Destroy(other.gameObject);
            flashLight.SetActive(true);
         }
         else if (other.name.Equals("Gaveta"))
         {
            if (!other.GetComponent<Animator>().GetBool("Open"))
            {
               other.GetComponent<Animator>().SetBool("Open", true);

               if (!scSpeak.speaks[3].isPlaying)
               {
                  StartCoroutine(scSpeak.order(2, 1.5f));
                  StartCoroutine(scSpeak.order(2, 3f));
                  StartCoroutine(scSpeak.order(3, 5f));
               }
            }
            else
            {
               if (catchOuija)
               {
                  other.GetComponent<Animator>().SetBool("Open", false);
               }
            }

         }
         if (other.name.Equals("Tabuleiro"))
         {
            if (other.GetComponent<Ouija>().unLockeed)
            {
               other.gameObject.SetActive(false);
               catchOuija = true;
               ouijaRoomLocal.SetActive(true);
            }
         }
         else if (other.name.Equals("TabuleiroLocal"))
         {
            if (other.gameObject.active)
            {
               ouijaRoomLocal.SetActive(false);
               ouijaRoom.SetActive(true);
               txtInfo.gameObject.SetActive(false);
               block = true;
               StartCoroutine(Conversation());
            }
         }

      }
   }

   private void OnTriggerExit(Collider other)
   {
      if (other.name.Equals("P1") || other.name.Equals("P2") || other.name.Equals("PS") || other.name.Equals("Gaveta") || other.name.Equals("FlashLight") || other.name.Equals("Tabuleiro") || other.name.Equals("TabuleiroLocal"))
      {
         txtInfo.gameObject.SetActive(false);
      }
   }

   IEnumerator CloseDoor(SoundDoor door)
   {
      while (true)
      {
         yield return new WaitForSeconds(4.5f);
         break;
      }
      door.sCloseDoor.Play();
      door.isOpen = false;
      StopCoroutine("CloseDoor");
   }

   IEnumerator Conversation()
   {

      scFPS.transform.position = loc.transform.position;

      scFPS.transform.LookAt(tabuleiro.transform);

      while (true)
      {
         yield return new WaitForSeconds(2f);
         break;
      }
      StartCoroutine(scFPS.agaixando());
      while (true)
      {
         yield return new WaitForSeconds(1f);
         break;
      }
      StartCoroutine(scSpeak.order(4, 1f));
      while (true)
      {
         yield return new WaitForSeconds(3f);
         break;
      }
      pointer.SetTrigger("Q1");
      while (true)
      {
         yield return new WaitForSeconds(1f);
         break;
      }
      StartCoroutine(scSpeak.order(5, 0.5f));
      StartCoroutine(scSpeak.order(6, 3f));
      while (true)
      {
         yield return new WaitForSeconds(5f);
         break;
      }
      pointer.SetTrigger("Q2");
      while (true)
      {
         yield return new WaitForSeconds(2f);
         break;
      }

      pentagram.SetActive(true);
      demonSpawn.SetTrigger("Spawn");
      demonGrr.Play();

      scDemonSpawn.startTimer = true;

      while (true)
      {
         yield return new WaitForSeconds(1f);
         break;
      }
      block = false;
      StartCoroutine(scFPS.levantando());
      StartCoroutine(scSpeak.order(7, 1f));
      while (true)
      {
         yield return new WaitForSeconds(1f);
         break;
      }
    
      StartCoroutine(scSpeak.order(8, 2f));

   }

   private void OnDisable()
   {
      StopAllCoroutines();
   }
}
