using StarterAssets;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    StarterAssetsInputs starterAssetsInputs;

    private void Awake()
    {
        starterAssetsInputs = GetComponentInParent<StarterAssetsInputs>();
    }

    private void Update()
    {
        if (starterAssetsInputs.shoot)
        {
            RaycastHit hit;

            Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity);
            if (hit.collider != null)
            {
                Debug.Log("Hit: " + hit.collider.name);
                starterAssetsInputs.ShootInput(false);
            }
            
        }

    }
}
