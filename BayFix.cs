using Harmony;

[HarmonyPatch(typeof(HydroponicsBay), "LowFrequencyUpdate")]
public static class BayFix
{
    public static bool Prefix(HydroponicsBay __instance)
    {
        if (__instance != null)
        {
            if (__instance.mPlant == null)
            {
                if (WorldScript.instance != null)
                {
                    Segment segment = WorldScript.instance.GetSegment(__instance.mnX, __instance.mnY + 1, __instance.mnZ);
                    if (segment != null && __instance.mSegment != null)
                    {
                        if (segment.IsSegmentInAGoodState() && segment != __instance.mSegment)
                        {
                            ushort cube = segment.GetCube(__instance.mnX, __instance.mnY + 1, __instance.mnZ);
                            if (cube == 165)
                            {
                                SegmentEntity entity = segment.FetchEntity(eSegmentEntity.PlantEntity, __instance.mnX, __instance.mnY + 1, __instance.mnZ);
                                if (entity != null)
                                {
                                    if (entity is PlantEntity plant)
                                    {
                                        __instance.mPlant = plant;
                                        __instance.HasPlant = true;
                                    }
                                    else
                                    {
                                        __instance.mPlant = null;
                                        __instance.HasPlant = false;
                                    }
                                }
                                else
                                {
                                    __instance.mPlant = null;
                                    __instance.HasPlant = false;
                                }
                            }
                            else
                            {
                                __instance.mPlant = null;
                                __instance.HasPlant = false;
                            }
                        }
                    }
                }
            }
        }
        return true;
    }
}
