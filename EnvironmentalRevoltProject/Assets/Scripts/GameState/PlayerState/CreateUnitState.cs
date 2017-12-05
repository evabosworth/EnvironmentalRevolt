using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateUnitState : ScriptableObject, IPlayerState
{
    GameObjectGenerator objectGenerator;
    public GameObject unit;

    public void setUnit(GameObject passedUnit)
    {
        unit = passedUnit;
    }

    public void clickAction()
    {
        objectGenerator = FindObjectOfType<GameObjectGenerator>();
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Terrain")
                {
                    Vector3 newPosition = new Vector3();
                    newPosition = hit.transform.localPosition + Vector3.up;
                    objectGenerator.createAndDisplayGameObject(unit, newPosition, "unit");
                }
            }
        }
            
    }
}
