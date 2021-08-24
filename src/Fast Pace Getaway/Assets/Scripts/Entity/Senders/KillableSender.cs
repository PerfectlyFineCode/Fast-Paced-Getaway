public static class KillableSender
{
	public static void Kill<T>(this T instance, bool ignoreProtection = false) where T : IKillable
	{
		if (instance.IsDead || !ignoreProtection && !instance.Vulnerable) return;
		instance.Health = 0;
		instance.Killed();
	}

	public static void Heal<T>(this T instance) where T : IKillable
	{
		
	}

	public static void Damage<T>(this T instance, float damage) where T : IKillable
	{
		if (!instance.Vulnerable) return;
		var health = instance.Health -= damage;
		
		if (instance.IsDead) instance.Killed();
		else instance.Damaged(health, damage);
	}
}