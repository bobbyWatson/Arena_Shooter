using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Utils {

	public static Dictionary<T,T2>CreateDictionnary<T,T2>(T[] keys, T2[]values){
		Dictionary<T,T2> dic = new Dictionary<T,T2>();
		for(int i = 0; i < keys.Length; i++){
			dic.Add(keys[i],values[i]);
		}
		return dic;	
	}
}
