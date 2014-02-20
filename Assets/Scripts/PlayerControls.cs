using UnityEngine;
using System.Collections;

public enum PlayerAnimation {
	IdleUp,
	IdleDown,
	IdleRight,
	IdleLeft,
	MoveUp,
	MoveDown,
	MoveRight,
	MoveLeft,
	Spin
}

public class PlayerControls : MonoBehaviour {
	public float MovementSpeed = 8.0f;
	public GameObject BulletPrefab;
	public float FireSpeed = 10.0f;
	public float FireCooldown = 0.2f;

	private Animator _animator;
	private bool _isMoving;
	private PlayerAnimation _lastAnimation;
	private Vector2 _move;
	private float _timeToFire = 0.0f;
	private bool _firing = false;

	void Awake()
	{
		_animator = GetComponent<Animator>();
		_lastAnimation = PlayerAnimation.IdleDown;
		_move = new Vector2();
	}

	void Start () {
	
	}

	void FixedUpdate() {
		rigidbody2D.velocity = _move;
		if(_firing && _timeToFire <= 0) {
			_timeToFire = FireCooldown;
			Fire();
		}
	}


	void Fire() {

		Plane plane = new Plane(Vector3.forward, Vector3.zero);
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		float distance;

		if(plane.Raycast(ray, out distance))
		{
			Vector3 targetPosition = ray.origin + ray.direction * distance;
			Vector3 relative = targetPosition - transform.position;

			GameObject bullet = (GameObject)Instantiate(Resources.Load("BulletOfDeathPrefab"), 
			                                transform.position + relative.normalized * 0.5f, 
			                                transform.rotation);

			bullet.rigidbody2D.velocity = relative.normalized * FireSpeed;
		}
	}

	void Update () {
		PlayerAnimation nextAnimation = _lastAnimation;

		if(_timeToFire > 0) {
			_timeToFire -= Time.deltaTime;
		}

		if(Input.GetButtonUp("Fire1")) {
			_firing = false;
		}
		if(Input.GetButtonDown("Fire1")) {
			_firing = true;
		}

		if(Input.GetKeyUp(KeyCode.W)) {
			_move.y = 0;
			nextAnimation = PlayerAnimation.IdleUp;
		}
		
		if (Input.GetKeyUp(KeyCode.S)) {
			_move.y = 0;
			nextAnimation = PlayerAnimation.IdleDown;
		}

		if (Input.GetKeyUp(KeyCode.A)) {	
			_move.x = 0;
			nextAnimation = PlayerAnimation.IdleLeft;
		}
		
		if (Input.GetKeyUp(KeyCode.D)) {
			_move.x = 0;
			nextAnimation = PlayerAnimation.IdleRight;
		}

		
		if(Input.GetKey(KeyCode.W)) {
			_move.y = MovementSpeed;
			nextAnimation = PlayerAnimation.MoveUp;
		}
		
		if (Input.GetKey(KeyCode.S)) {
			_move.y = -MovementSpeed;
			nextAnimation = PlayerAnimation.MoveDown;
		}

		if (Input.GetKey(KeyCode.A)) {
			_move.x = -MovementSpeed;
			nextAnimation = PlayerAnimation.MoveLeft;
		}

		if (Input.GetKey(KeyCode.D)) {
			_move.x = MovementSpeed;
			nextAnimation = PlayerAnimation.MoveRight;
		}

		if(_lastAnimation != nextAnimation) {
			_lastAnimation = nextAnimation;
			switch(nextAnimation) {
			case PlayerAnimation.MoveDown:
				_animator.SetTrigger("MoveDown");
				break;
			case PlayerAnimation.MoveLeft:
				_animator.SetTrigger("MoveLeft");
				break;
			case PlayerAnimation.MoveRight:
				_animator.SetTrigger("MoveRight");
				break;
			case PlayerAnimation.MoveUp:
				_animator.SetTrigger("MoveUp");
				break;
			case PlayerAnimation.IdleDown:
				_animator.SetTrigger("IdleDown");
				break;
			case PlayerAnimation.IdleLeft:
				_animator.SetTrigger("IdleLeft");
				break;
			case PlayerAnimation.IdleRight:
				_animator.SetTrigger("IdleRight");
				break;
			case PlayerAnimation.IdleUp:
				_animator.SetTrigger("IdleUp");
				break;
			case PlayerAnimation.Spin:
				_animator.SetTrigger("Spin");
				break;
			}
		}
	}
}
