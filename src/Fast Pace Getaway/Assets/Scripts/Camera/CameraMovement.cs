using UnityEngine;

public class CameraMovement : MonoBehaviour
{
	public Transform Target;
	public float MoveDamping = 5;
	public float RotationDamping = 5f;
	public float Rotation;

	private void Update()
	{
		transform.rotation = Quaternion.Slerp(transform.rotation,
			Quaternion.AngleAxis(Rotation, Vector3.up),
			RotationDamping * Time.deltaTime);

		if (!Target) return;
		transform.position = Vector3.Lerp(transform.position, Target.position, MoveDamping * Time.fixedDeltaTime);
	}
}