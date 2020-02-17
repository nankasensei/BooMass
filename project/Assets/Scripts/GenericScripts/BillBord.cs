//作成者 : 植村将太
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Rotationをカメラに合わせるBillbord
/// </summary>
public class Billbord : MonoBehaviour
{
	/// <summary>[Update]</summary>
	void Update()
	{
		//回転をカメラに向かせる
		transform.rotation = Quaternion.LookRotation((Camera.main.transform.position - transform.position).normalized);
	}
}
