public interface IKillable
{
	public bool Vulnerable { get; set; }

	public bool IsDead { get; }

	public float Health { get; set; }

	void Damaged(float health, float damage);
	void Killed();
}