using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Applies rotation to Sprite.
/// </summary>
public class RotateSprite : MonoBehaviour
{
    private float rotationSpeed = 180;

    public void Rotate(float scale)
    {
        transform.Rotate(0, 0, -rotationSpeed * scale );
    }
}
