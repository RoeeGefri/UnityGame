using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BuilderManger : MonoBehaviour
{
    public Transform ballonsFolder;
    public Transform playerCam;
    public LayerMask hitRay;


    public GameObject turretToBuild;
    private GameObject turretToMove;
    private bool isValid;
    private RaycastHit hit;



    void Update()
    {
        
        if(turretToBuild == null)
            return;

        if(Physics.Raycast(playerCam.position,playerCam.forward,out hit, 100f, hitRay) && hit.collider.CompareTag("Ground"))
        {
            hit.point = new Vector3(hit.point.x,0,hit.point.z);
            if (!isValid)
            {
                turretToMove = Instantiate(turretToBuild,hit.point,Quaternion.identity);
                SetLayer(turretToMove);
                isValid = true;
            }
            else
            {
                turretToMove.transform.position = hit.point;
            }
        }
        else
        {
            isValid = false;
            Destroy(turretToMove);
        }

    }


    private void OnMouseDown()
    {
        if(Physics.Raycast(playerCam.position, playerCam.forward, out hit, 100f, hitRay) && hit.collider.name == "Sign")
        {
            turretToBuild = hit.collider.GetComponent<turretHandle>().turret;
            if(turretToBuild == null)
            {
                Destroy(turretToMove);
                isValid = false;
            }
            return;
        }

        if(turretToBuild == null)
        {
            return;
        }

        hit.point = new Vector3(hit.point.x,0,hit.point.z);
        if (hit.collider.CompareTag("Ground"))
        {
            GameObject go = Instantiate(turretToBuild, hit.point, Quaternion.identity);
            switch (go.name)
            {
                case "Turret(Clone)":
                    go.GetComponent<RegularTurret>().ballons = ballonsFolder;
                    break;
                case "CannonTurret(Clone)":
                    go.GetComponent<CannonTurret>().ballons = ballonsFolder;
                    break;
                case "LaserTurret(Clone)":
                    go.GetComponent<LaserTurretScript>().ballons = ballonsFolder;
                    break;
                default:
                    break;
            }

            turretToBuild = null;
            Destroy(turretToMove);
            isValid = false;
        }


    }


    private void SetLayer(GameObject go)
    {
        go.layer = LayerMask.NameToLayer("ignoreRay");

        foreach(Transform obj in go.GetComponentInChildren<Transform>())
        {
            SetLayer(obj.gameObject);
        }
    }


}
