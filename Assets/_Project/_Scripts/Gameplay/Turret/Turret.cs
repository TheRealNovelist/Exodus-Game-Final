using UnityEngine;
public class Turret : MonoBehaviour, IDamageable
{
    [SerializeField] private float range = 5f;
    [SerializeField] private float turningSpeed = 5f;

    [SerializeField] private Transform weaponPart;
    [SerializeField] private TurretProjectile projectile;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private LayerMask enemyMask;
    
    private Transform target;
    private float timer = 0;
    [SerializeField] private float duration = 3f;
    private bool waitingToShoot = false;
    
    private GameObject nearestEnemy = null;
    private GameObject currentEnemy = null;

    public float fullHealth = 100;
    private float currentHealth;

    private void Start()
    {
        InvokeRepeating(nameof(UpdateTarget),0f,0.5f);
        currentHealth = fullHealth;
    }
    

    // Update is called once per frame
    void Update()
    {
        //if there is no target
        if (target == null)
        {
            //turret rotates to orginal rotation
            if(weaponPart.rotation == Quaternion.identity) {return;}
            RotateYTo(weaponPart, Quaternion.identity, turningSpeed);
            return;
        } ;
        
        //rotate to target
        Vector3 dir = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        RotateYTo(weaponPart, lookRotation,turningSpeed);

        if (waitingToShoot)
        {

            if (timer < duration)
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
        TurretProjectile newOb = Instantiate(projectile, shootPoint.position,Quaternion.identity);
        newOb.SetDirect(direct);
        //Play sound
        waitingToShoot = true;
        
    }
    
    private void RotateYTo(Transform rotateObj,Quaternion angle, float speed)
    {
        Vector3 rotation = Quaternion.Lerp(rotateObj.rotation,angle,Time.deltaTime * speed).eulerAngles;
        rotateObj.rotation = Quaternion.Euler(0f,rotation.y,0f);
    }
    
    private void UpdateTarget()
    {
        Collider[] enemyInRange = Physics.OverlapSphere(transform.position, range,enemyMask);
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
        
        if (nearestEnemy != null && shortestDist <range)
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
            Destroy(gameObject);
        }
    }

    public void Damage(int amount)
    {
        throw new System.NotImplementedException();
    }
}
