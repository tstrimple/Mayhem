using UnityEngine;
using System.Collections;

public class SelfDestruct : MonoBehaviour {
	public float Timer = 1.0f;
	private float _elapsed = 0.0f;

	void Start () {
	}
	
	void Update () {
		_elapsed += Time.deltaTime;
		if(_elapsed > Timer){
			Destroy(this.gameObject);
		}	
	}
}
