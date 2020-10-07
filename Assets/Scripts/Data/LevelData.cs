using UnityEngine;
[CreateAssetMenu(menuName = "Level/Level", fileName = "Level")]

public class LevelData : ScriptableObject
{
    [Header("NumberOfStartLevel")]
    [SerializeField] private int _mainLevel = 0;
    public int NumberOfMainLevel { get => _mainLevel; protected set { } }

    [Header("CountOfNormalZombies")]
    [SerializeField] private int _countOfNormalZombies = 0;
    public int CountOfNormalZombies { get => _countOfNormalZombies; protected set { } }

    [Header("TimeBetweenSpawn")]
    [SerializeField] private float _timeBetweenSpawn = 0;
    public float TimeBetweenSpawn { get => _timeBetweenSpawn; protected set { } }

    [Header("BoundsForNormalZombiesCount")]
    [SerializeField] private int _boundsOfZombies = 0;
    public int BoundsForNormalZombiesCount { get => _boundsOfZombies; protected set { } }
    #region prefabs
    [Header("NormalZombiesPrefab")]
    [SerializeField] private GameObject[] _normalZombiePrefab = null;

    public GameObject[] NormalZombiesPrefab { get => _normalZombiePrefab; protected set { } }

    [Header("ClownZombiePrefab")]
    [SerializeField] private GameObject _clownZombiePrefab = null;

    public GameObject ClownZombiePrefab { get => _clownZombiePrefab; protected set { } }

    [Header("BossPrefab")]
    [SerializeField] private GameObject _bossPrefab = null;

    public GameObject BossPrefab { get => _bossPrefab; protected set { } }
    #endregion

}
