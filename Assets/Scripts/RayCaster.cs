using UnityEngine;

public class RayCaster : MonoBehaviour {

    private AlphaRemover m_AlphaRemover = new AlphaRemover();
    private Camera mainCamera;
    private Transform m_CurrentModel;

	// Use this for initialization
	void Start () {
		mainCamera = GetComponent<Camera>();
    }
	
	// Update is called once per frame
	void Update () {
        Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if(hit.transform != m_CurrentModel)
            {
                //Model changed
                m_CurrentModel = hit.transform;

                Renderer rend = m_CurrentModel.GetComponent<Renderer>();

                if (rend == null || rend.sharedMaterial == null || rend.sharedMaterial.mainTexture == null)
                    return;

                m_AlphaRemover.SetTexture(rend.material.mainTexture as Texture2D);
            }
            m_AlphaRemover.Remove(hit.textureCoord);
        }
        //Debug.Log("Score: " + m_AlphaRemover.PercentageRevealed() * 100);
    }
}
