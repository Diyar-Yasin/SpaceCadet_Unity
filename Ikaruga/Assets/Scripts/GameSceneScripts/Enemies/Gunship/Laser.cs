using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://www.youtube.com/watch?v=vdci2oxVaoA&ab_channel=1MinuteUnity
public class Laser : MonoBehaviour
{
    private float defDistanceRay = 100;
    private int layerMask;
    public LineRenderer lineRenderer;
    public Transform firePoint;
    Transform mTransform;
    
    LineRenderer r;
    public bool laser1;
    public bool activeLasers;

    void Start()
    {
        layerMask = (LayerMask.GetMask("Player")); // How does this layermask work?
        activeLasers = false;
    }

    public void controlLasers(bool activate)
    {
        activeLasers = activate;

        if (!activeLasers)
        {
            r.enabled = false;
        }
        else 
        {
            r.enabled = true;
        }
        
    }

    void Update()
    {
        if (activeLasers)
        {
            Shoot();
        }
    }

    private void Draw2DRay(Vector2 startPos, Vector2 endPos)
    {
        lineRenderer.SetPosition(0, startPos);
        lineRenderer.SetPosition(1, endPos);
    }

    private void Awake()
    {
        mTransform = GetComponent<Transform>();
        r = GetComponent<LineRenderer>();
    }

    private void Shoot()
    {
        const float sevenPiOverFour = (7 * Mathf.PI) / 4;
        const float fivePiOverFour = (5 * Mathf.PI) / 4;

        if (activeLasers)
        {
            if (laser1)
            {
                Draw2DRay(firePoint.position, new Vector3( Mathf.Cos(sevenPiOverFour), Mathf.Sin(sevenPiOverFour),0f ) * defDistanceRay);
            }
            else
            {
                Draw2DRay(firePoint.position, new Vector3( Mathf.Cos(fivePiOverFour), Mathf.Sin(fivePiOverFour),0f ) * defDistanceRay);
            }
        }

        RaycastHit2D hitLaser1 = Physics2D.Linecast(firePoint.position, new Vector3( Mathf.Cos(sevenPiOverFour), Mathf.Sin(sevenPiOverFour),0f ) * defDistanceRay, layerMask);
        RaycastHit2D hitLaser2 = Physics2D.Linecast(firePoint.position, new Vector3( Mathf.Cos(fivePiOverFour), Mathf.Sin(fivePiOverFour),0f ) * defDistanceRay, layerMask);

        if (hitLaser1.collider != null && hitLaser1.collider.tag == "Player")
        {
            PlayerDamage dmgPlayer = hitLaser1.collider.GetComponent<PlayerDamage>();

            if (dmgPlayer)
            {
                dmgPlayer.RayCastCollision();
            }
        }
        if (hitLaser2.collider != null && hitLaser2.collider.tag == "Player")
        {
            PlayerDamage dmgPlayer = hitLaser2.collider.GetComponent<PlayerDamage>();

            if (dmgPlayer)
            {
                dmgPlayer.RayCastCollision();
            }
        }
    }
}