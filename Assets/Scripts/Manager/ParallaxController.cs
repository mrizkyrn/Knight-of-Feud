using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    private Transform cam;
    private Vector3 camStartPosition;

    private GameObject[] backgrounds;
    private Renderer[] renderers;
    private float[] backSpeed;

    private float farthestBack;

    [Range(0f, 0.5f)]
    [SerializeField] float parallaxSpeed = 0.2f;

    private void Start()
    {
        cam = Camera.main.transform;
        camStartPosition = cam.position;

        int backCount = transform.childCount;

        backgrounds = new GameObject[backCount];
        renderers = new Renderer[backCount];
        backSpeed = new float[backCount];

        for (int i = 0; i < backCount; i++)
        {
            backgrounds[i] = transform.GetChild(i).gameObject;
            renderers[i] = backgrounds[i].GetComponent<Renderer>();
        }

        CalculateFarthestBack();
        CalculateBackSpeed(backCount);
    }

    private void LateUpdate()
    {
        float distance = cam.position.x - camStartPosition.x;
        transform.position = new Vector3(cam.position.x, transform.position.y, 0);

        for (int i = 0; i < backgrounds.Length; i++)
        {
            float speed = backSpeed[i] * parallaxSpeed;
            Vector2 offset = new Vector2(distance, 0) * speed;
            renderers[i].material.SetTextureOffset("_MainTex", offset);
        }
    }

    private void CalculateFarthestBack()
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            float backgroundZ = backgrounds[i].transform.position.z;
            float camZ = cam.position.z;

            if ((backgroundZ - camZ) > farthestBack)
            {
                farthestBack = backgroundZ - camZ;
            }
        }
    }

    private void CalculateBackSpeed(int backCount)
    {
        for (int i = 0; i < backCount; i++)
        {
            float backgroundZ = backgrounds[i].transform.position.z;
            float camZ = cam.position.z;

            backSpeed[i] = 1 - (backgroundZ - camZ) / farthestBack;
        }
    }
}
