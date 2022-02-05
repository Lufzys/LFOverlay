using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using LFOverlay.Cheat.SDK.Variables;
using Memorys;

namespace LFOverlay.Cheat.SDK.Entites
{
    class Entity
    {
        public int Address = 0;

        public Entity(int address)
        {
            Address = address;
        }

        public static bool operator !(Entity entity)
        {
            return (entity.Address <= 0);
        }

        public bool Dormant => Memory.Read<bool>(Address + Offsets.signatures.m_bDormant);
        public int Health => Memory.Read<int>(Address + Offsets.netvars.m_iHealth);
        public int Armor => Memory.Read<int>(Address + Offsets.netvars.m_ArmorValue);
        public Vector3 Origin => Memory.Read<Vector3>(Address + Offsets.netvars.m_vecOrigin);
        public Vector2 PunchAngle => Memory.Read<Vector2>(Address + Offsets.netvars.m_aimPunchAngle);
        public int BoneMatrix => Memory.Read<int>(Address + Offsets.netvars.m_dwBoneMatrix);
        public Enums.Team Team => (Enums.Team)Memory.Read<int>(Address + Offsets.netvars.m_iTeamNum);

        public Vector3 Bone(int boneId)
        {
            return new Vector3
            {
                X = Memory.Read<float>(BoneMatrix + 0x30 * boneId + 0x0C),
                Y = Memory.Read<float>(BoneMatrix + 0x30 * boneId + 0x1C),
                Z = Memory.Read<float>(BoneMatrix + 0x30 * boneId + 0x2C)
            };
        }
    }
}
