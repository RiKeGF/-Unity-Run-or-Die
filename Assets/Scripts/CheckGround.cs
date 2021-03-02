using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGround : MonoBehaviour
{
   public string typeGround;

   private void OnTriggerStay(Collider collision)
   {
      if (collision.CompareTag("GroundWood"))
      {
         typeGround = "GroundWood";
      }
      else if (collision.CompareTag("GroundLand"))
      {
         typeGround = "GroundLand";
      }
      else if (collision.CompareTag("GroundBrick"))
      {
         typeGround = "GroundBrick";
      }
      else if (collision.CompareTag("GroundGrass"))
      {
         typeGround = "GroundGrass";
      }
   }
}