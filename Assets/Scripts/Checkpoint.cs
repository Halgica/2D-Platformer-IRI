using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private SpriteRenderer indicatorRenderer;

    [SerializeField] private Color activeColor = Color.green;
    [SerializeField] private Color inactiveColor = Color.yellow;

    private void Awake()
    {
        indicatorRenderer.color = inactiveColor;
    }

    public Transform GetSpawnPoint()
    {
        return spawnPoint;
    }

    public void SetIndicatorColor(bool isActive)
    {
        indicatorRenderer.color = isActive ? activeColor : inactiveColor;
    }
}
