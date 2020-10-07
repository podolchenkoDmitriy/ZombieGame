using UnityEngine;
[CreateAssetMenu(menuName = "Zombie/Zombie", fileName = "Zombie")]

public class ZombieData : ScriptableObject
{
    [Header("HitPoints")]
    [SerializeField] private float[] _hitPoints = null;
    public float HitPoints { get => _hitPoints[Random.Range(0, (_hitPoints.Length - 1))]; protected set { } }

    [Header("Attack range")]
    [SerializeField] private float _range = 0;
    public float AttackRange { get => _range; protected set { } }

    [Header("Move Speed")]
    [SerializeField] private float _moveSpeed = 0;
    public float MoveSpeed { get => _moveSpeed; protected set { } }


    [Header("ScaleBoss")]
    [SerializeField] private Vector3 _scale = Vector3.zero;
    public Vector3 BossScale { get => _scale; protected set { } }


    public enum TypeOfZombie
    {
        Normal,
        Clown,
        Boss1,
        Boss2,
        Boss3,
    }
    [SerializeField] private TypeOfZombie zombie = TypeOfZombie.Normal;

    public TypeOfZombie Zombie { get => zombie; protected set { } }

}
