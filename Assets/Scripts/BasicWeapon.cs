using UnityEngine;
using System.Collections;

public class BasicWeapon : MonoBehaviour {
	public float CoolDown = 1.0f;
	public float ProjectileSpeed = 15.0f;
	public GameObject ProjectilePrefab;

	private float _timeToFire = 0.0f;
	private bool _firing = false;
	private Vector3 _target;


	void Start () {
	
	}

	void Fire() {
		Vector3 relative = _target - transform.position;
		
		GameObject bullet = (GameObject)Instantiate(Resources.Load("BulletOfDeathPrefab"), 
		                                            transform.position + relative.normalized * 0.5f, 
		                                            transform.rotation);
		
		bullet.rigidbody2D.velocity = relative.normalized * ProjectileSpeed;
	}

	void FixedUpdate() {
		if(_firing && _timeToFire <= 0) {
			_timeToFire = CoolDown;
			Fire();
		}
	}

	public void Update() {
		if(_timeToFire > 0) {
			_timeToFire -= Time.deltaTime;
		}
	}

	public void BeginFire() {
		_firing = true;
	}

	public void EndFire() {
		_firing = false;
	}

	public void AimAt(Vector3 target) {
		_target = target;
	}
}
