using UnityEngine;
[CreateAssetMenu(menuName = "ExtraWave/ExtraWave", fileName = "ExtraWave")]

public class ExtraWaveData : ScriptableObject
{

    [Header("SettingsForEachPoint")]
    [Header("Each number of array == " +
        "number of spawn point to drop enemies")]
    [Space]
    [SerializeField] private int[] _countOfNormalZombies = new int[3];
    public int[] CountOfNurmalZombies { get => _countOfNormalZombies; protected set { } }
    [SerializeField] private int[] _countOfClownsFor = new int[3];
    public int[] CountOfClowns { get => _countOfClownsFor; protected set { } }

}
