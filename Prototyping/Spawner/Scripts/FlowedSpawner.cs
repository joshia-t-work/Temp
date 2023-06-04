using DKP.CutsceneSystem;
using DKP.InstancePooling;
using DKP.SaveSystem.Data;
using DKP.UnitSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;
using static DKP.SpawnSystem.InternalData;

namespace DKP.SpawnSystem
{
    /// <summary>
    /// Spawner that creates units through ActionScript
    /// </summary>
    public class FlowedSpawner : MonoBehaviour
    {
        [SerializeField] InternalData SpawnerData;
        CancellationToken ct;
        CancellationTokenSource cts = new CancellationTokenSource();

        private void Awake()
        {
            ct = cts.Token;
            CutsceneEvents.I.Spawn += Spawn;
            CutsceneEvents.I.StopObjective += StopObjective;
        }

        private void OnDestroy()
        {
            cts.Cancel();
            cts.Dispose();
            CutsceneEvents.I.Spawn -= Spawn;
            CutsceneEvents.I.StopObjective -= StopObjective;
        }

        private Task Spawn(CancellationToken ct, SpawnCommand spawnCommand)
        {
            UnitData unit = SpawnerData.FindUnit(spawnCommand.Unit);
            SpawnerData spawner = SpawnerData.FindSpawner(spawnCommand.SpawnPosition);
            float delay = float.Parse(spawnCommand.Delay);
            int count = int.Parse(spawnCommand.Count);
            float interval = float.Parse(spawnCommand.Interval);
            _ = StartSpawner(unit, spawner, delay, count, interval);
            return Task.CompletedTask;
        }

        private Task StopObjective(CancellationToken ct)
        {
            cts.Cancel();
            return Task.CompletedTask;
        }

        private async Task StartSpawner(UnitData unit, SpawnerData spawner, float delay, int count, float interval)
        {
            try
            {
                await Task.Delay((int)(delay * 1000), ct);
                for (int i = 0; i < count; i++)
                {
                    SpawnerData.Spawn(transform, unit, spawner);
                    await Task.Delay((int)(interval * 1000), ct);
                }
            }
            catch (TaskCanceledException) { }
        }
    }
}
