using UnityEngine;
using System.Collections;

public class SuspensionController : MonoBehaviour {
	[Range(0, 20)]
	[SerializeField] private float _naturalFrequency = 10;

	[Range(0, 3)]
    [SerializeField] private float _dampingRatio = 0.8f;

	[Range(-1, 1)]
    [SerializeField] private float _forceShift = 0.03f;

    [SerializeField] private bool _setSuspensionDistance = true;

	private WheelCollider[] _wheels;
	private Vector3 _wheelRelativeBody;
    private JointSpring _spring;
	private float _distance;

    private void Start()
    {
		_wheels = GetComponentsInChildren<WheelCollider>();
    }

    void Update () {
		
		foreach (WheelCollider wc in _wheels) {
			_spring = wc.suspensionSpring;
			_spring.spring = Mathf.Pow(Mathf.Sqrt(wc.sprungMass) * _naturalFrequency, 2);
			_spring.damper = 2 * _dampingRatio * Mathf.Sqrt(_spring.spring * wc.sprungMass);

			wc.suspensionSpring = _spring;

			_wheelRelativeBody = transform.InverseTransformPoint(wc.transform.position);
			_distance = GetComponent<Rigidbody>().centerOfMass.y - _wheelRelativeBody.y + wc.radius;

			wc.forceAppPointDistance = _distance - _forceShift;

			
			if (_spring.targetPosition > 0 && _setSuspensionDistance)
				wc.suspensionDistance = wc.sprungMass * Physics.gravity.magnitude / (_spring.targetPosition * _spring.spring);
		}
	}

}
