using Harmony;

[HarmonyPatch(typeof(HydroponicsBay), "GetPopupText")]
public static class BayInfo
{
    static void Postfix(HydroponicsBay __instance, ref string __result)
    {
        if (__instance != null)
        {
            if (__instance.mPlant != null)
            {
                __result += "\nAdvanced bay: " + __instance.mPlant.mbOnAdvancedHydroBay;

                int growthMultiplier = 15;

                if (__instance.mPlant.mbOnAdvancedHydroBay)
                {
                    growthMultiplier += 15;
                }

                if (__instance.mPlant.mRoomController != null)
                {
                    float ghrf = __instance.mPlant.mRoomController.mrGreenhouseRating;
                    int greenHouseRating = (int) (ghrf * 100);
                    __result += "\nGreenhouse rating: " + greenHouseRating + "%";
                    if (greenHouseRating > 100)
                    {
                        growthMultiplier *= 2;
                    }
                }

                __result += "\nGrowth multiplier: " + growthMultiplier + "x";
            }
            else
            {
                __result += "\nPlant not found!";
            }
        }
    }
}

