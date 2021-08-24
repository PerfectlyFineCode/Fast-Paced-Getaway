using UnityEngine;

public class TileGeneration : MonoBehaviour
{
	private int X { get; set; }

	private int Y { get; set; }

	private Bounds OriginalBounds { get; set; }
	private Bounds Bounds { get; set; }

	private void Awake()
	{
		var status = TryGetComponent(out MeshRenderer rend);
		var combinedBounds = status ? rend.bounds : new Bounds(transform.position, Vector3.zero);

		var renderers = GetComponentsInChildren<MeshRenderer>();
		foreach (var render in renderers) combinedBounds.Encapsulate(render.bounds);

		combinedBounds.Encapsulate(combinedBounds.center + Vector3.up * WorldGeneration.TILE_SIZE);
		Bounds = combinedBounds;
	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawWireCube(Bounds.center, Bounds.size);
	}

	public void SetGeneration(int x, int y)
	{
		X = x;
		Y = y;

		UpdateTile();
	}

	private void UpdateTile()
	{
		if (X % 3 == 0) Debug.Log("yep");
	}
}