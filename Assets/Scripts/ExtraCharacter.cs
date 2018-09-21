using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraCharacter : MonoBehaviour
{
	public Vector3 floorSize;

	private int caso = 0; //1-4 (amarillo, rojo, azul muy oscuro & verde)
	private int counter;

	// Use this for initialization
	void Start () {
		counter = 0;
	}

	int calcularPosInicial(float colorRojo)
	{
		if (colorRojo > 0.4)
		{
			// Zona roja
			Vector3 posicion = transform.position;
			posicion.x += 0.05f;
			posicion.z += 0.05f;
			transform.position = posicion;

		} else if (colorRojo < 0.4 && colorRojo > 0)
		{
			// Zona amarilla o morada
			Vector3 posicion = transform.position;
			posicion.x += 0.05f;
			transform.position = posicion;
		} else if (colorRojo == 0)
		{
			// Zona verde o cercano
			Vector3 posicion = transform.position;
			posicion.x -= 0.05f;
			posicion.z -= 0.05f;
			transform.position = posicion;
		}
		return 1;
	}
	
	// Update is called once per frame
	void Update ()
	{
		float posX = (transform.position.x );
		int texPosX = Mathf.FloorToInt(posX * Floor.fsmTex.width);

		float posZ = (transform.position.z );
		int texPosZ = Mathf.FloorToInt(posZ * Floor.fsmTex.height);

		Vector3 pos = transform.position;
		Color noiseColor = Floor.noiseTex.GetPixel(texPosX, texPosZ);
		//Debug.Log("Color pixel en follower: " + Floor.noiseTex.GetPixel(texPosX, texPosZ));
		
		//if para detectar color
		Debug.Log("Noise color r: "+ noiseColor.r);
		Debug.Log("Noise color g: "+ noiseColor.g);
		Debug.Log("Noise color b: "+ noiseColor.b);
		pos.x += (noiseColor.r)/15;
		//pos.z -= noiseColor.g;
		switch (caso)
		{
			case 1:
				pos.x += (noiseColor.r)/15;
				break;
			case 2:
				pos.x += (noiseColor.r)/15;
				pos.z += (noiseColor.g)/15;
				break;
			case 3:
				pos.z += (noiseColor.g)/15;
				break;
			case 4:
				pos.x -= (noiseColor.r)/15;
				pos.z -= (noiseColor.g)/15;
				break;
			default:
				break;
		}
		
		transform.position = pos;
		if (counter == 0)
		{
			//caso = calcularPosInicial(noiseColor.r);
		}
		caso = calcularPosInicial(noiseColor.r);
		counter++;
		/*if(counter % 30 == 0)
		{
			counter = 0;
			Debug.Log("Pos en follower: " + Floor.noiseTex.GetPixel(Mathf.FloorToInt(noiseColor.r), Mathf.FloorToInt(noiseColor.g)));
		}*/
	}
}
