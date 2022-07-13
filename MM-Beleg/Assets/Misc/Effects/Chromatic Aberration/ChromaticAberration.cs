using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ChromaticAberration : MonoBehaviour
{

	public Shader shader;
	private Material material;
	public bool onTheScreenEdges = true;
	public float multiplier = 1;
	// Lautstärke von AudioAnalyzer
	private float CA = 0;

	public void Start()
	{
		material = new Material(shader);
	}

	void Update()
	{
		CA = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioAnalyzer>().GetLoudness() * 0.64f;
	}

	public void OnRenderImage(RenderTexture inTexture, RenderTexture outTexture)
	{
		// TODO Toggle
		if (shader != null)
		{
			material.SetFloat("_ChromaticAberration", 0.015f * CA * multiplier);

			if (onTheScreenEdges)
				material.SetFloat("_Center", 0.5f);

			else
				material.SetFloat("_Center", 0);

			Graphics.Blit(inTexture, outTexture, material);
		}
		else
		{
			Graphics.Blit(inTexture, outTexture);
		}
	}
}