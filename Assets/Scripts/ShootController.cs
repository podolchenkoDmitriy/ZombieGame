using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject _bulletPrefab;
    int _bulletPerClip;
    int _damage;
    float _fireDelay;
    float _reloadTime;
    public float _aimDistance = 500f;
    public Transform _cross;
    Ray _crossRay;
    public LayerMask _layerMask;
    bool shooted = false;
    public WeaponData weaponData;
    int _numberOfShootInMagazin;
    private bool shoot = false;
    bool reload = false;
    public Material lasermat;
    LineRenderer line;
    GameObject part;
    Animator anim;
    void Start()
    {
        anim = GetComponentInParent<Animator>();
        part = ParticleHolder.instance._bulletFire.gameObject;
        part = Instantiate(part,_cross.transform.position, _cross.rotation, _cross);
        part.SetActive(false);
        _bulletPrefab = weaponData.BulletPrefab;
        _bulletPerClip = weaponData.BulletsCount;
        _damage = weaponData.WeaponDamage;
        _fireDelay = weaponData.FireDelay;
        _reloadTime = weaponData.ReloadTime;
        if (!GetComponent<LineRenderer>())
        {
            line = _cross.gameObject.AddComponent<LineRenderer>();
            line.material = lasermat;
            line.startWidth = 0.05f;
            line.endWidth = 0.05f;
        }
        StartCoroutine(AIM());

    }
    Ray Hit()
    {
        Ray hit = new Ray(_cross.position, _cross.forward * _aimDistance);
        Debug.DrawRay(hit.origin, hit.direction * _aimDistance, Color.green);
        _crossRay = hit;

        return _crossRay;
    }
    IEnumerator AIM()
    {
        while (true)
        {
            Hit();
            line.SetPosition(0, _cross.position);
            line.SetPosition(1, _crossRay.GetPoint(_aimDistance));

            Physics.Raycast(_crossRay, out RaycastHit hit);
            if (hit.collider!=null)
            {
                line.SetPosition(1, hit.point);

            }
            if (CameraSwitch.topView)
            {
                if (Physics.Raycast(_crossRay.origin, _crossRay.direction, _aimDistance, _layerMask))
                {
                    if (hit.collider.GetComponent<ZombieController>())
                    {
                        if (!shooted)
                        {
                            shooted = true;
                            Shoot();
                        }
                    }
                    else
                    {
                        yield return null;
                    }
                }
            }
            else
            {
                if(PointerDown.buttonPressed)
                {
                    if (!shooted)
                    {
                        shooted = true;
                        Shoot();
                    }
                }
            }

           
            yield return new WaitForFixedUpdate();

        }


    }
    private IEnumerator StartShoot()
    {
        if (!shoot && !reload)
        {
            yield return new WaitForFixedUpdate();

            part.SetActive(true);

            GameObject obj;
            obj = Instantiate(_bulletPrefab, _cross);
            obj.transform.parent = null;
            AudioManager.instance.PlayShootSound();

            shoot = true;
            yield return new WaitForSeconds(0.1f);
            part.SetActive(false);

            yield return new WaitForSeconds(_fireDelay - 0.1f);
            shoot = false;
            if (_numberOfShootInMagazin >= _bulletPerClip)
            {
                if (!reload)
                {
                    reload = true;
                    StartCoroutine(InitBullets());
                }
            }
            shooted = false;

        }
    }
    private IEnumerator InitBullets()
    {
        AudioManager.instance.PlayReloadSound();
        anim.SetBool("Reload_b", true);
        yield return new WaitForSeconds(_reloadTime);
        anim.SetBool("Reload_b", false);

        _numberOfShootInMagazin = 0;
        if (reload)
        {
            reload = false;
        }
        shooted = false;

    }
    public void Shoot()
    {
        if (_numberOfShootInMagazin < _bulletPerClip)
        {
            StartCoroutine(StartShoot());
            _numberOfShootInMagazin++;
        }
        

    }
}
