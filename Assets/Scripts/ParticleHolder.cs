using UnityEngine;

public class ParticleHolder : MonoBehaviour
{
    // Start is called before the first frame update
    public static ParticleHolder instance;
    public ParticleSystem _hitBody;
    public ParticleSystem _hitHead;
    public ParticleSystem _zombieDeath;

    public ParticleSystem _fireEffect;
    public ParticleSystem _exploud;
    public ParticleSystem _bulletFire;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

        }
        else
        {
            Destroy(this);
        }

    }
}
