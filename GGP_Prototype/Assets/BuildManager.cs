using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("More than one build manager");
        }

        instance = this;
    }

    private TurretBlueprint turretToBuild;

    public bool CanBuild { get { return turretToBuild != null; } }

    public void SelectTurretToBuild(TurretBlueprint _turret)
    {
        turretToBuild = _turret;
    }

    public void BuildTurretOn(Node node)
    {
        if (PlayerStats.Money < turretToBuild.cost)
        {
            Debug.Log("Not Enough Money");
            return;
        }

        PlayerStats.Money -= turretToBuild.cost;

        // Build a turret
        GameObject _turret = (GameObject)Instantiate(turretToBuild.prefab, node.GetBuildPosition(), Quaternion.identity);

        node.turret = _turret;

        Debug.Log("Turret Built! Money left: " + PlayerStats.Money);
    }
}
