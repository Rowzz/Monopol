using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class Dice : MonoBehaviourPunCallbacks {

	Rigidbody rb;

	bool hasLanded;
	bool thrown;

	Vector3 initPosition;

	public DiceSide[] diceSides;
    public GameController gameController;

    void Start()
	{
		rb = GetComponent<Rigidbody>();
		initPosition = transform.position;
		rb.useGravity = false;
	}

	void Update()
	{
		if (!thrown && Input.GetKeyDown(KeyCode.Space) && gameController.IsOwnerTurn())
		{
			RollDice();
		}

		if (!hasLanded && thrown && rb.IsSleeping())
		{
            rb.isKinematic = hasLanded = true;
			rb.useGravity = false;

			SideValueCheck();
		}
	}

    public void RollDice()
	{
        float x = Random.Range(0, 500);
        float y = Random.Range(0, 500);
        float z = Random.Range(0, 500);
        RollDice(x, y, z);
        //int DiceIndex = GameController.Dices.IndexOf(this) ?
        //NetworkController.SendDice(X,Y,Z, DiceIndex);
    }

    public void RollDice(float x, float y, float z)
    {
        Reset();
        rb.useGravity = thrown = true;

        rb.AddTorque(x, y, z);
    }

    void Reset()
	{
		transform.position = initPosition;
        rb.isKinematic = rb.useGravity = thrown = hasLanded = false;
	}

    public void SetDiceLock(bool status)
    {
        thrown = status;
    }

    public bool GetDiceLock()
    {
        return thrown;
    }

	void SideValueCheck()
	{
		foreach (DiceSide side in diceSides)
		{
			if (side.OnGround())
			{
                gameController.SetDiceValue(side.sideValue);
			}
		}
	}
}
