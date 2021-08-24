using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WorldGeneration : MonoBehaviour
{
	public const float TILE_SIZE = 15f;
	public Transform PlayerTransform;
	public GameObject TilePrefab;
	[HideInInspector] public readonly Dictionary<Vector3, Tile> cacheTiles = new Dictionary<Vector3, Tile>();


	private void Update()
	{
		SetViewport(PlayerTransform.position, TILE_SIZE * 4);
	}

	private void FixedUpdate()
	{
		GenerateAtDistance(PlayerTransform.position, 4);
	}

	private void GenerateAtDistance(Vector3 pos, int distance)
	{
		for (var i = 0; i < distance; i++)
		{
			var p = new Vector3(pos.x + TILE_SIZE * i, -2f, 0);
			GenerateAtPos(p);
		}
	}

	private bool Exists(int X, int Y, int Z)
	{
		return cacheTiles.ContainsKey(new Vector3(X, Y, Z));
	}

	private bool Exists(Vector3 pos)
	{
		return cacheTiles.ContainsKey(pos);
	}

	private void GenerateAtPos(Vector3 pos)
	{
		var p = pos;

		p.x -= p.x % TILE_SIZE;
		p.z -= p.z % TILE_SIZE;
		p.y = -2f;

		var X = Mathf.FloorToInt(p.x);
		var Y = Mathf.FloorToInt(p.y);
		var Z = Mathf.FloorToInt(p.z);

		var PlatformPos = new Vector3(X, Y, Z);

		if (Exists(PlatformPos)) return;

		var obj = Instantiate(TilePrefab, p, Quaternion.identity);
		var behavior = obj.AddComponent<TileBehavior>();
		var generation = obj.AddComponent<TileGeneration>();
		var tile = new Tile
		{
			X = X,
			Y = Y,
			Z = Z,
			Platform = obj.transform
		};
		cacheTiles.Add(PlatformPos, tile);
		generation.SetGeneration(Mathf.FloorToInt(X / TILE_SIZE), Mathf.FloorToInt(Y / TILE_SIZE));
	}

	private void SetViewport(Vector3 center, float distance)
	{
		var withinRange = cacheTiles
			.Where(x => Vector3.Distance(x.Key, center) <= distance);

		var keyValuePairs = withinRange as KeyValuePair<Vector3, Tile>[] ?? withinRange.ToArray();

		foreach (var tile in cacheTiles)
		{
			var exist = keyValuePairs.Any(x => x.Value == tile.Value);
			tile.Value.SetVisibility(exist);
		}
	}
}

public class Tile
{
	public Transform Platform;
	public int X { get; set; }
	public int Y { get; set; }

	public int Z { get; set; }

	public bool IsEnabled => Platform.gameObject.activeSelf;

	public void SetVisibility(bool visible)
	{
		Platform.gameObject.SetActive(visible);
	}
}