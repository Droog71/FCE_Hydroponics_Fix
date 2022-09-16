using Harmony;

[HarmonyPatch(typeof(PlantEntity), "LowFrequencyUpdate")]
public static class PlantEntityFix
{
    public static bool Prefix(PlantEntity __instance, ref float ___Growth, ref float ___GrowthScalar)
    {
        if (__instance.mbOnHydroBay || __instance.mbOnAdvancedHydroBay)
        {
            if (__instance.mRoomController == null)
            {
                if (__instance.mSegment != null)
                {
                    foreach (System.Collections.Generic.List<SegmentEntity> entities in __instance.mSegment.mEntities)
                    {
                        if (entities != null)
                        {
                            foreach (SegmentEntity entity in entities)
                            {
                                if (entity != null)
                                { 
                                    if (entity is RoomController)
                                    {
                                        __instance.mRoomController = entity as RoomController;
                                    }
                                    if (__instance.mRoomController != null)
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            float growthScalar = ___GrowthScalar;
            growthScalar /= 18f;
            growthScalar = __instance.mbOnAdvancedHydroBay ? growthScalar * 15f : 0;
            if (__instance.mRoomController != null)
            {
                if (__instance.mRoomController.mrGreenhouseRating > 1f)
                {
                    growthScalar *= 2f;
                }
            }
            if (__instance.mbReadyForHarvest == false)
            {
                ___Growth += LowFrequencyThread.mrPreviousUpdateTimeStep / (1000f / growthScalar);
            }
        }
        return true;
    }
}

