using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerFollowPlayer : MonoBehaviour
{
	[SerializeField] private Transform playerTransform;

	void Update()
	{
		transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, transform.position.z);
	}
}
