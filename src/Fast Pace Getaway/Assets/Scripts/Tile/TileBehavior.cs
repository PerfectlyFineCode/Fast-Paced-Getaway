using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TileBehavior : MonoBehaviour
{
	public bool FallenState;
	private Rigidbody rb;
	private bool ShouldDisable;

	private void Awake()
	{
		TryGetComponent(out rb);
	}

	private void Update()
	{
		
	}

	public void StartFalling()
	{
		rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX |
		                 RigidbodyConstraints.FreezePositionZ;
		rb.useGravity = true;
		if (ShouldDisable) return;
		ShouldDisable = true;
		StartCoroutine(DoDisable(2f));
	}

	private IEnumerator DoDisable(float time)
	{
		yield return new WaitForSeconds(time);
		gameObject.SetActive(false);
		yield return null;
	}
}