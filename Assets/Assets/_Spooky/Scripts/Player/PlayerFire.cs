 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using CnControls;

public class PlayerFire : MonoBehaviour
{
   /* [SerializeField]
    private Vector3 clickPoint;
    [SerializeField]
    private PlayerMove my_Player; */
    public GameObject bulletP;

    [SerializeField]
    private float shootCooldown;

	[SerializeField]
	private Transform shootTransform;

    [SerializeField]
    private float velocidadAtaque = 15f;
	[SerializeField]
	private float moveForce = 6.5f;

	private Vector3 shootDirection;

	public BulletController playerBullet;

	private float horizontalAxis;
	private float verticalAxis;
   // private Ray my_Ray;
   // private RaycastHit my_RayHit;

    void Awake()
    {
       // my_Player = GetComponent<PlayerMove>();
    }
    private void Start()
    {
        playerBullet.Initialize();
    }
    void FixedUpdate()
    {
		horizontalAxis = CnInputManager.GetAxis ("Horizontal");
		verticalAxis = CnInputManager.GetAxis ("Vertical");

		if (shootCooldown >= 0)
			shootCooldown -= Time.deltaTime;

		if (horizontalAxis != 0 || verticalAxis != 0) {
			FireWithJoystick (horizontalAxis, verticalAxis);
			RotateTransform (horizontalAxis, verticalAxis );

//			Debug.Log ("H= " + horizontalAxis+ "V= " +verticalAxis );
		}
    }
 
	public void FireWithJoystick(float h, float v)
    {
        if (shootCooldown > 0.0f)
        {
            return;
        }

		shootDirection = new Vector3(h, 0, v);
        shootCooldown = velocidadAtaque;
        GameObject bullet = PoolsManagerBullets.Instance.GetBullet(playerBullet.bulletInfo.objectIndex);

		//GameObject bullet = Instantiate (bulletP, shootTransform.position, shootTransform.rotation) as GameObject;

		bullet.transform.position = shootTransform.position;
        bullet.transform.rotation = shootTransform.transform.rotation;
		bullet.GetComponent <Rigidbody > ().AddForce(shootTransform.forward * moveForce , ForceMode.Impulse);

		Debug.DrawRay (shootTransform.position, shootTransform.forward * 20, Color.green, 10);

	//	Debug.Log (shootDirection);
	//	Debug.DrawRay (transform.position, shootDirection, Color.red, 10, false);

        //Se le da la direccion en la cual esta manipulando el joystick
        //bullet.GetComponent<BulletController>().SetDirection(direction);
    }

	public void RotateTransform(float posX, float posZ)
    {
		float rotY = Mathf.Atan2(posX, posZ) * Mathf.Rad2Deg;
		shootTransform.rotation = Quaternion.Euler(0f, rotY , 0f);        

		//Debug.DrawRay(shootTransform .position, shootTransform .forward* 10, Color.red, 5);
	}
}
