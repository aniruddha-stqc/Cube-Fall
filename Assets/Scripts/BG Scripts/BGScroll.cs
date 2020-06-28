using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroll : MonoBehaviour
{
    public float scroll_Speed = 0.3f;

    private MeshRenderer mesh_Renderer;

    private void Awake()
    {
        mesh_Renderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Scroll();
    }

    //Scroll the background
    void Scroll()
    {
        Vector2 offset = mesh_Renderer.sharedMaterial.GetTextureOffset("_MainTex");

        //Keep scrolling the background

        offset.y += Time.deltaTime * scroll_Speed;

        mesh_Renderer.sharedMaterial.SetTextureOffset("_MainTex", offset); 

    }
}
