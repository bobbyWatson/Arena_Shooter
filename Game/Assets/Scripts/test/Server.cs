using UnityEngine;
using System.Collections;

public class Server : MonoBehaviour {

	private string myIp;
	private bool creatingServer = false;
	
	void OnGUI (){
		if(!Network.isServer && !Network.isClient){
			if(GUI.Button(new Rect(0,0,150,30), "createServer")){
				StartCoroutine(createServer());
			}
		}else if(creatingServer){
			GUI.Label(new Rect(0,0,150,30), "creating server");
		}else if(Network.isServer){
			GUI.Label(new Rect(0,0,150,30), "currenlty server");
		}

	}

	IEnumerator createServer (){
		Network.InitializeServer(3,8080,false);
		creatingServer = true;
		yield return StartCoroutine(GetMyIP());
		WWWForm form = new WWWForm();
		form.AddField("ip", myIp);
		WWW www = new WWW("localhost/RampageRooster/createServer.php",form);
		while(!www.isDone){
			yield return null;
		}
		creatingServer = false;
		yield break;
	}

	IEnumerator GetMyIP(){
		WWW myExtIPWWW = new WWW("http://checkip.dyndns.org");
		yield  return myExtIPWWW;
		myIp = myExtIPWWW.data;
		myIp = myIp.Substring(myIp.IndexOf(":")+1);
		myIp = myIp.Substring(0,myIp.IndexOf("<"));
		yield break;
	}
}
