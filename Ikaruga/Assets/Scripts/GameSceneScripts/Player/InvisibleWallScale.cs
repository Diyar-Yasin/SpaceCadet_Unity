using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// CREDITS: https://pastebin.com/HcxgsxXZ
public class InvisibleWallScale : MonoBehaviour
{
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;

    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, transform.position.z));
        objectWidth = transform.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.size.y / 2;
    }

    void LateUpdate()
    {
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, -1 * screenBounds.x, screenBounds.x);
        viewPos.y = Mathf.Clamp(viewPos.y, -1 * screenBounds.y, screenBounds.y);
        transform.position = viewPos;
    }
}
