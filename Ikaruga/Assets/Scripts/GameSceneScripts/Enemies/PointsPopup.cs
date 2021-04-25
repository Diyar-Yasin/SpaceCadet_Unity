using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// PointsPopup
//              Controls the points earned text above recently destroyed enemies.
// CREDITS: https://www.youtube.com/watch?v=iD1_JczQcFY&ab_channel=CodeMonkey
public class PointsPopup : MonoBehaviour
{
    // PRIVATE
        // CONSTANTS
        private const float DISAPPEAR_TIMER_MAX = 1f;

        // VARIABLES
        private TextMesh textMesh;
        private float disappearTimer;
        private Color textColor;
        private Vector3 moveVector;

    private void Awake()
    {
        textMesh = transform.GetComponent<TextMesh>();
    }

    public void Setup(int pointsAmount)
    {
        textMesh.text = pointsAmount.ToString();
        disappearTimer = DISAPPEAR_TIMER_MAX;
        moveVector = new Vector3(1, 1) * 20f;
    }

    public static PointsPopup Create(Vector3 position, int pointsAmount)
    {
        Transform pointsPopupTransform = Instantiate(GameAssets.i.pointsPopup, position, Quaternion.identity);
        PointsPopup pointsPopup = pointsPopupTransform.GetComponent<PointsPopup>();
        pointsPopup.Setup(pointsAmount);

        return pointsPopup;
    }

    private void Update()
    {
        transform.position += moveVector * Time.deltaTime;
        moveVector -= moveVector * 6f * Time.deltaTime;

        if (disappearTimer > DISAPPEAR_TIMER_MAX * 0.5f)
        {
            // First half of popup lifetime
            float increaseScaleAmount = 1f;
            transform.localScale += Vector3.one * increaseScaleAmount * Time.deltaTime;
        }
        else
        {
            // Second half of popup lifetime
            float decreaseScaleAmount = 1f;
            transform.localScale -= Vector3.one * decreaseScaleAmount * Time.deltaTime;
        }

        disappearTimer -= Time.deltaTime;
        
        if (disappearTimer <= 0)
        {
            // Start disappearing
            float disappearSpeed = 3f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if (textColor.a <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
