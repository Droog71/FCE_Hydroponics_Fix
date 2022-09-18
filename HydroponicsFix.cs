using Harmony;
using System.Reflection;

public class HydroponicsFix : FortressCraftMod
{
    public HarmonyInstance mHarmony;

    public void Awake()
    {
        mHarmony = HarmonyInstance.Create("HydroponicsFix");
        mHarmony.PatchAll(Assembly.GetExecutingAssembly());
    }

    public override ModRegistrationData Register()
    {
        return new ModRegistrationData();
    }
}
