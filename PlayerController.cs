using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour {
    public static PlayerController instance;
    public float speed=1;
    InputDirection inputdire;
    Vector3 mousepos;
    bool mouseclick;
    PlayerPosition playerPosition;
    PlayerPosition fromPosition;
    Vector3 xdirection;
    Vector3 yzdirection;
    CharacterController characterController;
    float jumpvalue = 7;
    float gravity = 20;
    float jumpdistance = 1.4f;
    public bool canDoubleJump = true;
    bool doubleJump = false;
    bool isQuickMoving = false;
    float tempSpeed = 0;
    float QuickMoveTime = 10;
    public float QuickMoveTimeLeft;
    public Text statusText;
    IEnumerator quickMoveCor;

    float magnetMoveTime = 10;
    public float magnetMoveTimeLeft;
    IEnumerator magnetMoveCor;
    public GameObject megnetCollider;
    // Use this for initialization
    void Start () {
        instance = this;
        characterController = GetComponent<CharacterController>();
        playerPosition = PlayerPosition.Mid;
        StartCoroutine(UpdateAction());
    }
	
    IEnumerator UpdateAction()
    {
        while(true)
        {
            GetInputDirection();
            //PlayAnimation();
            MoveLeftRight();
            MoveUpDown();
            yield return 0;
        }
    }
    /// <summary>
    /// /////2018.3.15
    /// </summary>
    void MoveUpDown()
    {
       if(inputdire==InputDirection.Down)
        {
            GetComponent<Animation>().Stop();
            AnimationManager.instance.animationHandler = AnimationManager.instance.PlayRoll;
        }
		if (characterController.isGrounded) {
            yzdirection = Vector3.zero;
            if (AnimationManager.instance.animationHandler != AnimationManager.instance.PlayRoll
                && AnimationManager.instance.animationHandler != AnimationManager.instance.PlayTurnLeft
                && AnimationManager.instance.animationHandler != AnimationManager.instance.PlayTurnRight)
            {
                AnimationManager.instance.animationHandler = AnimationManager.instance.PlayRun;
            }
            if (inputdire == InputDirection.Up)
            {
                AnimationManager.instance.animationHandler = AnimationManager.instance.PlayJumpUp;
                yzdirection.y += jumpvalue;
                if (canDoubleJump)
                {
                    doubleJump = true;
                }
            }
        }
        else 
		{
            if (inputdire == InputDirection.Down)
            {
                QuickDown();
            }
            if (inputdire == InputDirection.Up)
            {
                if(doubleJump)
                {
                    JumpDouble();
                    doubleJump = false;
                }
            }
			if(AnimationManager.instance.animationHandler != AnimationManager.instance.PlayJumpUp
                && AnimationManager.instance.animationHandler != AnimationManager.instance.PlayRoll
                && AnimationManager.instance.animationHandler != AnimationManager.instance.PlayDoubleJump)
            {
                AnimationManager.instance.animationHandler = AnimationManager.instance.PlayJumpLoop;
            }

		}
    }

    void JumpDouble()
    {
        AnimationManager.instance.animationHandler = AnimationManager.instance.PlayDoubleJump;
        yzdirection.y += jumpvalue * jumpdistance;
    }

    void QuickDown()
    {
        yzdirection.y -= jumpvalue * 3;
    }

    void MoveLeft()
    {
        if(playerPosition != PlayerPosition.Left)
        {
            GetComponent<Animation>().Stop();
            AnimationManager.instance.animationHandler = AnimationManager.instance.PlayTurnLeft;

            xdirection = Vector3.left;
            if(playerPosition==PlayerPosition.Mid)
            {
                playerPosition = PlayerPosition.Left;
                fromPosition = PlayerPosition.Mid;
            }
            if (playerPosition == PlayerPosition.Right)
            {
                playerPosition = PlayerPosition.Mid;
                fromPosition = PlayerPosition.Right;
            }
        }
    }

    void MoveRight()
    {
        if (playerPosition != PlayerPosition.Right)
        {
            GetComponent<Animation>().Stop();
            AnimationManager.instance.animationHandler = AnimationManager.instance.PlayTurnRight;

            xdirection = Vector3.right;
            if (playerPosition == PlayerPosition.Mid)
            {
                playerPosition = PlayerPosition.Right;
                fromPosition = PlayerPosition.Mid;
            }
            if (playerPosition == PlayerPosition.Left)
            {
                playerPosition = PlayerPosition.Mid;
                fromPosition = PlayerPosition.Left;
            }
        }
    }

    void MoveLeftRight()
    {
        if(inputdire==InputDirection.Left)
        {
            MoveLeft();
        }
        else if (inputdire == InputDirection.Right)
        {
            MoveRight();
        }

        if(playerPosition == PlayerPosition.Left)
        {
            if (transform.position.x < -1.7f) 
            {
                xdirection = Vector3.zero;
                transform.position = new Vector3(-1.7f, transform.position.y, transform.position.z);
            }
        }
        if(playerPosition==PlayerPosition.Mid)
        {
            if(fromPosition == PlayerPosition.Left)
            {
                if(transform.position.x>=0)
                {
                    xdirection = Vector3.zero;
                    transform.position = new Vector3(0f, transform.position.y, transform.position.z);
                }
            }
            if (fromPosition == PlayerPosition.Right)
            {
                if (transform.position.x <= 0)
                {
                    xdirection = Vector3.zero;
                    transform.position = new Vector3(0f, transform.position.y, transform.position.z);
                }
            }
        }
        if (playerPosition == PlayerPosition.Right)
        {
            if (transform.position.x > 1.7f)
            {
                xdirection = Vector3.zero;
                transform.position = new Vector3(1.7f, transform.position.y, transform.position.z);
            }
        }
    }
    public void QuickMove()
    {
        if(quickMoveCor != null)
        {
            StopCoroutine(quickMoveCor);
        }
        quickMoveCor = QuickMoveCoroutine();
        StartCoroutine(quickMoveCor);
    }

    IEnumerator QuickMoveCoroutine()
    {
        QuickMoveTimeLeft = QuickMoveTime;
        if (!isQuickMoving)
            tempSpeed = speed;
        isQuickMoving = true;
        speed = 10;
        while(QuickMoveTimeLeft>=0)
        {
            QuickMoveTimeLeft -= Time.deltaTime;
            yield return null;
        }
        speed = tempSpeed;
        isQuickMoving = false;
    }
    public void MegnetMove()
    {
        if (magnetMoveCor != null)
        {
            StopCoroutine(quickMoveCor);
        }
        magnetMoveCor = MegnetMoveCoroutine();
        StartCoroutine(magnetMoveCor);
    }
    
    IEnumerator MegnetMoveCoroutine()
    {
        magnetMoveTimeLeft = magnetMoveTime;
        megnetCollider.SetActive(true);

        while (magnetMoveTimeLeft >= 0)
        {
            magnetMoveTimeLeft -= Time.deltaTime;
            yield return null;
        }
        megnetCollider.SetActive(false);
    }

    void PlayAnimation()
    {
        if (inputdire==InputDirection.Left)
        {
            AnimationManager.instance.animationHandler = AnimationManager.instance.PlayTurnLeft;
        }
        else if (inputdire == InputDirection.Right)
        {
            AnimationManager.instance.animationHandler = AnimationManager.instance.PlayTurnRight;
        }
        else if (inputdire == InputDirection.Up)
        {
            AnimationManager.instance.animationHandler = AnimationManager.instance.PlayJumpUp;
        }
        else if (inputdire == InputDirection.Down)
        {
            AnimationManager.instance.animationHandler = AnimationManager.instance.PlayRoll;
        }
    }
    void GetInputDirection()
    {
        inputdire = InputDirection.NULL;
        if(Input.GetMouseButtonDown(0))
        {
            mouseclick = true;
            mousepos = Input.mousePosition;
        }
        if(Input.GetMouseButtonUp(0) && mouseclick)
        {
            Vector3 vec = Input.mousePosition - mousepos;
            if (vec.magnitude > 20) 
            {
                var angleY = Mathf.Acos(Vector3.Dot(vec.normalized, Vector2.up))*Mathf.Rad2Deg;
                var angleX = Mathf.Acos(Vector3.Dot(vec.normalized, Vector2.right)) * Mathf.Rad2Deg;
                if (angleY < 45) 
                {
                    inputdire = InputDirection.Up;
                }
                else if (angleY > 135)
                {
                    inputdire = InputDirection.Down;
                }
                else if (angleX <= 45)
                {
                    inputdire = InputDirection.Right;
                }
                else if (angleX >= 135)
                {
                    inputdire = InputDirection.Left;
                }
                mouseclick = false;
                Debug.Log(inputdire);
            }
        }
    }
	// Update is called once per frame
	void Update () {
        //transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
        yzdirection.z = speed;
        yzdirection.y -= gravity * Time.deltaTime; 
        characterController.Move((xdirection * 8 + yzdirection) * Time.deltaTime);

        statusText.text = GetTime(QuickMoveTimeLeft);
	}
    string GetTime(float time)
    {
        if (time <= 0)
            return "0";
        else
            return Mathf.RoundToInt(time).ToString();
    }
}

public enum InputDirection
{
    NULL,
    Up,
    Left,
    Down,
    Right
}

public enum PlayerPosition
{
    Left,
    Mid,
    Right
}
