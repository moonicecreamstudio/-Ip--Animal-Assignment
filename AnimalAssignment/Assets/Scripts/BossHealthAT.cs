using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.UI;

namespace NodeCanvas.Tasks.Actions {

	public class BossHealthAT : ActionTask {

        public Slider bossHealth;
        public BBParameter<float> bossCurrentHealth;
        public BBParameter<float> bossMaxHealth;
        public BBParameter<Vector3> bossHeadLocation;

        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit() {
			return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
		}

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
            bossHeadLocation.value = agent.transform.position;
            bossHealth.value = (bossCurrentHealth.value / bossMaxHealth.value) * 100;
			Debug.Log(bossHealth.value);
        }

		//Called when the task is disabled.
		protected override void OnStop() {
			
		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}
	}
}