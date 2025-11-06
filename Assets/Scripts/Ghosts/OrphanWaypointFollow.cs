using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Cinemachine;
using UnityEngine.UIElements.Experimental;

namespace Platformer
{

    public class OrphanWaypointFollow : MonoBehaviour
    {
        public List<GameObject> waypointsOrphan;
        
        public float speed = 2;
        int index = 0;
        public bool isLoop = true;
        public GameObject orphan;
        public Animator orphanAnim;
      
   

    

     


       

     
      

        public void Running()
        {
            
                //Debug.Log("Running");
                Vector3 destination = waypointsOrphan[index].transform.position;
                Vector3 newPos = Vector3.MoveTowards(transform.position, waypointsOrphan[index].transform.position, speed * Time.deltaTime);

                transform.position = newPos;

                orphanAnim.SetBool("RunFWD", true);
                orphanAnim.SetBool("Idel", false);
                orphanAnim.SetBool("Attack", false);

                float distance = Vector3.Distance(transform.position, destination);

                if (distance <= 0.0001)
                {
                    if (index < waypointsOrphan.Count - 1)
                    {
                        index++;
                    }
                    else if (isLoop)
                    {
                        index = 0;
                    }
                }


                Vector3 directionToTarget = waypointsOrphan[index].transform.position - orphan.transform.position;
                directionToTarget.y = 0;

                Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
                orphan.transform.rotation = Quaternion.RotateTowards(orphan.transform.rotation, targetRotation, Time.deltaTime * 300f);
            

        }



    
    }
}
