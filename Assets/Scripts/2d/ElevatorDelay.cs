﻿using UnityEngine;
using System.Collections;

public class ElevatorDelay : MonoBehaviour {

	[SerializeField] private TransformDirection direction;
	[SerializeField] private float transformSpeed;
	[SerializeField] private float transformLength;
	[SerializeField] private float transformDelay;

	private float fixedStopTime;
	private float stopTimer;
	private int binar;
	private bool startDelay;

	public enum TransformDirection{
		upDown,
		leftRight
	}

    // Use this for initialization
    IEnumerator Start () {

        yield return new WaitForSeconds(2f);
        if (direction != TransformDirection.leftRight && direction != TransformDirection.upDown)
			throw new UnityException ("<color=red>Error: Tranform Direction Error in ElevatorBehavior2D.</color>");
		if (transformSpeed <= 0 || transformLength <= 0)
			throw new UnityException ("<color=red>Error: Tranform Speed or Length Must be Greater than Zero.</color>");
		fixedStopTime = transformLength / transformSpeed;
		stopTimer = fixedStopTime;
		binar = 1;
		startDelay = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate(){
		stopTimer -= Time.deltaTime;

		if (stopTimer >= 0) {
			MovePlatform (binar);
		} else {

			if(!startDelay)
				StartCoroutine (DelayTransform ());
		}
	}


	private void MovePlatform(int i){
		if (direction == TransformDirection.upDown) {
			transform.Translate (i * Vector3.up * (Time.deltaTime * transformSpeed), Space.World);
		} else if (direction == TransformDirection.leftRight) {
			transform.Translate (i * Vector3.right * (Time.deltaTime * transformSpeed), Space.World);
		}

	}


	IEnumerator DelayTransform(){
		startDelay = true;
		yield return new WaitForSeconds (transformDelay);
		//Debug.Log("Platform Delay");

		stopTimer = fixedStopTime;
		binar = binar*(-1);

		startDelay = false;
		//Debug.Log("startDelay false");
	}
	/*
	void OnCollisionEnter(Collision other)
	{
		if (other.collider.CompareTag("player"))
		{
			other.transform.parent = transform;
		}
	}
	*/
	//void OnCollisionExit(Collision other)
	//{
	//	if (other.collider.CompareTag("player"))
	//	{
	//		other.transform.parent = null;
	//	}
	//}

}
