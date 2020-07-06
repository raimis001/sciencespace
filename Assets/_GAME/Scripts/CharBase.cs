using UnityEngine;

public class CharBase : MonoBehaviour
{

	private Animator anim;
	private void Awake()
	{
		anim = GetComponent<Animator>();
		anim.enabled = false;
	}

	private void Start()
	{
		Invoke("StartAnimator", Random.Range(0, 10));
	}

	void StartAnimator()
	{
		anim.enabled = true;
	}
}
