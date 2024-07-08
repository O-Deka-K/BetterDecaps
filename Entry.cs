using System.IO;
using Newtonsoft.Json;
using ThunderRoad;

namespace BetterDecaps
{
    public class Entry : ThunderScript
    {
        public static string Version = "2.2";

        [ModOption("Better Decaps", "Reduces head pop force on decapitation", defaultValueIndex = 1)]
        public static bool EnableBetterDecaps = true;

        [ModOption("Nonfatal Dismemberments", "Makes hand, arm and foot dismemberments nonfatal", defaultValueIndex = 1)]
        public static bool EnableNonfatalDismemberment = true;

        public override void ScriptLoaded(ModManager.ModData modData)
        {
            base.ScriptLoaded(modData);

            // Add the event handler
            EventManager.onCreatureSpawn += EventManager_onCreatureSpawn;
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
            if (EnableBetterDecaps)
            {
                creature.ragdoll.GetPart(RagdollPart.Type.Head).data.sliceSeparationForce = 0.5f;
            }
            else
            {
                creature.ragdoll.GetPart(RagdollPart.Type.Head).data.sliceSeparationForce = 3.5f;
            }

            // Nonfatal Dismemberment
            if (EnableNonfatalDismemberment)
            {
                creature.ragdoll.GetPart(RagdollPart.Type.LeftArm).data.sliceForceKill = false;
                creature.ragdoll.GetPart(RagdollPart.Type.RightArm).data.sliceForceKill = false;
                creature.ragdoll.GetPart(RagdollPart.Type.LeftHand).data.sliceForceKill = false;
                creature.ragdoll.GetPart(RagdollPart.Type.RightHand).data.sliceForceKill = false;
                creature.ragdoll.GetPart(RagdollPart.Type.LeftFoot).data.sliceForceKill = false;
                creature.ragdoll.GetPart(RagdollPart.Type.RightFoot).data.sliceForceKill = false;
            }
            else
            {
                creature.ragdoll.GetPart(RagdollPart.Type.LeftArm).data.sliceForceKill = true;
                creature.ragdoll.GetPart(RagdollPart.Type.RightArm).data.sliceForceKill = true;
                creature.ragdoll.GetPart(RagdollPart.Type.LeftHand).data.sliceForceKill = true;
                creature.ragdoll.GetPart(RagdollPart.Type.RightHand).data.sliceForceKill = true;
                creature.ragdoll.GetPart(RagdollPart.Type.LeftFoot).data.sliceForceKill = true;
                creature.ragdoll.GetPart(RagdollPart.Type.RightFoot).data.sliceForceKill = true;
            }
        }
    }
}
