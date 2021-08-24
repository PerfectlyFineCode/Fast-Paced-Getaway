using System;
using UnityEngine;

public interface IPlayerBase
{
	public Vector2 MousePosition { get; set; }
	public abstract PlayerBasicInput PlayerInput { get; set; }
	public abstract void OnControllerEnabled(PlayerBasicInput input);
}

public static class PlayerBaseBuilder
{
	public static void ActivateInput<T>(this T instance) where T : IPlayerBase
	{
		var input = new PlayerBasicInput();
		input.Player.MousePosition.performed += ctx => instance.MousePosition = ctx.ReadValue<Vector2>();
		instance.PlayerInput = input;
		input.Enable();
		instance.OnControllerEnabled(input);
	}

	public static void SetMove<T>(this T instance, Action<Vector2> moveAction) where T : IPlayerBase
	{
		var move = instance.PlayerInput.Player.Move;
		move.started += ctx => moveAction(ctx.ReadValue<Vector2>());
		move.performed += ctx => moveAction(ctx.ReadValue<Vector2>());
		move.canceled += ctx => moveAction(ctx.ReadValue<Vector2>());
	}
}