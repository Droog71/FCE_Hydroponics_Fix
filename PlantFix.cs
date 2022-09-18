using Harmony;
 
[HarmonyPatch(typeof(PlantEntity), "LowFrequencyUpdate")]
public static class PlantFix
{
    public static bool Prefix(PlantEntity __instance, ref float ___Growth, ref float ___GrowthScalar)
    {
        if (__instance.mbOnHydroBay || __instance.mbOnAdvancedHydroBay)
        {
            for (int distance = 0; distance < 64; distance += 16)
            {
                long[] up = { __instance.mnX, __instance.mnY + distance, __instance.mnZ };
                long[] down = { __instance.mnX, __instance.mnY - distance, __instance.mnZ };
                long[] left = { __instance.mnX - distance, __instance.mnY, __instance.mnZ };
                long[] right = { __instance.mnX + distance, __instance.mnY, __instance.mnZ };
                long[] front = { __instance.mnX, __instance.mnY, __instance.mnZ + distance };
                long[] back = { __instance.mnX, __instance.mnY, __instance.mnZ - distance };
                long[][] adjacent = { up, down, left, right, front, back };
                foreach (long[] location in adjacent)
                {
                    if (FoundRoomController(__instance, location[0], location[1], location[2]))
                    {
                        break;
                    }
                }
            }

            if (__instance.mbOnAdvancedHydroBay)
            {
                float growthScalar = ___GrowthScalar;
                growthScalar /= 18f;
                growthScalar = growthScalar * 45f;

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
        }
        return true;
    }

    private static bool FoundRoomController(PlantEntity plant, long x, long y, long z)
    {
        Segment segment = WorldScript.instance.GetSegment(x, y, z);
        if (segment != null)
        {
            if (segment.mEntities != null)
            {
                foreach (System.Collections.Generic.List<SegmentEntity> entities in segment.mEntities)
                {
                    if (entities != null)
                    {
                        foreach (SegmentEntity entity in entities)
                        {
                            if (entity != null)
                            {
                                if (entity is RoomController rc)
                                {
                                    if (plant.mnRoomID == rc.mnRoomID)
                                    {
                                        plant.mRoomController = rc;
                                    }
                                }
                                if (plant.mRoomController != null)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
        }
        return false;
    }
}
