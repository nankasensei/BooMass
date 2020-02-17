using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageRequest : MonoBehaviour
{
	/// <summary>
	/// リクエストの中身となるRequestQueue
	/// </summary>
	[System.Serializable]
	public struct RequestQueue
	{
		public RequestQueue(GameObject attackObject, float attack, AttackType attackType)
		{
			this.attackObject = attackObject;
			this.attack = attack;
			this.attackType = attackType;
		}

		/// <summary>攻撃するオブジェクト</summary>
		[Tooltip("攻撃するオブジェクト")]
		public GameObject attackObject;
		/// <summary>攻撃力</summary>
		[Tooltip("攻撃力")]
		public float attack;
		/// <summary>攻撃力</summary>
		[Tooltip("攻撃タイプ")]
		public AttackType attackType;
	}

	/// <summary>リクエストを貯めるキュー</summary>
	public List<RequestQueue> requestQueue { get { return m_requestQueue; } }

	/// <summary>ダメージリクエストを描画</summary>
	[SerializeField, Tooltip("ダメージリクエストを描画")]
	List<RequestQueue> m_requestQueue = new List<RequestQueue>();

	/// <summary>
	/// [Request]
	/// ダメージをリクエストする
	/// 引数1: 攻撃を行うオブジェクト
	/// 引数2: ふっとばし力
	/// 引数3: 攻撃力
	/// 引数4: 吹っ飛びタイプ
	/// </summary>
	public void Request(GameObject attackObject, float attack, AttackType attackType)
	{
		requestQueue.Add(new RequestQueue(attackObject, attack, attackType));
	}
}
