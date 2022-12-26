using UnityEngine;
using System.Collections;

public class WheelsController : MonoBehaviour
{
	[SerializeField] private float _maxAngle = 30;
	[SerializeField] private float _maxTorque = 300;

	private WheelCollider[] _wheels;
	private float _angle;
	private float _torque;
	private Quaternion q;
	private Vector3 p;

	private void Start()
	{
		_wheels = GetComponentsInChildren<WheelCollider>();
	}

    private void Update()
	{
		_angle = _maxAngle * Input.GetAxis("Horizontal");
		_torque = _maxTorque * Input.GetAxis("Vertical");
		foreach (WheelCollider wheel in _wheels)
		{
			if (wheel.transform.localPosition.z > 0)
				wheel.steerAngle = _angle;

			if (wheel.transform.localPosition.z < 0)
				wheel.motorTorque = _torque;
			wheel.GetWorldPose(out p, out q);
			Transform shapeTransform = wheel.transform.GetChild(0);
			shapeTransform.position = p;
			shapeTransform.rotation = q;
		}
	}
}
