using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    // Start is called before the first frame update
    public ZombieData zombieData;
    private Animator anim;
    private float _health;
    private float _movespeed;
    private float _attackRange;
    private NavMeshAgent zombie;
    private PlayerController player;
    private BoxCollider _head;
    private CapsuleCollider _body;
    private ZombieData.TypeOfZombie typeOfZombie;
    public float _timeForBlow = 3f;
    public GameObject _boss2;
    public GameObject _boss3;
    private void Awake()
    {
        _head = GetComponent<BoxCollider>();
        _body = GetComponent<CapsuleCollider>();
        anim = GetComponent<Animator>();
        zombie = GetComponent<NavMeshAgent>();
        _health = zombieData.HitPoints;
        _movespeed = zombieData.MoveSpeed;
        _attackRange = zombieData.AttackRange * 5f;
        player = FindObjectOfType<PlayerController>();
        zombie.speed = _movespeed;
        zombie.stoppingDistance = _attackRange;
        typeOfZombie = zombieData.Zombie;
        anim.speed = _movespeed/2;

        if (typeOfZombie == ZombieData.TypeOfZombie.Normal)
        {
            if (PlayerPrefs.HasKey("Level"))
            {
                _health += 50 * PlayerPrefs.GetInt("Level");
            }
        }

        if (typeOfZombie == ZombieData.TypeOfZombie.Boss1)
        {
            transform.localScale = zombieData.BossScale;
        }
        else if (typeOfZombie == ZombieData.TypeOfZombie.Boss2)
        {
            transform.localScale = zombieData.BossScale;

        }
        else if (typeOfZombie == ZombieData.TypeOfZombie.Boss3)
        {
            transform.localScale = zombieData.BossScale;

        }
    }
    CameraFollow shake;
    private void Start()
    {
        AudioManager.instance.PlayZombieSound();

        StartCoroutine(MoveDestination());
        if (typeOfZombie == ZombieData.TypeOfZombie.Clown)
        {
            shake = Camera.main.GetComponent<CameraFollow>();
        }
    }

    // Update is called once per frame
    public static bool eating = false;
    bool eat = false;
    private IEnumerator MoveDestination()
    {
        anim.SetBool("Walk", true);
        while (gameObject)
        {
            if (gameObject != null)
            {
                zombie.SetDestination(player.transform.position);
                yield return new WaitForSeconds(0.2f);
                if (Vector3.Distance(transform.position, zombie.destination) < 3f)
                {
                    if (!eating && !eat)
                    {
                        eat = true;
                        eating = true;
                        anim.SetBool("Atack", true);
                        anim.SetBool("Walk", false);
                        AudioManager.instance.PlayEatSound();
                    }
                   

                }
                else
                {
                    anim.SetBool("Walk", true);
                    anim.SetBool("Atack", false);
                    eat = false;
                    
                }
            }

        }
    }

    private IEnumerator BlowClown()
    {
        zombie.speed *= 2f;
        _attackRange *= 2f;
        GameObject part = ParticleHolder.instance._fireEffect.gameObject;
        Instantiate(part,transform);
        yield return new WaitForSeconds(_timeForBlow);

        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, _attackRange);
        shake.Shake(0.5f, 1f);
        AudioManager.instance.PlayExplousionSound();

        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (hit.GetComponent<ZombieController>())
            {
                hit.GetComponent<ZombieController>().SetHealth(125f);
            }
            else if (hit.GetComponent<PlayerController>())
            {
                eating = true;
            }
            if (rb != null)
                rb.AddExplosionForce(10000f, explosionPos, _attackRange, 3.0F);
        }
        part = ParticleHolder.instance._exploud.gameObject;
        Instantiate(part, transform.position, Quaternion.identity);
        Destroying();
    }
    private void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, _attackRange);
    }

    private void Destroying()
    {
        GameObject part = ParticleHolder.instance._zombieDeath.gameObject;
        part =  Instantiate(part, transform.position, Quaternion.identity);
        part.transform.localScale = transform.localScale;
        SpawnManager.normalZombies.Remove(gameObject);
        Destroy(gameObject);

    }

    public void SetHealth(float getDamage)
    {
        _health -= getDamage;
        if (_health <= 0)
        {
            _body.enabled = false;
            _head.enabled = false;
            if (typeOfZombie == ZombieData.TypeOfZombie.Normal)
            {

                Destroying();

            }
            else if (typeOfZombie == ZombieData.TypeOfZombie.Clown)
            {

                StartCoroutine(BlowClown());
            }
            else if (typeOfZombie == ZombieData.TypeOfZombie.Boss1)
            {

                StartCoroutine(SpawnBoss(2, _boss2));
            }
            else if (typeOfZombie == ZombieData.TypeOfZombie.Boss2)
            {

                StartCoroutine(SpawnBoss(3, _boss3));

            }
            else if (typeOfZombie == ZombieData.TypeOfZombie.Boss3)
            {
                Destroying();
            }
        }
        getDamage = 0;

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<Bullet>())
        {
            float damage = 0;
            bool headShot = false;

            foreach (ContactPoint contact in collision.contacts)
            {
                if (contact.thisCollider == _body)
                {
                    damage = collision.collider.GetComponent<Bullet>()._damage;

                }
                if (contact.thisCollider == _head)
                {
                    if (!headShot)
                    {
                        AudioManager.instance.PlayHeadshotSound();

                        headShot = true;
                        damage = collision.collider.GetComponent<Bullet>()._damage * 2;
                        GameObject part = ParticleHolder.instance._hitHead.gameObject;
                        Instantiate(part, contact.point, Quaternion.identity, transform);
                    }
                    
                }
            }

            AudioManager.instance.PlayHitSound();

            SetHealth(damage);

        }
    }
    IEnumerator SpawnBoss(int n, GameObject obj)
    {
        for (int i = 0; i < n; i++)
        {
            SpawnManager.normalZombies.Add(Instantiate(obj, transform.position, Quaternion.identity));
        }
        yield return new WaitForFixedUpdate();
        Destroying();
    }
}
