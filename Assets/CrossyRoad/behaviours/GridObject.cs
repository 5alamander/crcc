using UnityEngine;
using System.Collections;
using Sa1;

/// <summary>
/// generic grid object
/// </summary>
public class GridObject : MonoBehaviour {

	csp.Channel ch = csp.chan();

	// Use this for initialization
	void Start () {
		csp.go(this, recieve());
		csp.go(this, put(""));
	}

	// Update is called once per frame
	void Update () {
		//csp.go(this, put("asdf"));
		//csp.go(this, put("end"));
	}

	IEnumerator put (string str) {
		//yield return csp.put(ch, str);
		yield return csp.timeout(5f);
		csp.asyncPut(this, ch, "end");
	}

	IEnumerator recieve () {
		//while (true) {
		//	yield return csp.take(ch);
		//	Debug.Log(csp.ret.ToString());
		//	if (csp.ret.Equals("end")) break;
		//}
		yield return csp.alts(ch, csp.timeout(6f));
		if (csp.isTimeout) {
			Debug.Log("timeout");
		}
		else {
			Debug.Log(csp.ret);
		}
	}
}
