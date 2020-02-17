using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player statistics")]
    public float PLAYER_HP_MAX = 1;
    public float hp = 1;
    public bool isAlive = true;
    public float explodeTime;
    public DamageRequest damageRequest;

    [SerializeField]
    LayerMaskEx Gimmic01Layer;
    LayerMaskEx Gimmic02Layer;

    [SerializeField]
    PlayerExplosionManagement m_playerExplosionManagement = null;

    public AudioSource audioSource;
    public AudioClip connectingSound;
    public AudioClip hittingSound;

    public Vector3 movementDirection;

    [SerializeField]
    Rigidbody m_rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        TakeCollision.callback += PassFire;
        TakeCollision.callback += PlayConnectSound;
    }

    // Update is called once per frame
    void Update()
    {
		if (!GameSceneManager.instance.isInGame)
			return;

		Move();

        DamageCheck();			
    }

    void DamageCheck()
    {
        if (damageRequest.requestQueue.Count > 0)
        {
            foreach (var damageQuest in damageRequest.requestQueue)
            {
                takeDamage(damageQuest.attack);
            }
        }
    }

    void Move()
    {
        //Debug.Log(m_rigidbody.velocity.magnitude);
        movementDirection = new Vector3(Input.GetAxis("Vertical"), 0.0f, -Input.GetAxis("Horizontal"));

        m_rigidbody.AddTorque(movementDirection * 200000 * Time.deltaTime);

        //audioSource.clip = movingSound;
        //if (m_rigidbody.velocity.magnitude != 0 && !audioSource.isPlaying)
        //{
        //    audioSource.volume = (m_rigidbody.velocity.magnitude / 3.5f) * 0.2f;
        //    audioSource.Play();
        //}
    }

    void PlayConnectSound()
    {
        audioSource.volume = 1.0f;
        audioSource.PlayOneShot(connectingSound);
    }


    public void takeDamage(float damage)
    {
        hp -= damage;
        if( hp <= 0 )
        {
            m_playerExplosionManagement.Explosion();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Walls" || collision.gameObject.name == "Ground" || collision.gameObject.name == "DynamicFloor" || collision.gameObject.name == "DynamicWall")
        {
            audioSource.volume = 0.7f;
            audioSource.PlayOneShot(hittingSound);
        }
    }

    public void PassFire()
    {
    }

    void Explode()
    {
        Debug.Log("Exploded");
    }
}


