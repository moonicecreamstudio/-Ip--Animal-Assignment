using NodeCanvas.Framework;
using ParadoxNotion.Design;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace NodeCanvas.Tasks.Actions {

	public class FollowAT : ActionTask {

        public float sampleFrequency;
        public float sampleRadius;

        private NavMeshAgent navAgent;
        private float timeSinceLastSample = 0f;
        private Vector3 lastTarget = Vector3.zero;


		// Boss Head Location
        public BBParameter<GameObject> bossHead;
        private List<Vector3> PositionsHistory = new List<Vector3>();
		public Vector3 bossHeadLocation;
		Blackboard bossHeadBlackboard;

        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit() {
            navAgent = agent.GetComponent<NavMeshAgent>();

            return null;
        }

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
			bossHeadBlackboard = bossHead.value.GetComponent<Blackboard>();
		}

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
            bossHeadLocation = bossHeadBlackboard.GetVariableValue<Vector3>("bossHeadLocation");
            Debug.Log(bossHeadLocation);

            PositionsHistory.Insert(0, bossHeadLocation);

            //int index = 0;

            //Vector3 point = PositionsHistory[Mathf.Min(index * 5, PositionsHistory.Count - 1)];
            //agent.transform.position = point;

            //timeSinceLastSample += Time.deltaTime;
            //if (timeSinceLastSample > sampleFrequency)
            //{
            //    timeSinceLastSample = 0f;
            //    if (lastTarget != bossHeadLocation)
            //    {
            //        NavMeshHit navMeshHit;
            //        bool wasValidPointDetected = NavMesh.SamplePosition(targetPosition.value, out navMeshHit, sampleRadius, NavMesh.AllAreas);

            //        if (wasValidPointDetected)
            //        {
            //            navAgent.SetDestination(navMeshHit.position);
            //        }
            //        lastTarget = targetPosition.value;

            //    }
            //}
            //targetPosition.value = targetTransform.position;
        }

		//Called when the task is disabled.
		protected override void OnStop() {
			
		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}
	}
}