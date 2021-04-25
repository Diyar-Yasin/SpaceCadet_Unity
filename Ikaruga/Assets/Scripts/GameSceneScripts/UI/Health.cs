using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    // PUBLIC
        // VARIABLES
        public static Health current;
        public Sprite greenHP;
        public Sprite yellowHP;
        public Sprite orangeHP;
        public Sprite redHP;

    // PRIVATE
        // VARIABLES
        private SpriteRenderer spriteR;

    // Start is called before the first frame update
    void Start()
    {
        spriteR = gameObject.GetComponent<SpriteRenderer>();
        spriteR.sprite = greenHP;
    }

    public void UpdateHealth(int lives)
    {
        switch (lives)
        {
            case 3:
                spriteR.sprite = yellowHP;
                break;
            case 2:
                spriteR.sprite = orangeHP;
                break;
            case 1:
                spriteR.sprite = redHP;
                break;
            default:
                break;
        }
    }
}
