using System.Collections;
using UnityEngine;

public class Wind_Hashashin_Teleport : MonoBehaviour
{
    public GameObject myObject;
    public LayerMask enemyLayers;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            Vector2 point = transform.position; // Karakterin bulunduðu nokta
            float radius = 20f; // Dairenin yarýçapý

            Collider2D[] colliders = Physics2D.OverlapCircleAll(point, radius,enemyLayers);

            float closestDistance = Mathf.Infinity;
            Vector2 closestObjectPosition = Vector2.zero;

            foreach (Collider2D col in colliders)
            {
                float distance = Vector2.Distance(point, col.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestObjectPosition = col.transform.position;
                }
            }
            myObject.transform.position = closestObjectPosition;
        }
        
    }

    
}
