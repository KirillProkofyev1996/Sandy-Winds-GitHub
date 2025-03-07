using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipTracksAnimation : MonoBehaviour
{
    [SerializeField] private float speed;
    private Vector2 offset;
    private Material tracks;
    private ShipInput shipInput;

    private void Start()
    {
        shipInput = FindObjectOfType<ShipInput>();
        tracks = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        if (shipInput.GetVerticalDirection().y != 0 || shipInput.GetHorizontalDirection().x != 0)
        {
            offset = new Vector2(Time.time * speed, 0);
            offset.y = 0;
            
            tracks.mainTextureOffset = offset;
        }
    }
}
