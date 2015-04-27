﻿using UnityEngine;
using System.Collections;

public class RevolvingObject : MonoBehaviour {

	public float rotationalVelocity = 1f;
	public GameObject center;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (center != null) {
						float dx = transform.position.x - center.transform.position.x;
						float dy = transform.position.y - center.transform.position.y;
						float distance = Mathf.Sqrt (dx * dx + dy * dy);
						float theta = Mathf.Atan2 (dy, dx) + Time.deltaTime * rotationalVelocity * .5f;

						float newX = center.transform.position.x + distance * Mathf.Cos (theta);
						float newY = center.transform.position.y + distance * Mathf.Sin (theta);
						transform.position = new Vector3 (newX, newY, 0f);
				}
	}
}
