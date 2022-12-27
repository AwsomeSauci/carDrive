using UnityEngine;
using System.Collections;
using TMPro;
using System;

public class WheelsController : MonoBehaviour
{
	[SerializeField] private float _maxAngle = 30;
	[SerializeField] private float _maxTorque = 300;
	[SerializeField] TextMeshProUGUI _speed;

	private Rigidbody _rb;
	private WheelCollider[] _wheels;
	private float _angle;
	private float _torque;
	private Quaternion q;
	private Vector3 p;
	private bool wPressed = false;
	private float maxspeed = 10;
	private float time1 = 3;
	private float time2 = 1;
	private float timer1 = 0;
	private float timer2 = 0;

	private void Start()
	{
		_wheels = GetComponentsInChildren<WheelCollider>();
		_rb = transform.GetComponent<Rigidbody>();
	}

    private void Update()
	{
		if (Input.GetKey(KeyCode.W))
		{
			if(timer1<time1)
			timer1 += Time.deltaTime;
			else
			{
				if (timer2 < time2)
				{
					timer2 += Time.deltaTime;
				}
				else
				{
					timer2 = 0;
					maxspeed += maxspeed * 0.05f;
				}
			}
		}
		if (Input.GetKeyUp(KeyCode.W))
		{
			maxspeed = 10;
			timer1 = 0;
			timer2 = 0;
		}
        
		
        _speed.text =_rb.velocity.magnitude.ToString("#.##");

		_angle = _maxAngle * Input.GetAxis("Horizontal");
		_torque = _maxTorque * Input.GetAxis("Vertical");
		foreach (WheelCollider wheel in _wheels)
		{
			if (wheel.transform.localPosition.z > 0)
				wheel.steerAngle = _angle;

			//if (wheel.transform.localPosition.z < 0)
				wheel.motorTorque = _torque;
			wheel.GetWorldPose(out p, out q);
			Transform shapeTransform = wheel.transform.GetChild(0);
			shapeTransform.position = p;
			shapeTransform.rotation = q;
		}
		if (_rb.velocity.magnitude > maxspeed) _rb.velocity = _rb.velocity.normalized* maxspeed;

    }
}
