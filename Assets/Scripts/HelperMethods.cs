using UnityEngine;

public class HelperMethods
{
    private static readonly int _gameAreaLayer = 9;
    
    public static bool IsObjectOutOfGameAre(Collider2D collider, GameObject gameObject)
    {
        return collider.gameObject.layer == _gameAreaLayer &&
               gameObject.activeSelf;
    }
    
    public static int CalculateDamage(out bool isCritical, int damage, int criticalChance, int criticalMultiplayer)
    {
        int newDamage = damage;

        if (Random.Range(0, 100) <= criticalChance)
        {
            newDamage += newDamage * criticalMultiplayer / 100;
            isCritical = true;
        }
        else
        {
            isCritical = false;
        }
        
        return newDamage;
    }
}
