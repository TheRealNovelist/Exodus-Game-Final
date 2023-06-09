using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace CustomActions {

	[Category("Custom/Movement")]
	[Description("Rotate toward target with Lerping")]
	public class RotateTowards : ActionTask
	{
		[RequiredField] public BBParameter<Transform> body;
		[RequiredField] public BBParameter<Transform> target;
		public BBParameter<float> turnSpeed = 1f;
		public BBParameter<bool> useDeltaTime = true;
		public BBParameter<bool> freezeX = false;
		public BBParameter<bool> freezeY = false;
		public BBParameter<bool> freezeZ = false;


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
		protected override void OnUpdate()
		{
			var turn = useDeltaTime.value ? turnSpeed.value * Time.deltaTime : turnSpeed.value;
			body.value.RotateTowards(target.value, turn, freezeX.value, freezeY.value, freezeZ.value);
		}

		//Called when the task is disabled.
		protected override void OnStop() {
			
		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}
	}
}