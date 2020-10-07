using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody rb;
    public float _force = 10000f;
    public float _damage;
    private void Awake()
    {
        _damage = GetComponentInParent<ShootController>().weaponData.WeaponDamage;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Vector3 randomVector = new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), 0);
        rb.AddForce((transform.forward + randomVector)* _force, ForceMode.Force);
    }

    private void Destroying()
    {
        GameObject part = ParticleHolder.instance._hitBody.gameObject;
        Instantiate(part, transform.position, Quaternion.identity);
        Destroy(gameObject);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<ZombieController>())
        {
            Destroying();
        }
        else
        {
            Destroy(gameObject);
        }

    }
}
