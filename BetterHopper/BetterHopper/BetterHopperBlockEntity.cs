using System;
using Vintagestory.API.Common;
using Vintagestory.API.Common.Entities;
using Vintagestory.API.MathTools;
using Vintagestory.API.Server;
using Vintagestory.GameContent;

namespace BetterHopper;

public class BetterHopperBlockEntity: BlockEntityItemFlow 
{
    public override void Initialize(ICoreAPI api)
    {
        base.Initialize(api);
        RegisterGameTickListener((delta) =>
            {
                IWorldAccessor world = api.World;
                //align the position to the center of the block
                Vec3d pos = Pos.ToVec3d().OffsetCopy(0.5, 1.5, 0.5);
                Entity[] items = world.GetEntitiesAround(pos, 0.6f, 1.0f, entity => entity is EntityItem);
                foreach (Entity item in items)
                {
                    BetterHopperBlock hopper = world.BlockAccessor.GetBlock(Pos) as BetterHopperBlock;
                    hopper!.OnEntityCollide(world, item, Pos, BlockFacing.UP, Vec3d.Zero, false);
                }
            }, 100);
    }
}