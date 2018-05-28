using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour {


	private void GetInput()
	{
		m_verticalInput = Input.GetAxis("Vertical");
		m_horizontalInput = Input.GetAxis("Horizontal");
	}

	private void Steer()
	{
		m_steerAngle = Mathf.Lerp(m_steerAngle, maxSteerAngle * m_horizontalInput, 10 * Time.deltaTime);
		frontDriverW.steerAngle = m_steerAngle;
		frontPassengerW.steerAngle = m_steerAngle;
	}

	private void Accelerate()
	{
		frontDriverW.motorTorque = m_verticalInput * motorForce;
		frontPassengerW.motorTorque = m_verticalInput * motorForce;
	}

	private void UpdateWheelPoses()
	{
		UpdateWheelPose(frontDriverW, frontDriverT);
		UpdateWheelPose(frontPassengerW, frontPassengerT);
		UpdateWheelPose(rearDriverW, rearDriverT);
		UpdateWheelPose(rearPassengerW, rearPassengerT);
	}

	private void UpdateWheelPose(WheelCollider _collider, Transform _transform)
	{
		Vector3 _pos = _transform.position;
		Quaternion _rot = _transform.rotation;

		_collider.GetWorldPose(out _pos, out _rot);
		_transform.position = _pos;
		_transform.rotation = _rot;
	}

	private void FixedUpdate()
	{
		GetInput();
		Steer();
		Accelerate();
		UpdateWheelPoses();
	}

	private float m_horizontalInput;
	private float m_verticalInput;
	private float m_steerAngle;

	public WheelCollider frontDriverW, frontPassengerW;
	public WheelCollider rearDriverW, rearPassengerW;
	public Transform frontDriverT, frontPassengerT;
	public Transform rearDriverT, rearPassengerT;
	public DriveType driveType;
	public float maxSteerAngle = 30;
	public float motorForce = 50;

	public enum DriveType
	{
		FrontWheel,
		RearWheel,
		AllWheel
	}

}
