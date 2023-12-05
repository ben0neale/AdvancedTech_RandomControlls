using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SetMaterial : MonoBehaviour
{
    BoxCollider2D collider;
    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();
        collider.size = new Vector2(spriteRenderer.size.x, spriteRenderer.size.y);
    }
}
