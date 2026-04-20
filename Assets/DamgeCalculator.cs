using UnityEngine;

public static class DamgeCalculator
{
    public const int SomeConst = 2;

    public static int CalculateDamage(AttackData attackData, DefenceData defenceData)
    {
        int damage = attackData.Damage - defenceData.Armor;
        if (defenceData.Resistnaces.TryGetValue(attackData.Type, out int resistance))
        {
            damage -= resistance;
        }

        return attackData.Damage - defenceData.Armor;
    }
}
