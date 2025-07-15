using HarmonyLib;
using Vintagestory.API.Common;
using Vintagestory.API.MathTools;

namespace BetterHopper;

[HarmonyPatch]
public class BetterHopperModSystem : ModSystem
{
    private Harmony Harmony;
    
    public override void Start(ICoreAPI api)
    {
        if (!Harmony.HasAnyPatches(Mod.Info.ModID))
        {
            Harmony = new Harmony(Mod.Info.ModID);
            Harmony.PatchAll(); // Applies all harmony patches
        }
        
        api.RegisterBlockClass(Mod.Info.ModID + ".BetterHopper", typeof(BlockBetterHopper));
    }

    [HarmonyPrefix]
    [HarmonyPatch(typeof(EntityBehaviorPassivePhysics), nameof(EntityBehaviorPassivePhysics.OnPhysicsTick))]
    public static void OnPhysicsTick(EntityBehaviorPassivePhysics __instance)
    {

        BlockPos posMinus1 = __instance.entity.Pos.AsBlockPos.DownCopy();
        BlockPos posMinus2 = posMinus1.DownCopy();
        Block blockMinus1 = __instance.Entity.World.BlockAccessor.GetBlock(posMinus1);
        Block blockMinus2 = __instance.Entity.World.BlockAccessor.GetBlock(posMinus2);
        if (blockMinus1 is BlockBetterHopper hopper)
        {
            hopper.OnEntityCollide(__instance.Entity.World, __instance.Entity, posMinus1, BlockFacing.UP, Vec3d.Zero, false);
            return;
        }
        if (blockMinus2 is BlockBetterHopper hopper2)
        {
            hopper2.OnEntityCollide(__instance.Entity.World, __instance.Entity, posMinus2, BlockFacing.UP, Vec3d.Zero, false);
        }
    }
    
    
    public override void Dispose()
    {
        Harmony?.UnpatchAll(Mod.Info.ModID);
    }
}