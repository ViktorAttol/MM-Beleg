using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DimensionController : MonoBehaviour
{
    public static DimensionController Instance;
    private EntityDimension currentDimension = EntityDimension.RED;
    
    // Start is called before the first frame update
    void Start()
    {
        currentDimension = EntityDimension.RED;
        if (Instance != null) {
            Destroy(Instance);
        }
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetDimension(EntityDimension.RED);
        } else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetDimension(EntityDimension.GREEN);

        } else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetDimension(EntityDimension.BLUE);

        } 
    }

    void SetDimension(EntityDimension dimension)
    {
        currentDimension = dimension;
    }

    public EntityDimension GetCurrentDimension()
    {
        return currentDimension;
    }
}
