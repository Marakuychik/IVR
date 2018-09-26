using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovingObject : MonoBehaviour 
{
	public float mTime = 0.1f;
	public LayerMask blockLayer;

	private BoxCollider2D bCol;
	private Rigidbody2D rb2d;
	private float invmTime;

	protected virtual void Start () 
	{
		bCol = GetComponent<BoxCollider2D> ();
		rb2d = GetComponent<Rigidbody2D> ();
		invmTime = 1f / mTime;
	}

	protected bool Move (int xDir, int yDir, out RaycastHit2D hit)
	{
		Vector2 start = transform.position;
		Vector2 end = start + new Vector2 (xDir, yDir);

		bCol.enabled = false;
		hit = Physics2D.Linecast (start, end, blockLayer);
		bCol.enabled = true;
		if (hit.transform == null) 
		{
			StartCoroutine (SM (end));
			return true;
		}
		return false;
	}

	protected IEnumerator SM (Vector3 end)
	{
		float sqrRD = (transform.position - end).sqrMagnitude;

		while (sqrRD > float.Epsilon) 
		{
			Vector3 nPos = Vector3.MoveTowards (rb2d.position, end, invmTime * Time.deltaTime);
			rb2d.MovePosition (nPos);
			sqrRD = (transform.position - end).sqrMagnitude;
			yield return null;
		}
	}

	protected virtual void AM <T> (int xDir, int yDir)
		where T : Component
	{
		RaycastHit2D hit;
		bool cM = Move (xDir, yDir, out hit);
		if (hit.transform == null)
			return;
		T hitComponent = hit.transform.GetComponent<T> ();
		if (!cM && hitComponent != null)
			OCM (hitComponent);
	}
		
	protected abstract void OCM <T> (T component)
		where T :  Component;
}
