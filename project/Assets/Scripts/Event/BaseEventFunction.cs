//作成者 : 植村将太
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Event処理関数のBaseとなるBaseEventFunction class
/// </summary>
public abstract class BaseEventFunction : MonoBehaviour
{
	///<summary>自身が所属するEventPoint</summary>
	public EventPoint myPoint { get; set; } = null;
	///<summary>Receive EventNumber</summary>
	public int receiveNumber { get { return m_receiveNumber; } }
	///<summary>this event instance id</summary>
	public int instanceEventID { get; private set; } = -1;

	///<summary>Instance id counter</summary>
	static int m_instanceIDCounter = 0;

	///<summary>メッセージ受信設定</summary>
	[SerializeField, Tooltip("メッセージ受信設定")]
	bool m_isReceiveMessage = false;
	///<summary>受信するEventNumber</summary>
	[SerializeField, Tooltip("受信するEventNumber")]
	int m_receiveNumber = 0;

	//Debug Only
#if UNITY_EDITOR
	/// <summary>OnValidate用OldBuf</summary>
	int m_oldReceiveEventNumber = -1;
	/// <summary>OnValidate用OldBuf</summary>
	bool m_isOldReceiveMessage = false;
#endif

	/// <summary>
	/// [EventCallback]
	/// イベント発生時に行われるコールバック
	/// 引数1: 呼び出しクラスの識別名
	/// 引数2: イベントオブジェクト名
	/// 引数3: イベントタグ
	/// 引数4: イベント番号
	/// </summary>
	public abstract void EventCallback(GameObject eventObject, GameObject detectionObject,
		GameObject parameter1, string parameter2, long parameter3);

	/// <summary>[Start]</summary>
	protected void Start()
	{
		//Debug Only, 保存
#if UNITY_EDITOR
		m_oldReceiveEventNumber = m_receiveNumber;
		m_isOldReceiveMessage = m_isReceiveMessage;
#endif

		//Receive設定の場合Managerに登録
		if (m_isReceiveMessage)
			EventManager.instance.AddFunction(this);
		//InstanceID設定
		if (instanceEventID == -1)
			instanceEventID = m_instanceIDCounter++;
	}

	//Debug Only
#if UNITY_EDITOR
	/// <summary>[OnValidate]</summary>
	protected void OnValidate()
	{
		//Managerがいなければ終了
		if (EventManager.instance == null) return;

		//Not Receive->Receive = 登録
		if ((m_isReceiveMessage ^ m_isOldReceiveMessage) & m_isReceiveMessage)
			EventManager.instance.AddFunction(this);
		//Receive->Not Receive = 解除
		else if ((m_isReceiveMessage ^ m_isOldReceiveMessage) & m_isOldReceiveMessage)
			EventManager.instance.RemoveFunction(this);
		//Receive != Receive = 解除 & 登録
		else if (m_oldReceiveEventNumber != m_receiveNumber)
		{
			var temp = m_receiveNumber;
			m_receiveNumber = m_oldReceiveEventNumber;
			EventManager.instance.RemoveFunction(this);
			m_receiveNumber = temp;
			EventManager.instance.AddFunction(this);
		}

		//保存
		m_oldReceiveEventNumber = m_receiveNumber;
		m_isOldReceiveMessage = m_isReceiveMessage;
	}
#endif

	/// <summary>[OnDestroy]</summary>
	protected void OnDestroy()
	{
		//Receive設定の場合Remove
		if (m_isReceiveMessage && EventManager.instance)
			EventManager.instance.RemoveFunction(this);
	}
}
