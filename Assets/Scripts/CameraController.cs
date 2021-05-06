using UnityEngine;

public class CameraController : MonoBehaviour {

	public float panSpeed = 30f;
	public float panBorderThickness = 10f;

	public float scrollSpeed = 5f;
	public float minY = 10f;
	public float maxY = 80f;

	BuildManager buildManager;

	void Start () {

		BuildManager buildManager;
		buildManager = BuildManager.instance;
	}

	// Update is called once per frame
	void Update () {

		if (GameManager.GameIsOver)
		{
			this.enabled = false;
			return;
		}

		if (Input.GetKey("w"))
		{
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
		}
		if (Input.GetKey("s"))
		{
			transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
		}
		if (Input.GetKey("d"))
		{
			transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
		}
		if (Input.GetKey("a"))
		{
			transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
		}

		float scroll = Input.GetAxis("Mouse ScrollWheel");

		Vector3 pos = transform.position;

		pos.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
		pos.y = Mathf.Clamp(pos.y, minY, maxY);

		transform.position = pos;

		// findNearestNode();

	}

	void findNearestNode() {
		GameObject[] floors = GameObject.FindGameObjectsWithTag("Node");
		float shortestDistance = Mathf.Infinity;
		Node nearestNode = null;
		foreach (GameObject floor in floors)
		{
			float distanceToFloor = Vector3.Distance(transform.position, floor.transform.position);

			Node node = floor.GetComponent<Node>();
			node.rend.material.color = node.notEnoughMoneyColor;
			if (distanceToFloor < shortestDistance)
			{
				shortestDistance = distanceToFloor;
				nearestNode = node;
			}
		}
		nearestNode.rend.material.color = nearestNode.hoverColor;
		if (Input.GetMouseButtonDown(0) || Input.GetKey("l")) {
			Debug.Log("A");
			TurretBlueprint tb = buildManager.GetTurretToBuild();
			Debug.Log(tb);
			nearestNode.BuildTurret(tb);
		}
		// else {Debug.Log("no");}
	}
}
