using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour {

    public static Texture2D fsmTex;


    public static float xOrg;
    public static float yOrg;

    // The number of cycles of the basic noise pattern that are repeated
    // over the width and height of the texture.
    public static float scale = 1.0F;

    public static Texture2D noiseTex;
    private static Color[] pix;


    // Use this for initialization
    void Start () {

        noiseTex = new Texture2D(512, 512);
        pix = new Color[noiseTex.width * noiseTex.height];
        CalcNoise();
   

        fsmTex = new Texture2D(512, 512);
        GetComponent<Renderer>().material.mainTexture = fsmTex;

        for (int y = 0; y < fsmTex.height; y++)
        {
            for (int x = 0; x < fsmTex.width; x++)
            {
                Color color;
                if (x < 50 || x > 512-50)
                {
                    color = Color.green;
                }
                else
                {
                    color = Color.white;
                }
                fsmTex.SetPixel(x, y, color);
            }
        }
        fsmTex.Apply();
    }


    public static void CalcNoise()
    {
        // For each pixel in the texture...
        float y = 0.0F;

        while (y < noiseTex.height)
        {
            float x = 0.0F;
            while (x < noiseTex.width)
            {
                float xCoord = xOrg + x / noiseTex.width * scale;
                float yCoord = yOrg + y / noiseTex.height * scale;
                float signX = Random.Range(-1.0f, 1.0f);
                float signY = Random.Range(-1.0f, 1.0f);
                float sample = Mathf.PerlinNoise(xCoord, yCoord);
                pix[(int)y * noiseTex.width + (int)x] = new Color(signX*sample, signY*sample, sample);
                x++;
            }
            y++;
        }

        // Copy the pixel data to the texture and load it into the GPU.
        noiseTex.SetPixels(pix);
        noiseTex.Apply();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
