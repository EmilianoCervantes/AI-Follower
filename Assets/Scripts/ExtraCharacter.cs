using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraCharacter : MonoBehaviour {

	// Use this for initialization
	public Vector3 floorSize;
	private long counter;
	void Start () {
		counter = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float posX = (transform.position.x + floorSize.x / 2.0f) / floorSize.x;
		int texPosX = Mathf.FloorToInt(posX * Floor.fsmTex.width);

		float posZ = (transform.position.z + floorSize.z / 2.0f) / floorSize.z;
		int texPosZ = Mathf.FloorToInt(posZ * Floor.fsmTex.height);

		Color color = Floor.fsmTex.GetPixel(texPosX, texPosZ);
		counter++;
		if(counter % 30 == 0)
		{
			counter = 0;
			Vector3 pos = transform.position;
			Vector3 delta = new Vector3(color.r - 0.5f, 0, -(color.g - 0.5f));
			pos += delta;
			transform.position = pos;
		}
	}
}