using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {

    public Vector3 floorSize;
    float dir;
    float delta;
    int counter;
    
    //Get it

	// Use this for initialization
	void Start ()
    {
        dir = 1.0f;
        delta = 0.1f;
        counter = 0;
    }


    void doVectorMap()
    {
        float posX = (transform.position.x + floorSize.x / 2.0f) / floorSize.x;
        int texPosX = Mathf.FloorToInt(posX * Floor.fsmTex.width);

        float posZ = (transform.position.z + floorSize.z / 2.0f) / floorSize.z;
        int texPosZ = Mathf.FloorToInt(posZ * Floor.fsmTex.height);

        for (int y = 0; y < Floor.fsmTex.height; y++)
        {
            for (int x = 0; x < Floor.fsmTex.width; x++)
            {
                float xForTex = (texPosX-x) / (float)Floor.fsmTex.width;
                float yForTex = (y - texPosZ) / (float)Floor.fsmTex.height;
                float zForTex = 0;
                float alphaForTex = 0;
                Color c = new Color(xForTex, yForTex, zForTex, alphaForTex);
                if (x == 0 && y == 0) Debug.Log(c);
                Floor.fsmTex.SetPixel(x, y, c);
            }
        }
        Floor.fsmTex.Apply();
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        float posX = (transform.position.x + floorSize.x / 2.0f) / floorSize.x;
        int texPosX = Mathf.FloorToInt(posX * Floor.fsmTex.width);

        float posZ = (transform.position.z + floorSize.z / 2.0f) / floorSize.z;
        int texPosZ = Mathf.FloorToInt(posZ * Floor.fsmTex.height);

        //Color color = Floor.fsmTex.GetPixel(texPosX, texPosZ);
        //Color newColor = new Color(0.0f, 0.0f, 1.0f, 0.5f);
        //Floor.fsmTex.SetPixel(texPosX, texPosZ, newColor);

        //if(color.r == 0 && color.g == 1 && color.b == 0)
        //{
        //    Debug.Log("posX=" + posX);  // 0.5 OK
        //    Debug.Log("posZ=" + posZ);  // 0.5 OK
        //    Debug.Log("texPosX=" + texPosX); // 256
        //    Debug.Log("texPosZ=" + texPosZ);  // 256
        //    Debug.Log("color=" + color);  // White
        //    dir = -dir;
        //    //doVectorMap();
        //}

        Vector3 pos = transform.position;
        Color noiseColor = Floor.noiseTex.GetPixel(texPosX, texPosZ);
        //Debug.Log("Color pixel en posicion x, z: " + Floor.noiseTex.GetPixel(texPosX, texPosZ));
        //Da --> Color pixel en posicion x, z: RGBA(0.000, 0.000, 0.263, 1.000) casi SIEMPRE
        //Cuando cambia de posicion los valores en (X,Y) cambian pero cuando el color se empareja, regresa
        
        pos.x += noiseColor.r;
        pos.z += noiseColor.g;
        //pos.x += dir * delta;
        transform.position = pos;

        counter++;
        if(counter % 30 == 0)
        {
            counter = 0;
            //Floor.fsmTex.Apply();
            doVectorMap();
            Floor.CalcNoise();
        }
    }
}
