using NodeCanvas.Framework;
using ParadoxNotion.Design;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System;
using System.Xml.Linq;


namespace NodeCanvas.Tasks.Actions {

	// Lots of issues, for some reason I can't add arrays to BB, List isn't allowing me to extract the value

	public class PathAT : ActionTask {
        public BBParameter<Vector3> targetPosition;
        public BBParameter<Vector3> bossHeadLocation;
        //public BBParameter<List<Vector3>> targetList = new BBParameter<List<Vector3>>(); // Took 2 hours to find this
        public int currentPosition;
        public Transform targetTransform; 
		public Vector3 currentTarget;
		public Transform[] targetList2;
        float timer1;

        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit() {
			return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
            //targetList = new BBParameter<List<Vector3>>();
            //targetTransform.position = targetList.value[3];
		}

		//Called once per frame while the action is active.
		protected override void OnUpdate() {

			float distance = Vector3.Distance(bossHeadLocation.value, targetTransform.position);
			targetTransform.position = targetList2[currentPosition].transform.position;
            targetPosition.value = targetTransform.position;

			if (currentPosition == 56)
			{
				currentPosition = 0;
            }

			if (distance <= 20)
			{
				timer1 += Time.deltaTime;
				if (timer1 > 0.2f)
				{
                    currentPosition += 1;
					timer1 = 0;
                }
            }

		}

		//Called when the task is disabled.
		protected override void OnStop() {
			
		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}
	}
}