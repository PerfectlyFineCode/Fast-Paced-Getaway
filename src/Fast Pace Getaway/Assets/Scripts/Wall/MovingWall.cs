using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MovingWall : MonoBehaviour
{
	[HideInInspector] public WorldGeneration generation;
	public WallDirection Direction;
	public float speed;

	private IEnumerable<KeyValuePair<Vector3, Tile>> tile;

	private void Awake()
	{
		generation = FindObjectOfType<WorldGeneration>();
	}

	// Start is called before the first frame update
	private void Start()
	{
	}

	// Update is called once per frame
	private void Update()
	{
		var dir = GetDirection(Direction);

		transform.position += dir * (speed * Time.deltaTime);

		var v = transform.position - Vector3.zero;

		//TODO Fix up this - so if wall goes further than any tile's position - make it fall.
	}

	private void OnDrawGizmos()
	{
		if (tile == null) return;
		foreach (var t in tile.ToArray()) Gizmos.DrawLine(t.Key, t.Key + Vector3.up * 5);
	}

	private Vector3 GetDirection(WallDirection direction)
	{
		var dir = Vector3.zero;

		if (direction.HasFlag(WallDirection.X))
			dir += Vector3.right;

		if (direction.HasFlag(WallDirection.Y))
			dir += Vector3.up;

		if (direction.HasFlag(WallDirection.Z))
			dir += Vector3.forward;

		if (direction.HasFlag(WallDirection.Backward))
			dir *= -1;

		return dir;
	}
}

[Flags]
public enum WallDirection
{
	Forward = 1 << 0,
	Backward = 1 << 1,

	X = 1 << 2,
	Y = 1 << 3,
	Z = 1 << 4
}