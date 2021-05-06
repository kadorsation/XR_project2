using UnityEngine;

public class Shop : MonoBehaviour {

	public TurretBlueprint standardTurret;
	public TurretBlueprint missileLauncher;
	public TurretBlueprint laserBeamer;
	public TurretBlueprint attackBot;
	public TurretBlueprint drone;
	public TurretBlueprint ciccio;
	public TurretBlueprint gun;
	public TurretBlueprint sphere;


	BuildManager buildManager;

	void Start ()
	{
		buildManager = BuildManager.instance;
	}

	public void SelectStandardTurret ()
	{
		Debug.Log("Standard Turret Selected");
		buildManager.SelectTurretToBuild(standardTurret);
	}

	public void SelectMissileLauncher()
	{
		Debug.Log("Missile Launcher Selected");
		buildManager.SelectTurretToBuild(missileLauncher);
	}

	public void SelectLaserBeamer()
	{
		Debug.Log("Laser Beamer Selected");
		buildManager.SelectTurretToBuild(laserBeamer);
	}

	public void SelectAttackBot ()
	{
		Debug.Log("AttackBot Selected");
		buildManager.SelectTurretToBuild(attackBot);
	}

	public void SelectDrone()
	{
		Debug.Log("Drone Selected");
		buildManager.SelectTurretToBuild(drone);
	}

	public void SelectCiccio()
	{
		Debug.Log("Ciccio Selected");
		buildManager.SelectTurretToBuild(ciccio);
	}

	public void SelectGun ()
	{
		Debug.Log("Gun Selected");
		buildManager.SelectTurretToBuild(gun);
	}

	public void SelectSphere()
	{
		Debug.Log("Sphere Selected");
		buildManager.SelectTurretToBuild(sphere);
	}

}
