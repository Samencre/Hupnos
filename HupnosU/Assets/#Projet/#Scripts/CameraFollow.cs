using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; 
    public Vector3 posOffset; // Decalage de la caméra par rapport au joueur
    public float smoothTime = 0.2f; // Duree du lissage
    private Vector3 velocity; // Vitesse interne utilisee par SmoothDamp

    void Awake()
    {
        if (player != null)
            transform.position = player.position + posOffset;
    }

    void Update()
    {
        if (player == null) return;

        // Suivi lissé du joueur
        transform.position = Vector3.SmoothDamp(transform.position, player.position + posOffset, ref velocity, smoothTime);
    }
}


