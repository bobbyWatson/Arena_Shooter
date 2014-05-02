using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public PlayerMove move;
	public PlayerShoot shoot;
	public GameObject body;
	public CircleCollider2D collider;
	public Animator meshAnimator;
	public Animator canonAnimator;
	public SpriteRenderer bodyRenderer;
	public SpriteRenderer canonRenderer;
	public GameObject canon;
	public SpriteRenderer target;
	public ParticleSystem explosion;
	public ParticleSystem douille;
	public bool keyboardControl = false;
	public int lifes;
	public Transform respawn;
	public float respawnTime;
	public enum Colors{
		RED,
		BLUE,
		GREEN,
		YELLOW,
		WHITE,
		BLACK,
		PURPLE,
		PINK,
		NONE
	}

	public Colors color;
	public GamepadInput.GamePad.Index gamePadIndex;
	public float invincibleTime;
	private bool invincible;

	public void Die (){
		if(!invincible){
			explosion.Play();
			move.enabled = false;
			shoot.enabled = false;
			rigidbody2D.Sleep();
			collider.enabled = false;
			bodyRenderer.enabled = false;
			canonRenderer.enabled = false;
			target.enabled = false;
			lifes--;
			if(lifes >= 0)
				StartCoroutine(Respawn());
		}
	}

	public IEnumerator Respawn (){
		yield return new WaitForSeconds(respawnTime);
		move.enabled = true;
		shoot.enabled = true;
		rigidbody2D.WakeUp();
		collider.enabled = true;
		bodyRenderer.enabled = true;
		canonRenderer.enabled = true;
		target.enabled = true;
		transform.position = respawn.position;
		invincible = true;
		StartCoroutine(Blink());
		yield return new WaitForSeconds(invincibleTime);
		invincible = false;
	}

	public IEnumerator Blink (){
		float t = 0;
		float alpha = 1;
		while(t < respawnTime){
			t += 0.1f;
			alpha = alpha == 1 ? 0 : 1;
			Color c = bodyRenderer.material.color;
			c.a = alpha;
			bodyRenderer.material.color = c;
			canonRenderer.material.color = c;
			yield return new WaitForSeconds(0.1f);
		}
	}

	public void Init (Player.Colors receivedColor, GamepadInput.GamePad.Index receivedIndex){
		color = receivedColor;
		gamePadIndex = receivedIndex;
		meshAnimator.runtimeAnimatorController = Game_Controller.instance.playerAnimatorDic[color];
		canonAnimator.runtimeAnimatorController = Game_Controller.instance.playerCanonAnimatorDic[color];
		target.sprite = Game_Controller.instance.playerTargetDic[color];
	}
}
