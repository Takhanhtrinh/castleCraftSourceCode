public enum ItemType
{

    NONE,
    AXE,
    SPEAR,
    SWORD,
    TOWER,
    WOOD,
    STONE
}
public class InventoryItem
{
    public ItemType m_item;
    public int m_Quantity;

public InventoryItem()
{
    m_item = ItemType.NONE;
    m_Quantity = 0;
}
public InventoryItem(ItemType item, int quantity)
{
    if (quantity > 0)
    {
        m_item = item;
        m_Quantity = quantity;
    }
    else 
    {
        item = ItemType.NONE;
        quantity = 0;
    }
}
public InventoryItem(ItemType type)
{
    m_item = type;
    m_Quantity = 1;
}
}