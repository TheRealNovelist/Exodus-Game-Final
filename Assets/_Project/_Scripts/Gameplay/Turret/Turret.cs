using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Turret : MonoBehaviour, IDamageable
{
    [SerializeField] private float range = 5f;
    [SerializeField] private float turningSpeed = 5f;
    [SerializeField] private float shootGap = 3f;
    [SerializeField] private float scanGap = 3f;
    [SerializeField] private int damage = 10;

    [SerializeField] private Transform weaponPart;
    [SerializeField] private TurretProjectile projectile;
    [SerializeField] private List<Transform> shootPoint = new List<Transform>();
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private ParticleSystem shootingParticles;

    private Transform target;
    private float timer = 0;
    private bool waitingToShoot = false;

    private GameObject nearestEnemy = null;
    private GameObject currentEnemy = null;

    public float fullHealth = 300;
    private float currentHealth;

    private void Start()
    {
        InvokeRepeating(nameof(UpdateTarget), 0f, scanGap);
        currentHealth = fullHealth;
    }


    // Update is called once per frame
    void Update()
    {
        //if there is no target
        if (target == null)
        {
            //turret rotates to orginal rotation
            if (weaponPart.rotation == Quaternion.identity)
            {
                return;
            }

            RotateYTo(weaponPart, Quaternion.identity, turningSpeed);
            return;
        }

        //rotate to target
        Vector3 dir = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        
        RotateYTo(weaponPart, target.transform, turningSpeed);

        if (waitingToShoot)
        {
            if (timer < shootGap)
            {
                timer += Time.deltaTime;
            }
            else
            {
                waitingToShoot = false;
                timer = 0;
                Shoot(dir);
            }
        }
    }

    void Shoot(Vector3 direct)
    {
        foreach (Transform point in shootPoint)
        {
            //Play sound
            audioManager.PlayOneShot("TurretShoot");
            shootingParticles.Play();
            TurretProjectile newOb = Instantiate(projectile, point.position, Quaternion.identity);
            newOb.GetComponent<Rigidbody>().AddForce(point.forward * 100, ForceMode.Impulse);
            newOb.Init(direct, damage, transform);
        }

        
        // Play shooting particles
        //shootingParticles.Play();

        waitingToShoot = true;
    }

    private void RotateYTo(Transform rotateObj, Quaternion angle, float speed)
    {
        Vector3 rotation = Quaternion.Lerp(rotateObj.rotation, angle, Time.deltaTime * speed).eulerAngles;
        rotateObj.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }
    
    private void RotateYTo(Transform rotateObj, Transform target, float speed)
    {
        rotateObj.transform.RotateTowards(target.transform, Time.deltaTime * speed, freezeX: true, freezeZ: true);
    }

    private void UpdateTarget()
    {
        Collider[] enemyInRange = Physics.OverlapSphere(transform.position, range, enemyMask);
        float shortestDist = Mathf.Infinity;

        foreach (var enemy in enemyInRange)
        {
            float distToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distToEnemy < shortestDist)
            {
                shortestDist = distToEnemy;
                currentEnemy = nearestEnemy;
                nearestEnemy = enemy.gameObject;
            }
        }

        if (nearestEnemy != null && shortestDist < range)
        {
            target = nearestEnemy.transform;
            if (nearestEnemy != currentEnemy)
            {
                waitingToShoot = true;
            }
        }
        else
        {
            target = null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    public void Damage(float amount, Transform source = null)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            if (_turretSlot) _turretSlot._holdingTurret = null;
            Destroy(gameObject);
        }
    }

    private TurretSlot _turretSlot;

    public void Init(TurretSlot slot)
    {
        _turretSlot = slot;
    }
}