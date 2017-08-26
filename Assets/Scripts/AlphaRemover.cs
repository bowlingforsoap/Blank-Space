using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaRemover {

    private Texture2D texture;
    private long pixelsRevealed = 0;
    private long textureSize;

	public void SetTexture(Texture2D tex)
    {
        texture = tex;
        textureSize = texture.width * texture.height;
        pixelsRevealed = 0;
	}
	
	public void Remove(Vector2 textCoord)
    {
        Vector2 pixelUV = new Vector2(textCoord.x * texture.width, textCoord.y * texture.height);
        Circle((int)pixelUV.x, (int)pixelUV.y, 10);
    }

    public float PercentageRevealed()
    {
        return pixelsRevealed / (float)textureSize;
    }

    private void RemoveAlpha(int pixelX, int pixelY)
    {
        Color currentColor = texture.GetPixel(pixelX, pixelY);
        if(currentColor.a < 1.0f)
        {
            currentColor.a = 1.0f;
            texture.SetPixel(pixelX, pixelY, currentColor);
            pixelsRevealed++;
        }
    }

    private void Circle(int cx, int cy, int r)
    {
        for (int x = 0; x <= r; x++)
        {
            int d = (int)Mathf.Ceil(Mathf.Sqrt(r * r - x * x));
            for (int y = 0; y <= d; y++)
            {
                int px = cx + x;
                int nx = cx - x;
                int py = cy + y;
                int ny = cy - y;

                RemoveAlpha(px, py);
                RemoveAlpha(nx, py);

                RemoveAlpha(px, ny);
                RemoveAlpha(nx, ny);
            }
        }
        texture.Apply();
    }
}
