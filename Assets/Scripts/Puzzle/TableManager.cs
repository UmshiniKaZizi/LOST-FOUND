using UnityEngine;

public class TableManager : MonoBehaviour
{
    public static TableManager Instance;

    [Header("All tables in the puzzle")]
    public Table[] tables;

    [Header("Door to open")]
    public Door door;

    private void Awake()
    {
        Instance = this;
    }

    // Call this whenever a table updates
    public void CheckAllTables()
    {
        foreach (var table in tables)
        {
            if (!table.HasCorrectItem())
                return; // At least one table is missing correct item
        }

        // All tables have correct items
        door.OpenDoor();
    }
}
