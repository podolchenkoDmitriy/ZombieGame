using UnityEngine;
[CreateAssetMenu(menuName = "Weapon/Weapon", fileName = "Weapon")]

public class WeaponData : ScriptableObject
{
    [Header("BulletsPerClip")]
    [SerializeField] private int _bulletPerClip = 0;
    public int BulletsCount { get => _bulletPerClip; protected set { } }

    [Header("WeaponDamage")]
    [SerializeField] private int _damage = 0;
    public int WeaponDamage { get => _damage; protected set { } }

    [Header("FireDelay")]
    [SerializeField] private float _fireDelay = 0;
    public float FireDelay { get => _fireDelay; protected set { } }


    [Header("ReloadTime")]
    [SerializeField] private float _reloadTime = 0;
    public float ReloadTime { get => _reloadTime; protected set { } }

    [Header("BulletPrefab")]
    [SerializeField] private GameObject bulletPrefab = null;

    public GameObject BulletPrefab { get => bulletPrefab; protected set { } }

}
