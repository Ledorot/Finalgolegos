using UnityEngine;

[RequireComponent(typeof(Camera))]
public class Camera2DFollow : MonoBehaviour
{

    public Transform target;
    public Transform leftLimit;
    public Transform rightLimit;
    public Transform upLimit;
    public Transform downLimit;
    public float damping = 1;
    public float lookAheadFactor = 3;
    public float lookAheadReturnSpeed = 0.5f;
    public float lookAheadMoveThreshold = 0.1f;
	public float yRestriction = -1;
    public float offset = 2f;

    private float offsetZ;
    private Vector3 lastTargetPosition;
    private Vector3 currentVelocity;
    private Vector3 lookAheadPos;
	private float nextTimeToSearch = 0f;

    private Camera mainCamera;
    private bool leftLimited = false;
    private bool rightLimited = false;
    private bool upLimited = false;
    private bool downLimited = false;

    // Use this for initialization
    private void Start()
    {
        lastTargetPosition = target.position;
        offsetZ = (transform.position - target.position).z;
        transform.parent = null;
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    private void Update()
    {
		if (target == null) {
			FindPlayer ();
			return;
		}
        // only update lookahead pos if accelerating or changed direction
        float xMoveDelta = (target.position - lastTargetPosition).x;

        bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

        if (updateLookAheadTarget) {
            lookAheadPos = lookAheadFactor * Vector3.right * Mathf.Sign(xMoveDelta);
        }
        else {
            lookAheadPos = Vector3.MoveTowards(lookAheadPos, Vector3.zero, Time.deltaTime * lookAheadReturnSpeed);
        }

        Vector3 aheadTargetPos = target.position + lookAheadPos + Vector3.forward * offsetZ;
        Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref currentVelocity, damping);
        newPos.y = Mathf.Clamp(newPos.y, yRestriction, Mathf.Infinity);

        if (ReachedLimit(true)) {
            newPos.x = transform.position.x;
            newPos.z = transform.position.z;
        }
        if (ReachedLimit(false)) {
            newPos.y = transform.position.y;
        }

        transform.position = newPos;
        lastTargetPosition = target.position;
    }

    private bool ReachedLimit(bool horizontal) {
        //Calculate the camera's extent in world coordinates
        
        if (horizontal) {
            Vector2 rightScreenLimit = mainCamera.ScreenToWorldPoint(new Vector2(mainCamera.pixelWidth, 0f));
            Vector2 leftScreenLimit = mainCamera.ScreenToWorldPoint(new Vector2(0f, 0f));
            //If the camera can the camera see the edge of the level to the right
            if (rightScreenLimit.x >= rightLimit.position.x && !rightLimited) {
                rightLimited = true;
                return true;
            }
            //If the camera can the camera see the edge of the level to the left
            else if (leftScreenLimit.x <= leftLimit.position.x && !leftLimited) {
                leftLimited = true;
                return true;
            }
            //If the camera can now follow the player from the right
            else if (rightLimited && transform.position.x > target.position.x) {
                rightLimited = false;
                return false;
            }
            //If the camera can now follow the player from the left
            else if (leftLimited && transform.position.x < target.position.x) {
                leftLimited = false;
                return false;
            }
            if (rightLimited || leftLimited) {
                return true;
            }
            return false;
        }
        else {
            Vector2 upScreenLimit = mainCamera.ScreenToWorldPoint(new Vector2(0f, mainCamera.pixelHeight));
            Vector2 downScreenLimit = mainCamera.ScreenToWorldPoint(new Vector2(0f, 0f));
            //If the camera can the camera see the edge of the level to the right
            if (upScreenLimit.y >= upLimit.position.y && !upLimited) {
                upLimited = true;
                return true;
            }
            //If the camera can the camera see the edge of the level to the left
            else if (downScreenLimit.y <= downLimit.position.y && !downLimited) {
                downLimited = true;
                return true;
            }
            //If the camera can now follow the player from the right
            else if (upLimited && transform.position.y > target.position.y) {
                upLimited = false;
                return false;
            }
            //If the camera can now follow the player from the left
            else if (downLimited && transform.position.y < target.position.y) {
                downLimited = false;
                return false;
            }
            if (upLimited || downLimited) {
                return true;
            }
            return false;
        }
    }

	public void FindPlayer() {
		if (nextTimeToSearch <= Time.time) {
			GameObject searchResult = GameObject.FindGameObjectWithTag ("Player");
			if (searchResult != null) {
				target = searchResult.transform;
			}
			nextTimeToSearch = Time.time + 0.5f;
		}	
	}

}
