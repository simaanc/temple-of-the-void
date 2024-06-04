using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class TilemapInteraction2D : MonoBehaviour
{
    public Tilemap tilemap;
    public InputAction useAction;
    private bool isTilemapVisible = false;

    private void Start()
    {
        // Ensure the tilemap and its collider are initially visible and enabled
        if (tilemap != null)
        {
            tilemap.gameObject.SetActive(true);
            TilemapCollider2D tilemapCollider = tilemap.GetComponent<TilemapCollider2D>();
            if (tilemapCollider != null)
            {
                tilemapCollider.enabled = true;
            }
        }
    }

    private void OnEnable()
    {
        useAction.Enable();
        useAction.performed += OnUsePerformed;
    }

    private void OnDisable()
    {
        useAction.performed -= OnUsePerformed;
        useAction.Disable();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            useAction.Enable();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            useAction.Disable();
        }
    }

    private void OnUsePerformed(InputAction.CallbackContext context)
    {
        Vector3Int cellPosition = GetPlayerCellPosition();
        if (tilemap.HasTile(cellPosition))
        {
            ToggleTilemap(cellPosition);
        }
    }

    Vector3Int GetPlayerCellPosition()
    {
        Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        return tilemap.WorldToCell(playerPosition);
    }

    void ToggleTilemap(Vector3Int cellPosition)
    {
        isTilemapVisible = !isTilemapVisible;
        tilemap.SetTileFlags(cellPosition, TileFlags.None);
        tilemap.SetColor(cellPosition, isTilemapVisible ? Color.white : new Color(1, 1, 1, 0));

        TilemapCollider2D tilemapCollider = tilemap.GetComponent<TilemapCollider2D>();
        if (tilemapCollider != null)
        {
            tilemapCollider.enabled = isTilemapVisible;
        }
    }
}
