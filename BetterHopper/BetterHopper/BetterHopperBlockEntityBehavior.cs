using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Common.Entities;
using Vintagestory.API.Datastructures;
using Vintagestory.API.MathTools;
using Vintagestory.API.Server;

namespace BetterHopper;

public class BetterHopperBlockEntityBehavior: BlockEntityBehavior
{

    private long id;
    public BetterHopperBlockEntityBehavior(BlockEntity blockentity) : base(blockentity)
    {
    }

    public override void Initialize(ICoreAPI api, JsonObject json)
    {
        if (api is ICoreServerAPI sapi)
        {
            id = sapi.Event.RegisterGameTickListener((delta) =>
            {
                IWorldAccessor world = api.World;
                //align the position to the center of the block
                Vec3d pos = Pos.ToVec3d().OffsetCopy(0.5, 1.5, 0.5);
                Entity[] items = world.GetEntitiesAround(pos, 0.6f, 1.0f, entity => entity is EntityItem);
                foreach (Entity item in items)
                {
                    sapi.Logger.Debug($"Armel: {item}");
                    BetterHopperBlock hopper = world.BlockAccessor.GetBlock(Pos) as BetterHopperBlock;
                    hopper!.OnEntityCollide(world, item, Pos, BlockFacing.UP, Vec3d.Zero, false);
                }
            }, 100);
        }
        
        base.Initialize(api, json);
    }

    public override void OnBlockRemoved()
    {
        Blockentity.UnregisterGameTickListener(id);
        base.OnBlockRemoved();
    }
}