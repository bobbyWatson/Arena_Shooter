using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BulletManager : MonoBehaviour {

	public GameObject bulletPrefab;
	public int poolStartNumber;
	public int poolAddStep;
	private List<Bullet> pool;
	private GameObject poolContainer;


	void Awake (){
		poolContainer = new GameObject("pool");
		pool = new List<Bullet>();
		AddToPool(poolStartNumber);
		Game_Controller.instance.bulletManager = this;
	}


	private Bullet AddToPool (int number){
		for(int i = 0; i < number; i++){
			GameObject o = GameObject.Instantiate(bulletPrefab) as GameObject;
			Bullet bullet = o.GetComponent<Bullet>();
			bullet.transform.parent = poolContainer.transform;
			bullet.gameObject.SetActive(false);
			pool.Add(bullet);
		}
		return pool[pool.Count-1];
	}

	private int SortPool (Bullet b1, Bullet b2){
		if(b1.canUse == b2.canUse){
			return 0;
		}else if(b2.canUse == false){
			return -1;
		}
		return 1;
	}

	public void Pool(Bullet b){
		b.OnPool();
	}

	public Bullet UnPool(){
		pool.Sort(SortPool);
		Bullet b = pool[0];
		if(!pool[0].canUse)
			b = AddToPool(poolAddStep);
		b.OnUnpool();
		return b;
	}
}
