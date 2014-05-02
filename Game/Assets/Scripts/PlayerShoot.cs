using UnityEngine;
using System.Collections;

public class PlayerShoot : MonoBehaviour {

	public Player player;
	public float inputShootStep = 0.8f;
	public float bulletSpeed;
	private PlayerMove.controlFunctionType controleFunction;
	private bool shoots = false;
	private int shooting;

	void Start (){
		if(player.keyboardControl)
			controleFunction = KeyboardControl;
		else
			controleFunction = GamepadControl;

		shooting = Animator.StringToHash("shooting");
	}

	void Update (){
		controleFunction();
	}

	void GamepadControl (){
		float triggerPressure = GamepadInput.GamePad.GetTrigger(GamepadInput.GamePad.Trigger.RightTrigger, player.gamePadIndex,false);
		if(!shoots && triggerPressure >= inputShootStep){
			Shoot();
			shoots = true;
		}else if(shoots && triggerPressure <= inputShootStep){
			shoots = false;
		}
	}

	void KeyboardControl (){
		if(Input.GetMouseButtonDown(0))
			Shoot();
	}

	void Shoot (){
		player.douille.Play();
		player.canonAnimator.SetTrigger(shooting);
		Bullet bullet = Game_Controller.instance.bulletManager.UnPool();
		bullet.transform.position = player.transform.position;
		StartCoroutine(enableBullet(bullet));
		bullet.originColor = player.color;
		bullet.rigidbody2D.velocity = player.move.ShootVec * bulletSpeed;
	}

	IEnumerator enableBullet (Bullet bullet){
		yield return new WaitForSeconds(0.1f);
		bullet.gameObject.layer = LayerMask.NameToLayer("Bullet");
	}
}
