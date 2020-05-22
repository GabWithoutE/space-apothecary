using UnityEngine;

namespace Items.CraftingItems
{
    [CreateAssetMenu(menuName = "Items/CraftingItem")]
    public class CraftingItem : Item
    {
        public override void Spawn(Transform spawnTransform)
        {
            GameObject item = Instantiate(
                emptyGameObject,
                spawnTransform.position + RelativeSpawnPosition,
                Quaternion.identity
            );
            // item.transform.position = spawnTransform.position;
            item.transform.localScale = Size;

            SpriteRenderer spriteRenderer = item.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = Sprite;

            GetPickedUpByPlayerComponent mainComponent = item.AddComponent<GetPickedUpByPlayerComponent>();
            mainComponent.Item = this;

            MoveEntity moveEntityBehaviour = item.AddComponent<MoveEntity>();
            moveEntityBehaviour.MoveEntityDelegate = ItemMovementDelegate;
        }

        public override void GetPickedUp(Transform pickerUpper)
        {
            pickerUpper.GetComponent<PlayerInventoryReferences>().CraftingItemRuntimeSet.Add(new CraftingItemReference(this));
        }
    }
}
