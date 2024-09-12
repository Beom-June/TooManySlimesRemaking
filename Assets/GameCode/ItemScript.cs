using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ItemScript : MonoBehaviour {

	public enum CollectibleTypes {NoType, purpleJewel, Type2, Type3, Type4, Type5};
	public CollectibleTypes CollectibleType; 

	[Header("Item Setting")]
	[SerializeField] private bool _rotate;					//	Roate Bool
	[SerializeField] private float _rotationSpeed;			//	아이템 회전 속도
	[SerializeField] private AudioClip _collectSound;		//	아이템 획득시 사운드
	[SerializeField] private GameObject _collectEffect;		//	획득시 이펙트 (파티클)

	void Start () 
	{
		
	}
	
	void Update () 
	{
		if (_rotate)
        {
			transform.Rotate (Vector3.up * _rotationSpeed * Time.deltaTime, Space.World);
        }
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") {
			Collect ();
		}
	}

	public void Collect()
	{
		if(_collectSound)
			AudioSource.PlayClipAtPoint(_collectSound, transform.position);
		if(_collectEffect)
			Instantiate(_collectEffect, transform.position, Quaternion.identity);

		if (CollectibleType == CollectibleTypes.NoType) {

			//Add in code here;

		}
		if (CollectibleType == CollectibleTypes.purpleJewel) {

			//Add in code here;
		}
		if (CollectibleType == CollectibleTypes.Type2) {

			//Add in code here;
		}
		if (CollectibleType == CollectibleTypes.Type3) {

			//Add in code here;
		}
		if (CollectibleType == CollectibleTypes.Type4) {

			//Add in code here;
		}
		if (CollectibleType == CollectibleTypes.Type5) {

			//Add in code here;
		}
		Destroy (gameObject);
	}
}