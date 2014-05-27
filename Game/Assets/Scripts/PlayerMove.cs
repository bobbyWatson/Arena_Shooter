using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMove : MonoBehaviour {
	
	public float speed;
	public Player player;
	private Vector2 moveVec;
	private Vector3 shootVec;
	public Vector3 ShootVec{
		get{return shootVec;}
	}
	public delegate void controlFunctionType();
	private controlFunctionType controlFunction;
	private int speedX;
	private int speedY;

	void Start (){
		if(player.keyboardControl)
			controlFunction = KeyboardControl;
		else
			controlFunction = GamepadControl;

		speedX = Animator.StringToHash("speedX");
		speedY = Animator.StringToHash("speedY");
	}

	void Update(){
		controlFunction();
		player.meshAnimator.SetFloat(speedX, moveVec.x);
		player.meshAnimator.SetFloat(speedY, moveVec.y);
		Move();
	}

	private void GamepadControl (){
		moveVec = GamepadInput.GamePad.GetAxis(GamepadInput.GamePad.Axis.LeftStick, player.gamePadIndex, false);
		shootVec = GamepadInput.GamePad.GetAxis(GamepadInput.GamePad.Axis.RightStick, player.gamePadIndex, false);
	}

	private void KeyboardControl (){
		moveVec.x = Input.GetAxis("Horizontal");
		moveVec.y = Input.GetAxis("Vertical");
		shootVec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		shootVec.z = player.canon.transform.position.z;
		shootVec = shootVec - player.canon.transform.position;
		shootVec = Vector3.Normalize(shootVec);
	}
	private void Move (){
		if(shootVec == Vector3.zero)
			shootVec = new Vector3(1,0,shootVec.z);
		rigidbody2D.velocity = moveVec * speed;
		Vector3 rotation = Vector3.zero;
		if((moveVec.x > 0.1 || moveVec.x < -0.1 )&& (moveVec.y > 0.1 || moveVec.y <-0.1)){
			rotation.z = Mathf.Rad2Deg * (Mathf.Atan2(moveVec.y, moveVec.x)) -90; 
			player.body.transform.rotation = Quaternion.Euler(rotation);
		}
		rotation.z = Mathf.Rad2Deg * (Mathf.Atan2(shootVec.y, shootVec.x)) -90; 
		player.canon.transform.rotation = Quaternion.Euler(rotation);

	}
}
