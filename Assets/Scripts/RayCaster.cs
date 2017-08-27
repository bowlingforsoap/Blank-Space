using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCaster : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Camera currentCam = GetComponent<Camera>();
        Ray ray = currentCam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * 10.0f);
		Physics.Raycast (ray, out hit);
//        if (Physics.Raycast(ray, out hit))
//            print("I'm looking at " + hit.transform.name + " UV: " + hit.textureCoord);



        Renderer rend = hit.transform.GetComponent<Renderer>();
        MeshCollider meshCollider = hit.collider as MeshCollider;

        if (rend == null || rend.sharedMaterial == null || rend.sharedMaterial.mainTexture == null || meshCollider == null)
            return;

        Texture2D tex = rend.material.mainTexture as Texture2D;
        Vector2 pixelUV = hit.textureCoord;
        pixelUV.x *= tex.width;
        pixelUV.y *= tex.height;
		Circle (tex, (int)pixelUV.x, (int)pixelUV.y, 10);

        tex.Apply();
    }

	public void Circle(Texture2D tex, int cx, int cy, int r)
	{
		int x, y, px, nx, py, ny, d;

		for (x = 0; x <= r; x++)
		{
			d = (int)Mathf.Ceil(Mathf.Sqrt(r * r - x * x));
			for (y = 0; y <= d; y++)
			{
				px = cx + x;
				nx = cx - x;
				py = cy + y;
				ny = cy - y;

                Color currentColor = tex.GetPixel(px, py);
                currentColor.a = 1.0f;
				tex.SetPixel(px, py, currentColor);
                currentColor = tex.GetPixel(nx, py);
                currentColor.a = 1.0f;
                tex.SetPixel(nx, py, currentColor);

                currentColor = tex.GetPixel(px, ny);
                currentColor.a = 1.0f;
                tex.SetPixel(px, ny, currentColor);
                currentColor = tex.GetPixel(nx, ny);
                currentColor.a = 1.0f;
                tex.SetPixel(nx, ny, currentColor);

			}
		}    
	}
}
