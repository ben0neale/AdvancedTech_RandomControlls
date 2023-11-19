using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMaterial : MonoBehaviour
{
    Material Mat;

    // Start is called before the first frame update
    void Start()
    {
        Mat = this.GetComponentInChildren<MeshRenderer>().material;
        Mat.mainTextureScale = new Vector2(transform.localScale.x, transform.localScale.y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
