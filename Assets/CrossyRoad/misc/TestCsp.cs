using UnityEngine;
using System.Collections;
using Sa1;


public class TestCsp : MonoBehaviour {

	// Use this for initialization
	void Start () {
		csp.go(this, simple());
		csp.go(this, putYield());
		// csp.go(this, timeout());
		// csp.go(this, wholeTimeout());
		// csp.go(this, daisyChainFunctor());
		//csp.go(GetComponent<Bullet>(), daisyChainFunctor());
		// csp.go(this, pingpongFunctor());
	}

	// Update is called once per frame
	void Update () {
		//csp.go(this, put("asdf"));
		//csp.go(this, put("end"));
	}

	public csp.Channel boring (string message) {
		var ch = csp.chan();
		csp.go(this, boringGoroutine(message, ch));
		return ch;
	}

	IEnumerator boringGoroutine (string message, csp.Channel ch) {
		yield return new WaitForSeconds(0.5f);
		for (var i = 0; i < 1000; i++) {
			yield return csp.put(ch, message);
			yield return csp.timeout(Random.value * 1);
		}
	}

	/// <summary>
	/// test1, simple test to take from channel
	/// </summary>
	IEnumerator simple () {
		var ch = boring("boring.simple");
		for (int i = 0; i < 5; i++) {
			yield return csp.take(ch);
			Debug.Log("you say " + csp.ret);
		}
		Debug.Log("you are boring");
	}

	IEnumerator putYield() {
		var ch = csp.chan();
		csp.takeAsync(this, ch, ob =>{
			Debug.Log((int)ob);
			Debug.Log("take next put");
		});
		yield return csp.put(ch, 1);
		csp.putAsync(this, ch, 1);
		yield return csp.take(ch);
		Debug.Log("take before");
		yield return csp.put(ch, 1);
		yield return csp.take(ch);
		Debug.Log("you are not expected to see this");
	}

	IEnumerator timeout () {
		var ch = boring("boring.timeout");
		while (!ch.closed) {
			yield return csp.alts(ch, csp.timeout(0.5f));
			if (csp.isTimeout) {
				Debug.Log("you are slow than 0.5s");
			}
			else {
				Debug.Log(csp.ret);
			}
		}
	}

	IEnumerator wholeTimeout () {
		var ch = boring("boring.wholeTimeout");
		var timer = csp.timeout(2);
		while (!ch.closed) {
			yield return csp.alts(ch, timer);
			if (csp.isTimeout) {
				Debug.Log("you talk too much");
				break;
			}
			else {
				Debug.Log(csp.ret);
			}
		}
	}

	IEnumerator daisyChainFunctor () {
		var n = 1000;
		var leftmost = csp.chan();
		var right = leftmost;
		var left = leftmost;

		for (var i = 0; i < n; i++) {
			right = csp.chan();
			csp.go(this, chain(left, right));
			left = right;
		}

		csp.putAsync(this, right, 1);
		var startTime = Time.time;

		yield return csp.take(leftmost);
		var endTime = Time.time;
		Debug.Log("daisy-chain: " + (int)csp.ret + " time: " + (endTime - startTime));
		// 10 - 0.4s
		// 100 - 2.2s
		// 500 - 10.6s
		// 1000 - 22.2s
	}

	IEnumerator chain (csp.Channel left, csp.Channel right) {
		yield return csp.take(right);
		yield return csp.put(left, 1 + (int)csp.ret);
	}

	IEnumerator pingpongFunctor () {
		var table = csp.chan(2);
		csp.go(this, player("ping", table));
		csp.go(this, player("pong", table));

		yield return csp.put(table, 0);
		yield return csp.put(table, 0);
		yield return csp.timeout(2f);

		table.close();
	}

	IEnumerator player (string playerName, csp.Channel table) {
		while (!table.closed) {
			yield return csp.take(table);
			var t = 1 + (int)csp.ret;
			Debug.Log(playerName + " " + t);

			yield return csp.put(table, t);
			yield return csp.timeout(0.5f);
		}
	}
}
