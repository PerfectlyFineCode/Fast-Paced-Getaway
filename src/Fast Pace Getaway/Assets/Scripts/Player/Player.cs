using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : Entity, IPlayerBase
{
	private static readonly int SpeedF = Animator.StringToHash("Speed_f");
	[SerializeField] private float MoveSpeed = 5f;

	[Header("Ground Checking")] [SerializeField]
	private Transform GroundCheckTransform;

	[SerializeField] private float GroundCheckRadius;
	[SerializeField] private LayerMask GroundMask;
	private Animator _animator;
	private Camera cam;
	private Transform cameraTransform;

	private CharacterController controller;
	private Plane MousePlane;

	private bool IsGrounded => Physics.CheckSphere(GroundCheckTransform.position, GroundCheckRadius, GroundMask);

	private Vector3 MoveDelta { get; set; }


	private Vector3 NextPosition => MoveDelta * MoveSpeed;

	private void Awake()
	{
		cam = Camera.main;
		if (Camera.main is { }) cameraTransform = Camera.main.transform;
		MousePlane = new Plane(Vector3.up, Vector3.zero);
		TryGetComponent(out _animator);
		TryGetComponent(out controller);
		this.ActivateInput();
	}

	private void Update()
	{
		this.Damage(10f);
	}

	private void FixedUpdate()
	{
		// controller.SimpleMove(RotateWithView(NextPosition));

		// var ray = cam.ScreenPointToRay(MousePosition);
		// if (!MousePlane.Raycast(ray, out var range)) return;
		//
		// var hit = ray.GetPoint(range);
		// var diff = hit - transform.position;
		// var angle = new Vector3(diff.x, 0, diff.z).normalized;

		var delta = MoveDelta;

		if (delta == Vector3.zero) return;
		var rot = Quaternion.LookRotation(delta);
		transform.rotation = Quaternion.Slerp(transform.rotation, rot, 5 * Time.deltaTime);
	}

	private void OnAnimatorMove()
	{
		var v = _animator.deltaPosition * MoveSpeed / Time.deltaTime;
		v.y = controller.velocity.y;
		controller.SimpleMove(v);
	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere(GroundCheckTransform.position, GroundCheckRadius);
	}

	public Vector2 MousePosition { get; set; }
	public PlayerBasicInput PlayerInput { get; set; }

	public void OnControllerEnabled(PlayerBasicInput input)
	{
		this.SetMove(Move);
		Debug.Log("test");
	}

	private Vector3 RotateWithView(Vector3 moveDir)
	{
		var dir = cameraTransform.TransformDirection(moveDir);
		dir.Set(dir.x, 0, dir.z);
		return dir.normalized * moveDir.magnitude;
	}

	private void Move(Vector2 delta)
	{
		MoveDelta = new Vector3(delta.x, 0, delta.y);
		_animator.SetFloat(SpeedF, delta.magnitude);
	}
}