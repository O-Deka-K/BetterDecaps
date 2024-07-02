using System.IO;
using Newtonsoft.Json;
using ThunderRoad;

namespace BetterDecaps
{
    public class ModifyRagdoll : ThunderScript
    {
        public static Config Config { get; private set; }
        public static string Version = "2.0";
        
        public override void ScriptLoaded(ModManager.ModData modData)
        {
            base.ScriptLoaded(modData);
            LoadConfig(modData.fullPath);

            // Add the event handler
            EventManager.onCreatureSpawn += EventManager_onCreatureSpawn;
        }

        private void LoadConfig(string modPath)
        {
            string path = Path.Combine(modPath, "BetterDecaps.json");

            // Load the config file, creating it if it doesn't exist
            if (!File.Exists(path))
            {
                File.WriteAllText(path, JsonConvert.SerializeObject(new Config(), Formatting.Indented));
            }

            Config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(path));
        }

        private void EventManager_onCreatureSpawn(Creature creature)
        {
            if (creature == null)
            {
                return;
            }

            // Only affect human NPCs
            if (creature.creatureId != "HumanMale" && creature.creatureId != "HumanFemale")
            {
                return;
            }

            // Better Decaps
            if (Config.enableBetterDecaps)
            {
                creature.ragdoll.GetPart(RagdollPart.Type.Head).data.sliceSeparationForce = 0.5f;
            }

            // Nonfatal Dismemberment
            if (Config.enableNonfatalDismemberment)
            {
                creature.ragdoll.GetPart(RagdollPart.Type.LeftArm).data.sliceForceKill = false;
                creature.ragdoll.GetPart(RagdollPart.Type.RightArm).data.sliceForceKill = false;
                creature.ragdoll.GetPart(RagdollPart.Type.LeftHand).data.sliceForceKill = false;
                creature.ragdoll.GetPart(RagdollPart.Type.RightHand).data.sliceForceKill = false;
                creature.ragdoll.GetPart(RagdollPart.Type.LeftFoot).data.sliceForceKill = false;
                creature.ragdoll.GetPart(RagdollPart.Type.RightFoot).data.sliceForceKill = false;
            }
        }
    }
}
