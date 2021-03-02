using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Demon : MonoBehaviour
{
   private NavMeshAgent navMesh;

   public GameObject player;
   public Transform[] AIPointsHouse;
   public Transform[] TeleportPointsHouse;
   public Transform[] AIPointsForest;
   public Transform[] TeleportPointsForest;
   public AudioSource sGrr;
   public AudioSource[] steps;
   public ParticleSystem psSmokeTeleport;
   public Image jumpScare;
   public CheckHouse scCheckHouse;
   public Fade scFade;
   public DemonSpawn scDemonSpawn;
   public Animator anim;

   [SerializeField] private bool seePlayer;
   [SerializeField] private bool startDelaySeePlayer;
   [SerializeField] private bool chase;
   [SerializeField] private bool block;

   [SerializeField] private float distancePlayer;
   [SerializeField] private float distanceAIPoint;
   [SerializeField] private float delaySeePlayer;
   [SerializeField] private float timerTeleport = 8f;
   [SerializeField] private float timerGrr = 8f;
   [SerializeField] private float timerStepsActual = 1f;
   [SerializeField] private float timerSteps = 1f;

   [SerializeField] private int AIPointCurrent;
   [SerializeField] private int IDTeleport;
   [SerializeField] private int IDAlreadyTeleport;

   public float distancePercept;
   public float speed;
   public float speedChase;

   private void Awake()
   {
      block = true;
      jumpScare.enabled = false;
   }

   void Start()
   {
      AIPointCurrent = 10;
      IDTeleport = 2;
      navMesh = transform.GetComponent<NavMeshAgent>();
      //navMesh.Warp(TeleportPointsHouse[IDTeleport].position);
      navMesh.enabled = false;

   }


   void Update()
   {
      if (scDemonSpawn.start && scDemonSpawn != null)
      {
         AIPointCurrent = 10;
         IDTeleport = 2;
         navMesh.Warp(TeleportPointsHouse[IDTeleport].position);
         navMesh.enabled = true;
         block = false;
         Destroy(scDemonSpawn.gameObject);
      }
      if (!block)
      {
         Steps();
         distancePlayer = Vector3.Distance(player.transform.position, this.transform.position);
         if (scCheckHouse.inHouse)
         {
            distanceAIPoint = Vector3.Distance(AIPointsHouse[AIPointCurrent].transform.position, this.transform.position);
         }
         else
         {
            distanceAIPoint = Vector3.Distance(AIPointsForest[AIPointCurrent].transform.position, this.transform.position);
         }

         if (navMesh.speed > 0 && navMesh.speed < 8)
         {
            anim.SetBool("Walking", true);
            anim.SetBool("Run", false);
         }
         else if (navMesh.speed >= 8)
         {
            anim.SetBool("Walking", false);
            anim.SetBool("Run", true);
         }
         else
         {
            anim.SetBool("Walking", false);
            anim.SetBool("Run", false);
         }

         SeePlayer();
         Teleport();

         Sound("Grr");

         if (distancePlayer > distancePercept)
         {
            SearchPlayer();
         }
         else
         {
            if (seePlayer)
            {
               ChasePlayer();

            }
            else
            {
               SearchPlayer();
            }
         }

         if (distancePlayer < 2f && !jumpScare.enabled)
         {
            jumpScare.enabled = true;

            if (!jumpScare.GetComponent<AudioSource>().isPlaying)
            {
               jumpScare.GetComponent<AudioSource>().Play();
               StartCoroutine(GameEnd());
            }

         }

         if (distanceAIPoint <= 2)
         {
            if (scCheckHouse.inHouse)
            {
               do
               {
                  AIPointCurrent = Random.Range(0, AIPointsHouse.Length);
               } while (AIPointsHouse[AIPointCurrent].GetComponent<ID>().id != IDTeleport);
            }
            else
            {
               do
               {
                  AIPointCurrent = Random.Range(0, AIPointsForest.Length);
               } while (AIPointsForest[AIPointCurrent].GetComponent<ID>().id != IDTeleport);
            }

            SearchPlayer();
         }

         if (startDelaySeePlayer)
         {
            delaySeePlayer += Time.deltaTime;

            if (delaySeePlayer >= 5 && !seePlayer)
            {
               startDelaySeePlayer = false;
               seePlayer = false;
               delaySeePlayer = 0;
               chase = false;
            }
         }
      }


   }

   void Steps()
   {
      if (navMesh.speed > 0 && navMesh.speed < 8)
      {
         timerStepsActual = 1.5f;
      }
      else if (navMesh.speed >= 8)
      {
         timerStepsActual = 0.5f;
      }

      if (navMesh.speed > 0)
      {
         timerSteps -= Time.deltaTime;

         if (timerSteps < 0)
         {
            int n = Random.Range(0, steps.Length);

            steps[n].Play();

            timerSteps = timerStepsActual;
         }
      }
   }

   void Teleport()
   {
      if (!seePlayer && !chase)
      {
         timerTeleport -= Time.deltaTime;

         if (timerTeleport < 0)
         {
            if (scCheckHouse.inHouse)
            {
               do
               {
                  IDTeleport = Random.Range(0, TeleportPointsHouse.Length);
               } while (IDTeleport == IDAlreadyTeleport);

               ParticleSystem smokeTemp = Instantiate(psSmokeTeleport, this.transform.position + new Vector3(0, 1, 0), Quaternion.identity);

               navMesh.Warp(TeleportPointsHouse[IDTeleport].position);

               IDAlreadyTeleport = IDTeleport;

               do
               {
                  AIPointCurrent = Random.Range(0, AIPointsHouse.Length);
               } while (AIPointsHouse[AIPointCurrent].GetComponent<ID>().id != IDTeleport);
               SearchPlayer();
               timerTeleport = 8f;
            }
            else
            {
               do
               {
                  IDTeleport = Random.Range(0, TeleportPointsForest.Length);
               } while (IDTeleport == IDAlreadyTeleport);

               ParticleSystem smokeTemp = Instantiate(psSmokeTeleport, this.transform.position + new Vector3(0, 1, 0), Quaternion.identity);

               navMesh.Warp(TeleportPointsForest[IDTeleport].position);

               IDAlreadyTeleport = IDTeleport;

               do
               {
                  AIPointCurrent = Random.Range(0, AIPointsForest.Length);
               } while (AIPointsForest[AIPointCurrent].GetComponent<ID>().id != IDTeleport);
               SearchPlayer();
               timerTeleport = 8f;

            }
         }

      }
   }

   void SeePlayer()
   {
      RaycastHit hit;
      Vector3 local = this.transform.position;
      Vector3 destiny = player.transform.position;
      Vector3 direction = destiny - local;

      if (Physics.Raycast(local, direction, out hit, 500) && distancePlayer < distancePercept)
      {
         if (hit.collider.gameObject.CompareTag("Player"))
         {
            seePlayer = true;
         }
         else
         {
            seePlayer = false;
         }
      }
      if (distancePlayer >= distancePercept)
      {
         seePlayer = false;
      }

      if (seePlayer)
      {
         transform.LookAt(player.transform);
      }

   }

   void SearchPlayer()
   {
      if (!chase)
      {
         navMesh.acceleration = 5;
         navMesh.speed = speed;
         if (scCheckHouse.inHouse)
         {
            navMesh.destination = AIPointsHouse[AIPointCurrent].position;
         }
         else
         {
            navMesh.destination = AIPointsForest[AIPointCurrent].position;
         }

      }
      else if (chase && !seePlayer)
      {
         startDelaySeePlayer = true;
      }

   }

   void ChasePlayer()
   {
      chase = true;
      navMesh.acceleration = 8;
      navMesh.speed = speedChase;
      navMesh.destination = player.transform.position - new Vector3(0, 1f, 0);
   }

   void Sound(string name)
   {
      switch (name)
      {
         case "Grr":
         {
            timerGrr -= Time.deltaTime;

            if (timerGrr < 0)
            {
               if (!sGrr.isPlaying)
               {
                  sGrr.Play();
               }
               timerGrr = Random.Range(10, 20);
            }
            break;
         }
      }
   }

   IEnumerator GameEnd()
   {
      while (true)
      {
         yield return new WaitForSeconds(2);
         scFade.FadeOut();
         yield return new WaitForSeconds(3);
         SceneManager.LoadScene("Menu");
      }
   }
}
