using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchBeaconEngine : MonoBehaviour {

    GameObject player;
    public float torchesIgnitionDistance;
    public LayerMask raycastMaska;
    float playerViewAngle;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerViewAngle = 180;
    }

    void Update()
    {
        foreach (Transform child in transform)
        {
            updateTorchBeaconStatus(child.gameObject);
        }
    }

    void updateTorchBeaconStatus(GameObject torch)
    {
        Vector3 directionToPlayer = player.transform.position - torch.transform.position;
        //float angleBetweenPlayerAndTorch = Vector3.Angle(-directionToPlayer, player.transform.forward);//calculate angle beetween vistion(from enemy's eyes) and vector from enemy to player

        if (/*angleBetweenPlayerAndTorch < playerViewAngle * 0.5f && */directionToPlayer.magnitude < torchesIgnitionDistance)
        {
            RaycastHit hit;

            if (Physics.Raycast(torch.transform.position, directionToPlayer, out hit, torchesIgnitionDistance, raycastMaska))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    torch.GetComponent<Light>().renderMode = LightRenderMode.ForcePixel;
                    torch.SetActive(true);
                }
            }
        }
        else
        {
            torch.SetActive(false);
            //torch.GetComponent<Light>().renderMode = LightRenderMode.ForceVertex;
        }
    }
}
