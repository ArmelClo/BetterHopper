using Vintagestory.API.Common;

namespace BetterHopper;

public class BetterHopperModSystem : ModSystem
{
    public override void Start(ICoreAPI api)
    {
        api.RegisterBlockClass(Mod.Info.ModID + ".BetterHopper", typeof(BetterHopperBlock));
        api.RegisterBlockEntityClass(Mod.Info.ModID + ".BetterHopperEntity", typeof(BetterHopperBlockEntity));
    }
}