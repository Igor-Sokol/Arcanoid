using System;

namespace Application.Scripts.Application.Scenes.Shared.Energy.Repository.Contracts
{
    public struct EnergySaveObject
    {
        public int Energy;
        public DateTime DateTime;

        public EnergySaveObject(int energy, DateTime dateTime)
        {
            Energy = energy;
            DateTime = dateTime;
        }
    }
}