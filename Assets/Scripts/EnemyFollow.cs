using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class EnemyFollow : MonoBehaviour
{

    enum TypeFollow
    {
        PATH = 0,
        POINT = 1,
        OBJECT = 2
    }

    [SerializeField]
    private float Mass = 45;

    [SerializeField]
    private float MaxSpeed = 150;

    [SerializeField]
    private float MaxForce = 15;

    [SerializeField]
    private float minDistanceToStop = 0.5f;

    private Vector2 velocity;

    private bool following = false;

    private Vector2 targetPoint;

    [SerializeField]
    private GameObject target;

    [SerializeField]
    private TypeFollow typeFollow;

    public UnityEvent onIdle;

    public UnityEvent onCloser;

    public UnityEvent onFollow;


	// Use this for initialization
	void Start ()
    {
        velocity = Vector2.zero;

	    if (isTargetAlive())
        {
            typeFollow = TypeFollow.OBJECT;
            following = true;
            invokeOnFollow();
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
	
        if (following)
        {
            var desiredVelocity = getDesiredVelocity();

            var steeringForce = desiredVelocity - velocity;

            steeringForce = Vector3.ClampMagnitude(steeringForce, MaxForce);
            steeringForce /= Mass;

            velocity = Vector3.ClampMagnitude(velocity + steeringForce, MaxSpeed);

            transform.position += (Vector3)(velocity * Time.deltaTime);
        }

        if (following && !checkMustFollow())
        {
            following = false;

            invokeOnCloser();

        } 

        if (!following && checkMustFollow())
        {
            following = true;

            invokeOnFollow();
        }

        updateFlipSide();
    }

    /// <summary>
    /// Sets the target to follow.
    /// </summary>
    /// <param name="target"></param>
    public void setTarget(GameObject target, bool follow = false)
    {
        this.target = target;
        this.typeFollow = TypeFollow.OBJECT;
        this.following = follow;
    }

    /// <summary>
    /// Return the game object target follow
    /// </summary>
    public GameObject getTarget()
    {
        return target;
    }

    /// <summary>
    /// Get desired velocity to stearing.
    /// </summary>
    /// <returns></returns>
    private Vector2 getDesiredVelocity()
    {
        Vector2 desiredVelocity = Vector2.zero;

        switch (typeFollow)
        {
		case TypeFollow.OBJECT:
			desiredVelocity = ((Vector2)target.transform.position - (Vector2)transform.position).normalized * MaxSpeed;
			break;

            case TypeFollow.POINT:
                desiredVelocity = ((Vector2)targetPoint - (Vector2)transform.position).normalized * MaxSpeed;
                break;
        }

        return desiredVelocity;
    }

    /// <summary>
    /// Return if must follow the target
    /// </summary>
    /// <returns></returns>
    private bool checkMustFollow()
    {
        bool mustFollow = false;

        switch (typeFollow)
        {
            case TypeFollow.OBJECT:
                
                if (!target)
                {
                    return false;
                }

                if (Vector2.Distance(transform.position, target.transform.position) <= minDistanceToStop)
                {
                    mustFollow = false;

                } else
                {
                    mustFollow = true;
                }

                break;

            case TypeFollow.POINT:

                // TODO: Must be implemented
                mustFollow = false; 

                break;

            case TypeFollow.PATH:

                // TODO: Must be implemented

                return false;

                break;

            default:

                // TODO: Must be implemented

                return false;

                break;
        }

        

        return mustFollow;
    }

    public bool isCloser()
    {
        return checkMustFollow();
    }

    /// <summary>
    /// Flip side walking
    /// </summary>
    public void updateFlipSide()
    {
        float xScale = 0;

        // Left
        if (velocity.x < 0)
        {
            xScale = Mathf.Abs(transform.localScale.x) * -1;

        } else // Right
        {
            xScale = Mathf.Abs(transform.localScale.x);
        }

        transform.localScale = new Vector3(
                xScale,
                transform.localScale.y,
                transform.localScale.z);
    }

    /// <summary>
    /// Return if is following
    /// </summary>
    /// <returns>bool</returns>
    public bool isFollowing()
    {
        return following;
    }

    /// <summary>
    /// Return if targets still alive to be followed
    /// </summary>
    /// <returns>bool</returns>
    private bool isTargetAlive()
    {

        bool isAlive = false;

        switch (typeFollow)
        {
            case TypeFollow.OBJECT:

                if (target != null && target.activeSelf)
                {
                    isAlive = true;
                }

                break;

            case TypeFollow.PATH:
                isAlive = false;
                break;

            case TypeFollow.POINT:
                isAlive = false;
                break;
        }

        return isAlive;
    }

    /// <summary>
    /// Invoke onCloser event
    /// </summary>
    private void invokeOnCloser()
    {
        switch (typeFollow)
        {
            default:
            case TypeFollow.OBJECT:

                if (target != null && target.activeSelf)
                {
                    onCloser.Invoke();

                }
                
                break;
        }
    }

    /// <summary>
    /// Invoke 
    /// </summary>
    private void invokeOnFollow()
    {
        onFollow.Invoke();
    }

    public void stopFollow()
    {
        following = false;
    }
}
