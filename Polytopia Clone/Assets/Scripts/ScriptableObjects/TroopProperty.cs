using UnityEngine;

[CreateAssetMenu]
public class TroopProperty : ScriptableObject
{
    [SerializeField] private int healh;
    [SerializeField] private int damage;
    [SerializeField] private int moveRange;
    [SerializeField] private int attackRange;

    public int Healh { get => healh; }
    public int Damage { get => damage; }
    public int MoveRange { get => moveRange; }
    public int AttackRange { get => attackRange; }
}
