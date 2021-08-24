using UnityEngine;

public abstract class Entity : MonoBehaviour, IEntity
{
	[field: SerializeField]
	public bool Vulnerable { get; set; }

	public bool IsDead => Health <= 0;

	[field: SerializeField]
	public float Health { get; set; }

	public virtual void Damaged(float health, float damage)
	{
	}

	public virtual void Killed()
	{
	}
}