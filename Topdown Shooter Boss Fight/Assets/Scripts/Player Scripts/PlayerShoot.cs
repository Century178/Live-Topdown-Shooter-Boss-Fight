using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Transform crosshair;
    [SerializeField] private Bullet bullet;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float offset = 90;
    [SerializeField] private float shootRate = 0.25f;
    private float timeSinceLastShot = 0;

    private void Awake()
    {
        timeSinceLastShot = shootRate;
        Cursor.visible = false;
    }

    private void Update()
    {
        Vector2 worldMousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 rawDirection = worldMousePos - (Vector2)transform.position;

        crosshair.position = worldMousePos;
        transform.localEulerAngles = new Vector3(0, 0, (Mathf.Atan2(rawDirection.y, rawDirection.x) * Mathf.Rad2Deg) - offset);

        timeSinceLastShot += Time.deltaTime;

        if (Input.GetMouseButton(0) && timeSinceLastShot >= shootRate)
        {
            timeSinceLastShot = 0;
            Instantiate(bullet, firePoint.position, transform.localRotation);
            Physics2D.IgnoreLayerCollision(3, 3);
        }
    }
}
