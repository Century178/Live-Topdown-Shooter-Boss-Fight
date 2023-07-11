using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour //Shout out to Smartoom, he made this script.
{
    [Header("Speed")]
    [SerializeField] private float speed;
    [SerializeField] private float chargeSpeed;

    [Header("Attack Amounts")]
    [SerializeField] private float projectilesToShoot;
    private float projectilesShot;
    [SerializeField] private float bombsToShoot;
    private float bombsShot;
    [SerializeField] private float timesToCharge;
    private float timesCharged;

    [Header("Attack Frequencies")]
    [SerializeField] private float safeShootingRange;
    [SerializeField] private float projectileShootingInterval;
    [SerializeField] private float bombShootingInterval;
    [SerializeField] private float ChargingInterval;
    private float timer;

    [Header("Repositioning")]
    [SerializeField] private Vector2 repositioningBox;
    [SerializeField] private LayerMask wallLayer;
    private Vector3 targetPosition;

    [Header("Charging")]
    public bool chargeActive;
    private Color normalColor;
    [SerializeField] private Color ChargingColor;
    [SerializeField] private float colorLerp;

    [Header("Components")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private GameObject bombPrefab;
    [SerializeField] private Transform warningTriangle;
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private Transform player;

    private enum BossState
    {
        Shooting,
        ShootingBombs,
        Repositioning,
        Charging
    }
    private BossState bossState;

    private void Awake()
    {
        player = PlayerMovement.Player.transform;
        normalColor = spriteRenderer.color;
    }

    void Start()
    {
        RandomizeTargetPos();
        bossState = BossState.Repositioning;
    }

    private void FixedUpdate()
    {
        switch (bossState)
        {
            case BossState.Shooting:
                if (safeShootingRange > Vector2.Distance(transform.position, player.position) || projectilesShot >= projectilesToShoot)
                {
                    RandomizeTargetPos();
                    projectilesShot = 0;
                    bossState = BossState.Repositioning;
                }
                FocusOn(player.position);
                if (timer >= projectileShootingInterval)
                {
                    projectilesShot += 1;
                    Instantiate(projectilePrefab, shootingPoint.position, transform.localRotation);
                    timer = 0;
                }
                else timer += 1;
                break;

            case BossState.ShootingBombs:
                if (safeShootingRange > Vector2.Distance(transform.position, player.position) || bombsShot >= bombsToShoot)
                {
                    RandomizeTargetPos();
                    bombsShot = 0;
                    bossState = BossState.Repositioning;
                }
                FocusOn(player.position);
                if (timer >= bombShootingInterval)
                {
                    bombsShot += 1;
                    Instantiate(bombPrefab, shootingPoint.position, transform.localRotation);
                    timer = 0;
                }
                else timer += 1;
                break;

            case BossState.Repositioning:
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed);
                FocusOn(targetPosition);
                if (transform.position == targetPosition)
                {
                    RandomState();
                }
                break;

            case BossState.Charging:
                spriteRenderer.color = Color.Lerp(spriteRenderer.color, ChargingColor, colorLerp);
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, chargeSpeed);
                if (transform.position == targetPosition)
                {
                    timesCharged += 1;

                    RandomizeTargetPos();
                    warningTriangle.position = targetPosition;
                    FocusOn(targetPosition);
                }
                if (timesCharged >= timesToCharge)
                {
                    RandomState();
                    warningTriangle.position = Vector2.one * 69; //send it away
                    spriteRenderer.color = normalColor;
                    timesCharged = 0;
                    tag = "Untagged";
                }
                break;

            default: break;
        }
    }

    private void RandomState()
    {
        int randInt = Random.Range(0, 4);
        switch (randInt)
        {
            case 0:
                bossState = BossState.Shooting;
                break;

            case 1:
                bossState = BossState.ShootingBombs;
                break;

            case 2:
                if (chargeActive)
                {
                    tag = "Enemy";
                    RandomizeTargetPos();
                    warningTriangle.position = targetPosition;
                    bossState = BossState.Charging;
                    FocusOn(targetPosition);
                }
                break;

            default: break;
        }
    }
    private void OnDestroy()
    {
        if (warningTriangle != null) warningTriangle.position = Vector2.one * 69; //send it away
    }

    private void RandomizeTargetPos()
    {
        targetPosition = new Vector3(Random.Range(-repositioningBox.x, repositioningBox.x), Random.Range(-repositioningBox.y, repositioningBox.y));
    }

    private void FocusOn(Vector3 target)
    {
        Vector2 rawDirection = target - transform.position;
        transform.localEulerAngles = new Vector3(0, 0, (Mathf.Atan2(rawDirection.y, rawDirection.x) * Mathf.Rad2Deg) - 90);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(Vector2.zero, repositioningBox * 2);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(targetPosition, 1);
    }
}
