using Harmony;

[HarmonyPatch(typeof(HydroponicsBay), "GetPopupText")]
public static class BayInfo
{
    static void Postfix(HydroponicsBay __instance, ref string __result)
    {
        bool foundRoomController = true;

        if (__instance != null)
        {
            if (__instance.mPlant != null)
            {
                __result += "\nAdvanced bay: " + __instance.mPlant.mbOnAdvancedHydroBay;

                int growthMultiplier = 15;

                if (__instance.mPlant.mbOnAdvancedHydroBay)
                {
                    growthMultiplier = 30;
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
                else
                {
                    foundRoomController = false;
                }

                __result += "\nGrowth multiplier: " + growthMultiplier + "x";
            }

            if (foundRoomController == false)
            {
                __result += "\n\nRoom controller not found.";
                __result += "\nPlease relocate bay.";
            }
        }
    }
}
