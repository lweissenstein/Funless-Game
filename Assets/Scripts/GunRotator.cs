using UnityEngine;

public class GunRotator : MonoBehaviour
{
    private Transform activeGunTransform;

    void Update()
    {
        if (activeGunTransform == null || !activeGunTransform.gameObject.activeInHierarchy)
            activeGunTransform = FindActiveGun();

        if (activeGunTransform == null) return;

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -Camera.main.transform.position.z;
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);

        Vector2 direction = (worldMousePos - activeGunTransform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        float rotationOffset = GetRotationOffset(activeGunTransform.parent.name);
        float rotationAngle = angle + rotationOffset;
        activeGunTransform.rotation = Quaternion.Euler(0f, 0f, rotationAngle);

        ApplySpriteFlip(activeGunTransform.parent.name);
    }

    private Transform FindActiveGun()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.activeSelf)
            {
                Transform gun = child.Find("PlayerGunSprite");
                if (gun != null) return gun;
            }
        }
        return null;
    }

    private float GetRotationOffset(string prefabParentName)
    {
        string name = prefabParentName.ToLower();
        if (name.Contains("up")) return 90f;
        if (name.Contains("down")) return -90f;
        if (name.Contains("left")) return 180f;
        return 0f;
    }

    private void ApplySpriteFlip(string prefabParentName)
    {
        string name = prefabParentName.ToLower();
        Vector3 newScale = Vector3.one;

        if (name.Contains("right"))
            newScale = Vector3.one;        
        else if (name.Contains("left"))
            newScale = new Vector3(1f, 1f, 1f); 
        else if (name.Contains("up") || name.Contains("down"))
            newScale = new Vector3(-1f, -1f, 1f);

        activeGunTransform.localScale = newScale;
    }
}
