using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions {

	public class HuntingTimeAT : ActionTask {

        public BBParameter<float> huntTimer;
        public BBParameter<bool> isHunting;
        public BBParameter<float> huntDuration;

        public BBParameter<GameObject> bossHealthUI;

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
            if (isHunting.value == true)
            {
                huntTimer.value += Time.deltaTime;
            }
            else
            {
                huntTimer.value = 0;
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