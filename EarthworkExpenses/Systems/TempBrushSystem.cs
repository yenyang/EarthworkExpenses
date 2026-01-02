// <copyright file="ModifyNetCompositionDataSystem.cs" company="Yenyang's Mods. MIT License">
// Copyright (c) Yenyang's Mods. MIT License. All rights reserved.
// </copyright>

using Colossal.Entities;
using Game;
using Game.Common;
using System;
using Unity.Collections;
using Unity.Entities;

namespace	EarthworkExpenses.Systems
{
    public partial class TempBrushSystem : GameSystemBase
    {
        private EntityQuery m_TempBrushQuery;
        private ModificationBarrier4 m_Barrier;

        protected override void OnCreate()
        {
            base.OnCreate();

            // Get System instances for other systems. 
            m_Barrier = World.GetOrCreateSystemManaged<ModificationBarrier4>();

            // Establish query for relevant entity components.
            m_TempBrushQuery = SystemAPI.QueryBuilder()
                .WithAllRW<Game.Tools.Temp>()
                .WithAll<Game.Tools.Brush>()
                .WithNone<Game.Common.Deleted>()
                .Build();

            RequireForUpdate(m_TempBrushQuery);


            Mod.log.Info($"{nameof(TempBrushSystem)}.{nameof(OnCreate)}");
        }

        protected override void OnUpdate()
        {
            NativeArray<Entity> entities = m_TempBrushQuery.ToEntityArray(Allocator.Temp);
            EntityCommandBuffer buffer = m_Barrier.CreateCommandBuffer();

            for (int i = 0; i < entities.Length; i++)
            {
                if (entities[i] == Entity.Null ||
                   !EntityManager.TryGetComponent(entities[i], out Game.Tools.Temp temp))
                {
                    continue;
                }

                temp.m_Cost = 100;
                buffer.SetComponent(entities[i], temp);
            }
            
        }
    }
}

