using System.Collections;
using UnityEngine;

public class ParticleCollector : MonoBehaviour
{
    public float _lifeTime;
    private void Start()
    {
        StartCoroutine(Destroyparticle(_lifeTime));
    }

    private IEnumerator Destroyparticle(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
