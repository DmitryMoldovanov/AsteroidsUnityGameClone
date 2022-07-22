using System;

public class HealthPoints
{
    public event Action OnDied;
    public event Action<int> OnChanged;

    private int _healthPoints;

    public HealthPoints(int startHealthPoints)
    {
        _healthPoints = startHealthPoints;
    }

    public void TakeDamage(int damage)
    {
        _healthPoints -= damage;
        OnChanged?.Invoke(_healthPoints);

        if (IsDied())
        {
            OnDied?.Invoke();
        }
    }

    public void ResetHealthPoints(int healthToReset)
    {
        _healthPoints = healthToReset;
    }

    private bool IsDied()
    {
        return _healthPoints <= 0;
    }
}
