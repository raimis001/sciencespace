using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildCursor : MonoBehaviour
{
	public Material allowMaterial;
	public Material denyMaterial;

	public Renderer materialRenderer;

	public void SetStatus(bool allow)
	{
		materialRenderer.material = allow ? allowMaterial : denyMaterial;
	}

}
